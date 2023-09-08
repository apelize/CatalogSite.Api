using System.Text;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Controllers;

[ApiController]
[Route("order")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly HttpClient _client;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
        _client = new HttpClient();
    }

    [HttpPost]
    public async Task<ActionResult<string>> SendOrderNotificationAsync(Order order)
    {
        _client.DefaultRequestHeaders.Clear();
        var request = new
        {
            Время = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(),
            Телефон = order.PhoneNumber,
            Товары = string.Join(",", order.ProductList)
        };

        _logger.LogInformation("Order Phone: {0}, Products: {1}", order.PhoneNumber, string.Join(",", order.ProductList));
        _logger.LogInformation(Environment.GetEnvironmentVariable("Notification_URL"));
        var responce = await _client.PostAsJsonAsync(Environment.GetEnvironmentVariable("Notification_URL"), request);
        return Ok();
    }
}