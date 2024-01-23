using FormulaAirline.Api.Models;
using FormulaAirline.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingsController : ControllerBase
{
    private readonly ILogger<BookingsController> _logger;

    private readonly IMessageProducer _messageProducer;

    private static List<Booking> _bookings;
    public BookingsController(
        ILogger<BookingsController> logger, 
        IMessageProducer messageProducer)
    {
        _logger = logger;
        _messageProducer = messageProducer;
        _bookings = new();
    }

    [HttpPost]
    public IActionResult CreatingBooking(Booking newBooking)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        _bookings.Add(newBooking);
        
        _messageProducer.SendingMessage(newBooking);

        return Ok();
    }
}