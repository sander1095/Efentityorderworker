﻿namespace Efentityorderworker;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public Order(User user)
    {
        UserId = user.Id;
    }
}
