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
    class DemeritPointsCalculatorTests
    {

        private DemeritPointsCalculator _demeritPointsCalculator { get; set; }

        [SetUp]
        public void SetUp()
        {
            _demeritPointsCalculator = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(64, 0)]
        [TestCase(65, 0)]
        [TestCase(66, 0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoints(int input, int output)
        {
            var result = _demeritPointsCalculator.CalculateDemeritPoints(input);

            Assert.That(result, Is.EqualTo(output));
        }

        [Test]
        public void CalculateDemeritPoints_WhenSpeedIsLessThanZero_ReturnException()
        {
            Assert.That(() => _demeritPointsCalculator.CalculateDemeritPoints(-1),
                Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

    }
}
