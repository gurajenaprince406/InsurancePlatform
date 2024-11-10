using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InsurancePlatform.Data;
using InsurancePlatform.Models;
//using InsurancePlatform.Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class MotorRiskController : ControllerBase
{
    private readonly AppDbContext _context;

    public MotorRiskController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/MotorRisk
    [HttpPost]
    public async Task<IActionResult> AddMotorRisk([FromBody] MotorRiskDto motorRiskDto, int quoteId)
    {
        var quote = await _context.Quotes.FindAsync(quoteId);
        if (quote == null)
        {
            return NotFound($"Quote with ID {quoteId} not found.");
        }

        var motorRisk = new MotorRisk
        {
            VehicleMake = motorRiskDto.VehicleMake,
            VehicleModel = motorRiskDto.VehicleModel,
            YearOfManufacture = motorRiskDto.YearOfManufacture,
            SumInsured = motorRiskDto.SumInsured,
            Rate = motorRiskDto.Rate
        };

        _context.MotorRisks.Add(motorRisk);
        await _context.SaveChangesAsync();

        return Ok(motorRisk);
    }

    // PUT: api/MotorRisk/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMotorRisk(int id, [FromBody] MotorRiskDto motorRiskDto)
    {
        var motorRisk = await _context.MotorRisks.FindAsync(id);
        if (motorRisk == null)
        {
            return NotFound();
        }

        motorRisk.VehicleMake = motorRiskDto.VehicleMake;
        motorRisk.VehicleModel = motorRiskDto.VehicleModel;
        motorRisk.YearOfManufacture = motorRiskDto.YearOfManufacture;
        motorRisk.SumInsured = motorRiskDto.SumInsured;
        motorRisk.Rate = motorRiskDto.Rate;

        await _context.SaveChangesAsync();
        return Ok(motorRisk);
    }

    // GET: api/MotorRisk/quote/{quoteId}
    [HttpGet("quote/{quoteId}")]
    public async Task<IActionResult> GetMotorRisks(int quoteId)
    {
        var motorRisks = await _context.MotorRisks
            .Where(m => m.QuoteId == quoteId)
            .ToListAsync();

        if (!motorRisks.Any())
        {
            return NotFound($"No motor risks found for Quote ID {quoteId}.");
        }

        return Ok(motorRisks);
    }
}
