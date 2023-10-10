namespace Efentityorderworker;

public class Worker : BackgroundService
{
    private readonly MyDbContext _dbContext;

    public Worker(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var user = new User("Test user");
        var order = new Order(user);

        _dbContext.Users.Add(user);
        _dbContext.Orders.Add(order);

        _dbContext.SaveChanges();
    }
}
