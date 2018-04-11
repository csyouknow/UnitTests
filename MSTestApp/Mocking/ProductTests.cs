using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace MSTestApp.Mocking
{
    [TestFixture]
    class ProductTests
    {
        [Test]
        public void GetPrice_GoldCustomer_Apply30PercentDiscount()
        {
            
            var product = new Product{ListPrice = 100};

            var result = product.GetPrice(new Customer {IsGold = true});

            Assert.That(result, Is.EqualTo(70));

        }

        //Using mock objects when not required
        //[Test]
        //public void GetPrice_GoldCustomer_Apply30PercentDiscount2()
        //{

        //    var customer = new Mock<ICustomer>();
        //    customer.Setup(c => c.IsGold).Returns(true);

        //    var product = new Product { ListPrice = 100 };

        //    var result = product.GetPrice(customer.Object);

        //    Assert.That(result, Is.EqualTo(70));

        //}
    }
}
