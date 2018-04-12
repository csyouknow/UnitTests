using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTestApp.Mocking
{
    public static class BookingHelper
    {

        //injecting an interface into a class using parameters
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository repository)
        {

            if (booking.Status == "Cancelled")
            {
                return string.Empty;
            }

            var bookings = repository.GetActiveBookings(booking.Id);

            var overlappingBooking = bookings.FirstOrDefault(
                b =>
                    booking.ArrivalDate < b.DepartureDate &&
                    b.ArrivalDate < booking.DepartureDate);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;

        }

    }

    public class Booking
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }

    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }
}
