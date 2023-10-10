namespace Efentityorderworker;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }


    private Order()
    {
        // For EF Core
    }

    public Order(User user)
    {
        UserId = user.Id;
    }
}
