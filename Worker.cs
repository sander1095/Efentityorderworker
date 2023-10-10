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
// The error is shown below.
// I would expect EF Core to be smart enough to realize that the user must be created BEFORE the blog order in order to get a FK to create the order with.

/*
 * info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (37ms) [Parameters=[@p0='?' (DbType = Int32), @p1='?' (Size = 4000)], CommandType='Text', CommandTimeout='30']
      SET NOCOUNT ON;
      INSERT INTO [Order] ([UserId])
      OUTPUT INSERTED.[Id]
      VALUES (@p0);
      INSERT INTO [User] ([Name])
      OUTPUT INSERTED.[Id]
      VALUES (@p1);
fail: Microsoft.EntityFrameworkCore.Update[10000]
      An exception occurred in the database while saving changes for context type 'Efentityorderworker.MyDbContext'.
      Microsoft.EntityFrameworkCore.DbUpdateException: An error occurred while saving the entity changes. See the inner exception for details.
       ---> Microsoft.Data.SqlClient.SqlException (0x80131904): The INSERT statement conflicted with the FOREIGN KEY constraint "FK_Order_User_UserId". The conflict occurred in database "db-seeumove-test", table "dbo.User", column 'Id'.
         at Microsoft.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
 */
        }
    }
}
