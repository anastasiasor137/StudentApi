using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private static List<Booking> bookings = new List<Booking>();
        private static int nextId = 1;

        // Бронирование ресурса
        [HttpPost]
        public ActionResult<Booking> CreateBooking([FromBody] Booking booking)
        {
            booking.Id = nextId++;
            bookings.Add(booking);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

        // Получение доступных ресурсов для бронирования
        [HttpGet("available")]
        public ActionResult<List<Booking>> GetAvailableResources(DateTime startDateTime, DateTime endDateTime)
        {
            var availableResources = bookings.Where(b => b.IsActive &&
                (b.EndDateTime <= startDateTime || b.StartDateTime >= endDateTime)).ToList();

            return Ok(availableResources);
        }

        // Получение деталей бронирования по ID
        [HttpGet("{id}")]
        public ActionResult<Booking> GetBooking(int id)
        {
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // Обновление деталей бронирования
        [HttpPut("{id}")]
        public ActionResult UpdateBooking(int id, [FromBody] Booking updatedBooking)
        {
            var existingBooking = bookings.FirstOrDefault(b => b.Id == id);
            if (existingBooking == null)
            {
                return NotFound();
            }

            existingBooking.ResourceType = updatedBooking.ResourceType;
            existingBooking.ResourceName = updatedBooking.ResourceName;
            existingBooking.StartDateTime = updatedBooking.StartDateTime;
            existingBooking.EndDateTime = updatedBooking.EndDateTime;

            return NoContent();
        }

        // Отмена бронирования
        [HttpDelete("{id}")]
        public ActionResult CancelBooking(int id)
        {
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            booking.IsActive = false; // Устанавливаем статус бронирования неактивным
            return NoContent();
        }
    }
}