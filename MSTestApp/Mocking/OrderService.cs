using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTestApp.Mocking
{
    class OrderService
    {

        private readonly IStorage _storage;

        public OrderService(IStorage storage)
        {
            _storage = storage;
        }

        public int PlaceOrder(Order order)
        {
            var orderId = _storage.Store(order);

            return orderId;
        }
        
    }

    public interface IStorage
    {
        int Store(Order order);
    }

    public class Storage : IStorage
    {
        public int Store(Order order)
        {
            return 1;
        }
    }

    public class Order
    {
        
    }
}
