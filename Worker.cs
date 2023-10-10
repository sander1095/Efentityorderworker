namespace Efentityorderworker;

public class Worker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public Worker(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {

            var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
            var user = new User("Test user");
            var order = new Order(user);

            dbContext.Users.Add(user);
            dbContext.Orders.Add(order);

            await dbContext.SaveChangesAsync();
        }
    }
}
