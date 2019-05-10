using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using ThoughtWorksCodeChallenge.BAL;
using ThoughtWorksCodeChallenge.Model;

namespace Tests
{
    public class Tests
    {

        CreateOrder objOrder;
        List<Order> ordersPlaced = null;

        string OrdersOutputFilePath = "/Users/saravananperumal/Projects/ThoughtWorksCodeChallenge/ThoughtWorksCodeChallenge/Data/Orders.json";

        [SetUp]
        public void Setup()
        {
            objOrder = new CreateOrder();
            File.Delete(OrdersOutputFilePath);
        }

        [Test(Description = "Test saving single order with 2 items")]
        public void TestSaveMethod()
        {
            try
            {
                var expectedCount = 1;
                objOrder.SaveOrder(1, new List<int> { 1, 2 }, "55441");
                ordersPlaced = objOrder.GetJsonData(OrdersOutputFilePath, new List<Order> { });
                Assert.AreEqual(expectedCount, ordersPlaced.Count);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        [Test(Description = "Test saving 2 orders with 2 items each")]
        public void TestSaveMethodWith2Orders()
        {
            try
            {
                Setup();
                var expectedCount = 2;
                objOrder.SaveOrder(1, new List<int> { 1, 2 }, "55441");
                objOrder.SaveOrder(2, new List<int> { 3, 4 }, "55441");
                ordersPlaced = objOrder.GetJsonData(OrdersOutputFilePath, new List<Order> { });
                Assert.AreEqual(expectedCount, ordersPlaced.Count);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [Test(Description = "Test saving 3rd order with increased delivery charges")]
        public void TestSaveMethodWithHigherDeliveryCharges()
        {
            try
            {
                Setup();
                var expectedCount = 3;
                var expectedDeliveryCharge = 20;
                objOrder.SaveOrder(1, new List<int> { 1, 2 }, "55441");
                objOrder.SaveOrder(2, new List<int> { 3, 4 }, "55441");
                objOrder.SaveOrder(3, new List<int> { 1, 3 }, "55441");
                //objOrder.SaveOrder(1, new List<int> { 1 }, "55441");
                ordersPlaced = objOrder.GetJsonData(OrdersOutputFilePath, new List<Order> { });
                Assert.AreEqual(expectedCount, ordersPlaced.Count);
                Assert.AreEqual(expectedDeliveryCharge, ordersPlaced[2].OrderItems[0].DeliveryCharge);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [Test(Description = "Test saving 3rd order without any increase in delivery charges for different zip code")]
        public void TestSaveMethodWithoutHigherDeliveryCharges()
        {
            try
            {
                Setup();
                var expectedCount = 3;
                var expectedDeliveryCharge = 15;
                objOrder.SaveOrder(1, new List<int> { 1, 2 }, "55441");
                objOrder.SaveOrder(2, new List<int> { 3, 4 }, "55441");
                objOrder.SaveOrder(1, new List<int> { 1 }, "55442");
                ordersPlaced = objOrder.GetJsonData(OrdersOutputFilePath, new List<Order> { });
                Assert.AreEqual(expectedCount, ordersPlaced.Count);
                Assert.AreEqual(expectedDeliveryCharge, ordersPlaced[2].OrderItems[0].DeliveryCharge);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}