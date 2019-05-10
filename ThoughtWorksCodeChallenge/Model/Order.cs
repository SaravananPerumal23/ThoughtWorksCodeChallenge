using System;
using System.Collections.Generic;

namespace ThoughtWorksCodeChallenge.Model
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>(); 
        }

        public int OrderId { get; set; }
        public User UserInfo { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public DateTime CreateTs { get; set; }
        public string ZipCode { get; set; }
        public OrderStatus Status { get;  set; }
    }

    public enum OrderStatus
    { 
        Placed,
        Processing,
        Complete
    }
}