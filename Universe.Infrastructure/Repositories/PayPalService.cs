using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Universe.Core.Abstractions.Options;
using Universe.Core.Contracts.PayPal;
using Universe.Core.Enums;
using Universe.Core.Interfaces;
using static System.Net.WebRequestMethods;

namespace Universe.Infrastructure.Repositories;

public class PayPalService(HttpClient _http, IOptions<PayPalSettings> payPalSettings) : IPayPalService
{
    private readonly HttpClient _http = _http;
    private readonly PayPalSettings _payPalSettings = payPalSettings.Value;

    private async Task<string> GetAccessToken()
    {
        var clientId = _payPalSettings.ClientId;
        var secret = _payPalSettings.Secret;

        var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{secret}"));

        var request = new HttpRequestMessage(HttpMethod.Post , $"{_payPalSettings.BaseUrl}/v1/oauth2/token");
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", auth);

        request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

        var response = await _http.SendAsync(request);

        var json = await response.Content.ReadAsStringAsync();

        var doc = JsonDocument.Parse(json);

        return doc.RootElement.GetProperty("access_token").GetString() ?? string.Empty;
    }

    public async Task<CreateOrderResponse> CreateOrderAsync(decimal amount)
    {
        var token = await GetAccessToken();

        var body = new
        {
            intent = "CAPTURE",
            purchase_units = new[]
            {
                new
                {
                    amount = new
                    {
                        currency_Code = "EGP",
                        value = amount.ToString("F2")
                    }
                }
            },
            application_context = new
            {
                return_url = "",
                cancel_url = ""
            }
        };

        
        var request = new HttpRequestMessage(HttpMethod.Post , $"{_payPalSettings.BaseUrl}/v2/checkout/orders");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        var response = await _http.SendAsync(request);

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

    public async Task<bool> RefundPaymentAsync(string captureId)
    {
        var token = await GetAccessToken();

        var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{_payPalSettings.BaseUrl}/v2/payments/captures/{captureId}/refund"
        );

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _http.SendAsync(request);

        return response.IsSuccessStatusCode;
    }
}
