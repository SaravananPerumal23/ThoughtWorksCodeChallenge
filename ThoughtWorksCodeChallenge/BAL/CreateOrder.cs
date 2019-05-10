using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using ThoughtWorksCodeChallenge.Model;

namespace ThoughtWorksCodeChallenge.BAL
{
    public class CreateOrder
    {
        public dynamic GetJsonData(string filePath, dynamic obj)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                return JsonConvert.DeserializeAnonymousType(r.ReadToEnd(), obj);
            }
        }

        public void SaveOrder(int userId, List<int> orderItemIds, string zipCode)
        {
            try
            {
                List<OrderItem> orderItems;
                List<User> users;
                List<Order> ordersOutput = null;
                int maxOrderCount = 0;

                orderItems = GetJsonData(Constants.RootFolderPath + Constants.OrderItemsListFilePath, new List<OrderItem> { });
                users = GetJsonData(Constants.RootFolderPath + Constants.UsersFilePath, new List<User> { });

                if (File.Exists(Constants.RootFolderPath + Constants.OrdersOutputFilePath))
                {
                    ordersOutput = GetJsonData(Constants.RootFolderPath + Constants.OrdersOutputFilePath, new List<Order> { });

                    if (ordersOutput == null)
                        ordersOutput = new List<Order>();

                    maxOrderCount = ordersOutput.Where(x => x.ZipCode.ToString() == zipCode &&
                                        x.CreateTs.AddMinutes(-5) <= DateTime.Now).ToList().Count();
                }

                Order order = new Order();
                foreach (var itemId in orderItemIds)
                {
                    OrderItem orderItem = new OrderItem();

                    orderItem = orderItems.FirstOrDefault(x => x.ItemId == itemId);
                    if (Constants.MaxOrderByZipCode <= maxOrderCount)
                    {
                        orderItem.DeliveryCharge += Constants.AdditionalDeliveryCharge;
                    }

                    order.OrderItems.Add(orderItem);
                }

                order.OrderId = ordersOutput == null ? 1 : ordersOutput.Count + 1;
                order.ZipCode = zipCode;
                order.CreateTs = DateTime.Now;
                order.Status = OrderStatus.Placed;
                order.UserInfo = users.Where(x => x.UserId == userId).FirstOrDefault();

                if (ordersOutput == null)
                    ordersOutput = new List<Order>();

                ordersOutput.Add(order);

                using (StreamWriter myFile = new StreamWriter(Constants.RootFolderPath + Constants.OrdersOutputFilePath))
                {
                    myFile.Write(JsonConvert.SerializeObject(ordersOutput));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
