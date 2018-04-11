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
    class FizzBuzzTests
    {

        [Test]
        [TestCase(15,"FizzBuzz")]
        [TestCase(3, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(4, "4")]
        public void GetOutput_WhenCalled_CheckOutputsAreCorrect(int input, string output)
        {

            var result = FizzBuzz.GetOutput(input);

            Assert.That(result, Is.EqualTo(output));

        }

    }
}
