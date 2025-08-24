using Razorpay.Api;

public class RazorpayService
{
    private readonly string _key;
    private readonly string _secret;

    public RazorpayService(IConfiguration configuration)
    {
        _key = configuration["Razorpay:KeyId"];
        _secret = configuration["Razorpay:KeySecret"];
    }

    public string CreateOrder(decimal amount)
    {
        var client = new RazorpayClient(_key, _secret);
        var options = new Dictionary<string, object>
        {
            ["amount"] = amount * 100, // in paise
            ["currency"] = "INR",
            ["receipt"] = $"order_rcptid_{DateTime.UtcNow.Ticks}"
        };
        var order = client.Order.Create(options);
        return order["id"].ToString();
    }
}