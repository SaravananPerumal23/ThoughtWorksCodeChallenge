using System;
namespace ThoughtWorksCodeChallenge.Model
{
    public class OrderItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public double ItemPrice { get; set; }
        public double DeliveryCharge { get; set; }
    }
}
