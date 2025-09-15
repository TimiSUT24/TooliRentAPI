using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.BLL.Services.Interfaces;
using TooliRent.DAL.Repositories.Interfaces;
using TooliRentClassLibrary.Models.DTO;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.BLL.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IToolRepository _toolRepository;
        public BookingService(IBookingRepository bookingRepository, IMapper mapper, IToolRepository toolService)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _toolRepository = toolService;
        }

        public async Task<BookingResponseDto?> ToolBooking(BookingRequestDto bookingRequest, string userId)
        {
            
            var userBookings = await _bookingRepository.FindAsync(b => b.UserId == userId && b.Status == BookingStatus.Pending || b.Status == BookingStatus.Active);
            if (userBookings.Count() >= 5)
            {
                throw new InvalidOperationException("User cannot have more than bookings.");
            }

            var tool = await _toolRepository.GetByNameAsync(bookingRequest.ToolName);

            if (tool == null)
            {
                throw new KeyNotFoundException($"Tool with name '{bookingRequest.ToolName}' not found.");
            }

            var availableItem = tool.ToolItems.FirstOrDefault(ti => ti.Status == ToolStatus.Available);

            if(availableItem == null)
            {
                throw new InvalidOperationException($"No available items for tool '{bookingRequest.ToolName}'.");
            }

            availableItem.Status = ToolStatus.Borrowed;

            var booking = new Booking
            {          
                UserId = userId,
                StartDate = bookingRequest.StartDate,
                EndDate = bookingRequest.EndDate,
                Status = BookingStatus.Pending,
                ToolItems = new List<ToolItem> { availableItem }
            };

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            return _mapper.Map<BookingResponseDto?>(booking);
        }

        public async Task<IEnumerable<BookingDetailedResponseDto?>> GetUserBookingsAsync(string userId)
        {
            var bookings = await _bookingRepository.GetUserBookingsAsync(userId);
            if (bookings == null || !bookings.Any())
            {
                throw new KeyNotFoundException("No bookings found for the user.");
            }

            return _mapper.Map<IEnumerable<BookingDetailedResponseDto?>>(bookings);
        }

        public async Task<bool> CancelBookingAsync(int bookingId, string userId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId, userId);
            if (booking == null)
            {
                throw new KeyNotFoundException($"Booking with ID '{bookingId}' not found for the user.");
            }

            if (booking.Status != BookingStatus.Pending)
            {
                throw new InvalidOperationException("Only pending bookings can be cancelled.");
            }

            booking.Status = BookingStatus.Cancelled;
            foreach (var item in booking.ToolItems)
            {
                item.Status = ToolStatus.Available;
            }

            if(booking.Status == BookingStatus.Cancelled)
            {
                await _bookingRepository.DeleteAsync(booking);               
            }

            await _bookingRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PickUp(int bookingId, string userId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId, userId);
            if (booking == null)
            {
                throw new KeyNotFoundException($"Booking with ID '{bookingId}' not found for the user.");
            }

            if (booking.Status != BookingStatus.Pending)
            {
                throw new InvalidOperationException("Only pending bookings can be picked up.");
            }

            booking.Status = BookingStatus.Active;
            await _bookingRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Return(int bookingId, string userId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId, userId);

            if (booking == null)
            {
                throw new KeyNotFoundException($"Booking with ID '{bookingId}' not found for the user.");
            }

            if (booking.Status != BookingStatus.Active)
            {
                throw new InvalidOperationException("Only active bookings can be returned.");
            }

            //write check when tool is returned late
            if(DateTime.UtcNow > booking.EndDate)
            {
                
            }


            booking.Status = BookingStatus.Completed;

            if(booking.Status != BookingStatus.Completed)
            {
                throw new InvalidOperationException("Booking status could not be updated to Completed.");
            }

            await _bookingRepository.SaveChangesAsync();
            return true;
        }
    }
}
