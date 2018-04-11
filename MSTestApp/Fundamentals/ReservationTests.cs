using System;
using MSTestApp.Fundamentals;
using NUnit.Framework;

namespace MSTestApp.Fundamentals
{
    [TestFixture]
    public class ReservationTests
    {
        [Test]
        //NameOfMethod_Scenario_ExpectedBehaviour
        public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
        {

            //Arrange
            var reservation = new Reservation();
            
            //Act
            var result = reservation.CanBeCancelledBy(new Reservation.User {IsAdmin = true});
            
            //Assert
            //Assert.IsTrue(result);
            //Assert.That(result, Is.True);
            Assert.That(result == true);
        }

        [Test]
        public void CanBeCancelledBy_SameUserCancelling_ReturnTrue()
        {
            var user = new Reservation.User();
            var reservation = new Reservation{MadeBy = user};

            var result = reservation.CanBeCancelledBy(user);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUserCancelling_ReturnFalse()
        {
            var reservation = new Reservation{MadeBy = new Reservation.User()};

            var result = reservation.CanBeCancelledBy(new Reservation.User());

            Assert.That(result, Is.False);
        }

    }
}
