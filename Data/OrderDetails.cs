using System.Text.Json;

namespace CafeBislerium.Data
{
    internal class OrderDetails
    {
        //Method for saving order details in order.json file
        public static void SaveOrderDetails(List<Order> orders)
        {
            string custOrdersFile = Utils.GetOrdersFile();

            var json = JsonSerializer.Serialize(orders);
            File.WriteAllText(custOrdersFile, json);
        }

        //Method for getting and listing order details from .json file
        public static List<Order> GetOrderDetails()
        {
            string custOrdersFile = Utils.GetOrdersFile();

            if (!File.Exists(custOrdersFile))
            {
                return new List<Order>();
            }

            var json = File.ReadAllText(custOrdersFile);
            var custOrders = JsonSerializer.Deserialize<List<Order>>(json);
            return custOrders;
        }

        //Method for adding items in order in orders.json file
        public static List<Order> AddNewOrders(Order currentOrders)
        {
            List<Order> custOrders = GetOrderDetails();
            bool itemExists = custOrders.Any(x => x.OrderID == currentOrders.OrderID);

            if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
            {
                custOrders.Add(
                    new Order()
                    {
                        OrderID = new Guid(),
                        Items = currentOrders.Items,
                        CustID = currentOrders.CustID,
                        OrderDate = DateTime.Now,
                        TotalPrice = currentOrders.TotalPrice,
                    }
                );
            }
            SaveOrderDetails(custOrders);
            return custOrders;
        }

        //Method for getting order history from orders.json file and listing according to customer ID
        public static List<Billings> GetOrderByCustId(long custID)
        {
            string custOrdersFile = Utils.GetOrdersFile();

            if (!File.Exists(custOrdersFile))
            {
                return new List<Billings>();
            }

            var json = File.ReadAllText(custOrdersFile);
            var custOrders = JsonSerializer.Deserialize<List<Order>>(json);

            var custOrderDetail = custOrders.Where(x => x.CustID == custID && x.OrderDate.Month == DateTime.Now.Month);

            var dataCount = custOrderDetail.Count();
            var data = new List<Billings>();

            foreach (var ordersCust in custOrderDetail)
            {
                data.Add(new Billings()
                {
                    CustID = ordersCust.CustID,
                    OrderDate = ordersCust.OrderDate
                    ,
                });
            }
            return data;
        }
    }
}
