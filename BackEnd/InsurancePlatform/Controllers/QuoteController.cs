using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using InsurancePlatform.Data;
using InsurancePlatform.Models;
//using InsurancePlatform.Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class QuoteController : ControllerBase
{
    private readonly AppDbContext _context;

    public QuoteController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/Quote
    [HttpPost]
    public async Task<IActionResult> CreateQuote([FromBody] QuoteDto quoteDto)
    {
        var quote = new Quote
        {
            CoverStartDate = quoteDto.CoverStartDate,
            CoverEndDate = quoteDto.CoverEndDate,
            PolicyType = quoteDto.PolicyType,
            Branch = quoteDto.Branch,
            QuoteNumber = GenerateQuoteNumber()
        };

        _context.Quotes.Add(quote);
        await _context.SaveChangesAsync();

        return Ok(quote);
    }

    // PUT: api/Quote/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuote(int id, [FromBody] QuoteDto quoteDto)
    {
        var quote = await _context.Quotes.FindAsync(id);
        if (quote == null)
        {
            return NotFound();
        }

        quote.CoverStartDate = quoteDto.CoverStartDate;
        quote.CoverEndDate = quoteDto.CoverEndDate;
        quote.PolicyType = quoteDto.PolicyType;
        quote.Branch = quoteDto.Branch;

        await _context.SaveChangesAsync();
        return Ok(quote);
    }

    // GET: api/Quote
    [HttpGet]
    public async Task<IActionResult> GetQuotes()
    {
        var quotes = await _context.Quotes.ToListAsync();
        return Ok(quotes);
    }

    // Method to generate a custom quote number
    private string GenerateQuoteNumber()
    {
        return $"QT-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
    }
}

