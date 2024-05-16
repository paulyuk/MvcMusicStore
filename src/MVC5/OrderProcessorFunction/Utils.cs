using Microsoft.Extensions.Logging;
using OrderProcessorFunction;

public class BackendWorker
{
    private readonly ILogger<ProcessOrder> _logger;
    private readonly Order _order;

    public BackendWorker(Order order, ILogger<ProcessOrder> logger)
    {
        _logger = logger;
        _order = order;
    }

    public void DoWork()
    {
        _logger.LogInformation("Worker running at: {time} on order for: {order}", DateTimeOffset.Now, _order.Username);
    }
}
