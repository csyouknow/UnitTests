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
    class HTMLFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEnloseTheStringWithStrongElement()
        {
            var formatter = new HTMLFormatter();

            var result = formatter.FormatAsBold("abc");

            Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);

            //Using multiple asserts
            //Assert.That(result, Does.StartWith("<strong>"));
            //Assert.That(result, Does.EndWith("</strong>"));
            //Assert.That(result, Does.Contain("abc"));
        }
    }
}
