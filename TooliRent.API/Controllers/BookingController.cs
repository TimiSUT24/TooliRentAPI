using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TooliRent.BLL.Services.Interfaces;
using TooliRentClassLibrary.Models.DTO;

namespace TooliRent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [Authorize(Roles = "User")]
        [HttpPost("create-booking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDto bookingRequest)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get the user id stored in the token 
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found in token.");
                }

                var bookingResponse = await _bookingService.ToolBooking(bookingRequest, userId);
                if (bookingResponse == null)
                {
                    return NotFound("Booking could not be created.");
                }
                return Ok(bookingResponse);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while creating the booking: {ex.Message}");
            }
        }
    }
}
