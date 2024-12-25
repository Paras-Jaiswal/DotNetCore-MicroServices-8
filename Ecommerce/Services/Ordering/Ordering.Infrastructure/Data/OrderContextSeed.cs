using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContext> logger)
        {
            if (!orderContext.Orders.Any())
            {
                //orderContext.Orders.AddRange(Ge)
            }
        }

        private static IEnumerable<Order> GetOrders()
        {
            return new List<Order> {
                new()
                {
                    UserName = "rahul",
                    FirstName = "Rahul",
                    LastName = "Sahay",
                    EmailAddress = "rahulsahay@eCommerce.net",
                    AddressLine = "Bangalore",
                    Country = "India",
                    TotalPrice = 750,
                    State = "KA",
                    ZipCode = "560001",

                    CardName = "Visa",
                    CardNumber = "1234567890123456",
                    CreatedBy = "Rahul",
                    Expiration = "12/25",
                    Cvv = "123",
                    PaymentMethod = 1,
                    LastModifiedBy = "Rahul",
                    LastModifiedDate = new DateTime(),
                }
            };
        }
    }
}
