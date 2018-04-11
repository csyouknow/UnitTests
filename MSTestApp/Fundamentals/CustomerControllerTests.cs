using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSTestApp.Fundamentals;
using NUnit.Framework;

namespace MSTestApp.Fundamentals
{
    [TestFixture]
    class CustomerControllerTests
    {

        private CustomerController _customerController { get; set; }

        [SetUp]
        public void SetUp()
        {
            _customerController = new CustomerController();
        }

        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            var result = _customerController.GetCustomer(0);

            //NotFound object
            Assert.That(result, Is.TypeOf<NotFound>());

            //NotFound object or one of its derivatives
            //Assert.That(result, Is.InstanceOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {

            var result = _customerController.GetCustomer(1);

            Assert.That(result, Is.TypeOf<Ok>());

        }
    }
}
