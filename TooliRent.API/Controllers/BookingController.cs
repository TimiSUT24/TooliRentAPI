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

        [Authorize(Roles = "User")]
        [HttpGet("user-bookings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserBookings()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get the user id stored in the token 
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found in token.");
                }

                var bookings = await _bookingService.GetUserBookingsAsync(userId);
                if (bookings == null || !bookings.Any())
                {
                    return NotFound("No bookings found for the user.");
                }
                return Ok(bookings);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while retrieving bookings: {ex.Message}");
            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete("cancel-booking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelBooking([FromQuery] int bookingId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get the user id stored in the token 
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found in token.");
                }

                var result = await _bookingService.CancelBookingAsync(bookingId, userId);
                if (!result)
                {
                    return NotFound("Booking could not be found or cancelled.");
                }

                return Ok("Booking cancelled successfully.");
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
                return BadRequest($"An error occurred while cancelling the booking: {ex.Message}");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPut("pick-up")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PickUpTool([FromQuery] int bookingId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get the user id stored in the token 
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found in token.");
                }

                var result = await _bookingService.PickUp(bookingId, userId);
                if (!result)
                {
                    return NotFound("Booking could not be found or updated.");
                }
                return Ok("Tool picked up successfully.");
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
                return BadRequest($"An error occurred while updating the booking: {ex.Message}");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPut("return")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReturnTool([FromQuery] int bookingId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //Get the user id stored in the token 
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found in token.");
                }

                var result = await _bookingService.Return(bookingId, userId);

                if (!result)
                {
                    return NotFound("Booking could not be found or updated.");
                }
                return Ok("Tool returned successfully.");
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
                return BadRequest($"An error occurred while updating the booking: {ex.Message}");
            }
        }

    }
}
