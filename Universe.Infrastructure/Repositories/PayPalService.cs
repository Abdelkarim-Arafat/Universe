using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using Universe.Core.Abstractions.Options;
using Universe.Core.Contracts.PayPal;
using Universe.Core.Enums;
using Universe.Core.Interfaces;
using static System.Net.WebRequestMethods;

namespace Universe.Infrastructure.Repositories;

public class PayPalService(HttpClient _httpClient, IOptions<PayPalSettings> payPalSettings) : IPayPalService
{
    private readonly HttpClient _httpClient = _httpClient;
    private readonly PayPalSettings _payPalSettings = payPalSettings.Value;

    private async Task<string> GetAccessToken()
    {
        var clientId = _payPalSettings.ClientId;
        var secret = _payPalSettings.Secret;

        var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{secret}"));

        var request = new HttpRequestMessage(HttpMethod.Post , $"{_payPalSettings.BaseUrl}/v1/oauth2/token");
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", auth);

        request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

        var response = await _httpClient.SendAsync(request);

        var json = await response.Content.ReadAsStringAsync();


        var doc = JsonDocument.Parse(json);

        return doc.RootElement.GetProperty("access_token").GetString() ?? string.Empty;
    }
    private async Task<decimal> GetEgpToUsdRate()
    {
        var json = await _httpClient.GetStringAsync("https://api.exchangerate-api.com/v4/latest/EGP");

        var doc = JsonDocument.Parse(json);

        var rate = doc.RootElement
                      .GetProperty("rates")
                      .GetProperty("USD")
                      .GetDecimal();

        return rate;
    }
    public async Task<CreateOrderResponse> CreateOrderAsync(decimal amount)
    {
        var token = await GetAccessToken();

        var rate = await GetEgpToUsdRate();
        amount *= rate;

        var body = new
        {
            intent = "CAPTURE",
            purchase_units = new[]
            {
                new
                {
                    amount = new
                    {
                        currency_code = "USD",
                        value = amount.ToString("F2")
                    }
                }
            },
            application_context = new
            {
                return_url = "https://playful-torrone-6e1691.netlify.app/student/services/payment-success",
                cancel_url = "https://playful-torrone-6e1691.netlify.app/student/services"
            }
        };
        
        var request = new HttpRequestMessage(HttpMethod.Post , $"{_payPalSettings.BaseUrl}/v2/checkout/orders");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);

        var json = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(json);

        var id = doc.RootElement.GetProperty("id").GetString() ?? string.Empty;

        var links = doc.RootElement.GetProperty("links").EnumerateArray();

        string approval = "";

        foreach (var item in links)
        {
            if(item.GetProperty("rel").GetString() == "approve")
            {
                approval = item.GetProperty("href").GetString()!;
                break;
            }
        }

        return new CreateOrderResponse (id, approval);
    }

    public async Task CaptureOrderAsync(string orderId)
    {
        var token = await GetAccessToken();

        var request = new HttpRequestMessage (
            HttpMethod.Post,
            $"{_payPalSettings.BaseUrl}/v2/checkout/orders/{orderId}/capture"
        );

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        request.Content = new StringContent("", Encoding.UTF8, "application/json");

        await _httpClient.SendAsync(request);

        //var json = await response.Content.ReadAsStringAsync();


        //var doc = JsonDocument.Parse(json);

        //if (!doc.RootElement.TryGetProperty("status", out var statusProp))
        //    throw new ArgumentNullException(json);

        //var status = doc.RootElement.GetProperty("status").GetString();

        //return response.IsSuccessStatusCode && status is "COMPLETED";
    }

    public async Task<bool> RefundPaymentAsync(string captureId)
    {
        var token = await GetAccessToken();

        var request = new HttpRequestMessage (
            HttpMethod.Post,
            $"{_payPalSettings.BaseUrl}/v2/payments/captures/{captureId}/refund"
        );

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        return response.IsSuccessStatusCode;
    }
}
