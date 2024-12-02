using System;
using System.Collections.Generic;


namespace RestaurantManagementSystem
{

    
    public class Order
    {


        public string Name { get; set;}
        public decimal Price { get; set;}
        public int Quantity { get; set;}

        public Order (string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        

    }

    public enum OrderStatus
    {
        ToGo,
        DineIn,
        Reserved
    }


    public class OrderDetails
    {
        public List<Order> Orders { get; set; } = new List<Order>();
        public decimal Bill { get; set; }
        public OrderStatus Status { get; set; }
        public bool Pay { get; set; }

        

        public OrderDetails (List<Order> orders, OrderStatus status, decimal bill, bool pay)
        {
           Orders = orders;
           Status = status;
           Bill = bill;
           Pay = pay;
        }
    }

    
}
