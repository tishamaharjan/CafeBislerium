using System.Text.Json;

namespace CafeBislerium.Data
{
    public static class CustomerDetails
    {
        public static List<Customer> GetCustDetails()//getting customer details
        {
            string custFile = Utils.GetCustFile();

            if (!File.Exists(custFile))
            {
                return new List<Customer>();
            }

            var jsonCust = File.ReadAllText(custFile);
            var Cust = JsonSerializer.Deserialize<List<Customer>>(jsonCust);
            return Cust;
        }

        public static void CustDetails(List<Customer> customers)//for saving customer details
        {
            string custFile = Utils.GetCustFile();

            var jsonCust = JsonSerializer.Serialize(customers);
            File.WriteAllText(custFile, jsonCust);
        }

        public static List<Customer> AddMembership(long custId)
        {
            List<Customer> customers = GetCustDetails();
            bool membershipExists = customers.Any(x => x.CustID == custId);

            if (!membershipExists)
            {
                customers.Add(new Customer()
                {
                    CustID = custId,
                    OrderNumbers = 1
                });
            }
            CustDetails(customers);
            return customers;
        }

        public static List<Customer> CustOrdersCount(long custID)
        {
            List<Customer> customers = GetCustDetails();
            Customer customer = customers.FirstOrDefault(members => members.CustID == custID);

            if (customer == null)
            {
                AddMembership(custID);
            }
            else
            {
                customer.OrderNumbers += 1;
            }
            CustDetails(customers);
            return customers;
        }

        public static List<Customer> CustGiveDiscount(long custID)
        {
            List<Customer> customers = GetCustDetails();
            Customer customer = customers.FirstOrDefault(customers => customers.CustID == custID);

            if (customer != null)
            {
                int x = OrderDetails.GetOrderByCustId(custID).Count;
                if (x % 10 == 0)
                {
                    customer.Discount = true;
                }
                else
                {
                    customer.Discount = false;
                }
            }
            CustDetails(customers);
            return customers;
        }
    }
}
