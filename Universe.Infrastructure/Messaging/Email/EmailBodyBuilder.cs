namespace Universe.Infrastructure.Messaging.Email;

public static class EmailBodyBuilder
{
   

    public static string GenerateEmailBody(string template, Dictionary<string, string> templateModel)
    {
        if (!Templates.ContainsKey(template))
            throw new ArgumentException($"Template '{template}' not found.");

        var body = Templates[template];

        foreach (var item in templateModel)
        {
            body = body.Replace(item.Key, item.Value);
        }

        return body;
    }


    private static readonly Dictionary<string, string> Templates = new()
    {
        {
            "ForgetPassword",
            @"<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <title>Password Reset</title>
    <style>
        body { margin:0; padding:0; background-color:#f4f6f9; font-family:Arial,sans-serif; }
        .container { max-width:520px; margin:40px auto; background:#fff; border-radius:12px; box-shadow:0 8px 24px rgba(0,0,0,0.08); padding:40px; text-align:center; }
        h1 { margin-bottom:10px; font-size:22px; color:#222; }
        p { color:#555; font-size:15px; line-height:1.6; }
        .otp-box { margin:30px 0; font-size:34px; letter-spacing:8px; font-weight:bold; color:#111; background:#f2f4f8; padding:18px 0; border-radius:10px; }
        .warning { margin-top:25px; font-size:13px; color:#888; }
        .footer { margin-top:35px; font-size:12px; color:#aaa; }
    </style>
</head>
<body>
    <div class='container'>
        <h1>Reset Your Password</h1>
        <p>Hi <b>{{name}}</b>,</p>
        <p>We received a request to reset your password. Use the verification code below to continue.</p>
        <div class='otp-box'>{{otp}}</div>
        <p>This code will expire in <b>10 minutes</b>.</p>
        <p class='warning'>If you didn’t request this, you can safely ignore this email.</p>
        <div class='footer'>© 2026 Universe • Security Team</div>
    </div>
</body>
</html>"
        }
        // هنا تقدر تضيف أي قالب جديد بنفس الطريقة:
        // { "TemplateName", "HTML CONTENT" }
    };
}
