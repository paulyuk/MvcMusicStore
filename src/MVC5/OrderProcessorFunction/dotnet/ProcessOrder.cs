using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace OrderProcessorFunction
{
    public class ProcessOrder
    {
        private readonly ILogger<ProcessOrder> _logger;

        public ProcessOrder(ILogger<ProcessOrder> logger)
        {
            _logger = logger;
        }

        // Routes to "api/cart/{shoppingCartId}/order"
        [Function("order")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "cart/{shoppingCartId}/order")] HttpRequest req,
            [FromBody] Order order,
            string shoppingCartId
        )
        {
            _logger.LogInformation(
                $"Received order with shoppingCardId: {shoppingCartId} from name:{order.FirstName} {order.LastName} "
            );
            _logger.LogInformation(order.ToString());

            var backendWorker = new BackendWorker(order, _logger);
            backendWorker.DoWork();

            return new OkObjectResult(order);
        }
    }

    public record Order(
        string LastName,
        string FirstName,
        string Address,
        string City,
        string State,
        string PostCode,
        string Country,
        string Email,
        string Phone,
        string Username
    );
}
