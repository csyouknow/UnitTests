using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTestApp.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }

    public class BookingRepository : IBookingRepository
    {

        //optional parameter to pass in to the method
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings = unitOfWork.Query<Booking>().Where(b => b.Status != "Cancelled");

            if (excludedBookingId.HasValue)
                bookings = bookings.Where(b => b.Id != excludedBookingId.Value);

            return bookings;
        }

    }
}
