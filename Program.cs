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

        static void Main(string[] args)
        {
            var LstofCustomer = GetCustomers();

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

        }
    }
}
