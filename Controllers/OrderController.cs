using System.Text;
using Entities;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult> SendOrderNotificationAsync(Order order)
    {
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add("User-Agent", "Catalog Site");
        StringBuilder orderString = new StringBuilder();
        orderString.AppendLine("Новый заказ").AppendLine($"Телефон: {order.PhoneNumber}").Append("Товары: ").AppendJoin(",", order.ProductList);
        _logger.LogInformation(orderString.ToString());

        var responce = await _client.PostAsync($"https://v1.nocodeapi.com/apelize/telegram/OXWeJwKWyDfcFGhc/sendText?text={orderString}", new StringContent(""));
        return Ok();
    }
}