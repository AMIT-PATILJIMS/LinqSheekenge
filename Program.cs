using System.Collections.Immutable;

namespace LinqSheekenge
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string ContactName { get; set; }

        public string Country { get; set; }
    }

    public class Shipper
    {
        public int ShipperID { get; set; }

        public string ShipperName { get; set;}
    }

    public class Order
    {
        //OrderID	CustomerID	EmployeeID	OrderDate	ShipperID
        public int OrderID { get; set; }

        public int CustomerID { get; set; }

        public int EmployeeID { get; set; }

        public string OrderDate { get;set; }

        public int ShipperID { get; set; }
    }

    public class Program
    {
        public static IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>()
            {
                new Customer() { CustomerId = 1, CustomerName = "Alfreds Futterkiste", ContactName = "Maria Anders", Country = "Germany" },

                new Customer() { CustomerId = 2, CustomerName = "Ana Trujillo Emparedados y helados", ContactName = "Ana Trujillo", Country = "Mexico" },

                new Customer() { CustomerId = 3, CustomerName = "Antonio Moreno Taquería", ContactName = "Antonio Moreno", Country = "Mexico" },

                new Customer() { CustomerId = 4, CustomerName = "Around the Horn", ContactName = "Thomas Hardy", Country = "UK" },

                new Customer() { CustomerId = 5, CustomerName = "Berglunds snabbköp", ContactName = "Christina Berglund", Country = "Sweden" }
            };
        }

        public static IEnumerable<Shipper> GetShippers() 
        {
            return new List<Shipper>()
            {
                new Shipper(){ ShipperID = 1, ShipperName = "Speedy Express"},
                new Shipper(){ ShipperID = 2, ShipperName = "United Package"},
                new Shipper(){ ShipperID = 3, ShipperName = "Federal Shipping"},
            };
        }

        public static IEnumerable<Order> GetOrders()
        {
            return new List<Order>()
            {
                new Order() { OrderID = 10248, CustomerID = 90, EmployeeID = 5, OrderDate = "1996-07-04", ShipperID = 3 },
                new Order() { OrderID = 10249, CustomerID = 81, EmployeeID = 6, OrderDate = "1996-07-05", ShipperID = 1 },
                new Order() { OrderID = 10250, CustomerID = 34, EmployeeID = 4, OrderDate = "1996-07-08", ShipperID = 2 },
            };
        }

        static void Main(string[] args)
        {
            var LstofCustomer = GetCustomers();

            var LstOfShipper = GetShippers();

            var LstOfOrders = GetOrders();

            /*
             * The following SQL statement lists the number of customers in each country:
             * 
             * SELECT COUNT(CustomerID), Country
               FROM Customers
               GROUP BY Country;
             */

            var results = from customer in LstofCustomer
                          group customer by customer.Country into g
                          select new
                          {
                              id = g.Key,
                              rowinCustomers = g.Select(x => x.CustomerId).Count()
                          };
            /*
            foreach (var result in results)
            {
                Console.WriteLine(result.id + " " + result.rowinCustomers);
            }
            */

            /*
             * Now Sort the result from high to low
             * SELECT COUNT(CustomerId), Country
             * FROM Customers
             * GROUP BY Country
             * ORDER By COUNT(CustomerId) DESC
             */

            var sortedResult = from customer in LstofCustomer
                               group customer by customer.Country into g
                               orderby g.Select(x => x.CustomerId).Count() descending
                               select new
                               {
                                   id = g.Key,
                                   rowinCustomers = g.Select(x => x.CustomerId).Count()
                               };


            foreach (var result in sortedResult)
            {
                Console.WriteLine(result.id + " " + result.rowinCustomers);
            }

            /*
             * SELECT Shippers.ShipperName, COUNT(Orders.OrderID) AS NumberOfOrders FROM Orders
                LEFT JOIN Shippers ON Orders.ShipperID = Shippers.ShipperID
                GROUP BY ShipperName;
             */

            /*
            var newResult = from Order in LstOfOrders//shipperId in LstOfShipper
                            join
                            shipper in LstOfShipper
                            on
                            Order.ShipperID equals shipper.ShipperID
                            group shipper.ShipperName into g
                            orderby g.Select(x => x.sh)
                            select new { 

                            }
            */
        }
    }
}
