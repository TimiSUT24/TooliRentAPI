using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRentClassLibrary.Models.DTO;

namespace TooliRent.BLL.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponseDto?> ToolBooking(BookingRequestDto bookingRequest, string userId);
        Task<IEnumerable<BookingDetailedResponseDto?>> GetUserBookingsAsync(string userId);
        Task<bool> CancelBookingAsync(int bookingId, string userId);
        Task<bool> PickUp(int bookingId, string userId);
        Task<ReturnToolResponseDto?> Return(int bookingId, string userId);
        Task<bool> PayLateFee(int bookingId, decimal? amount, string userId);
    }
}
