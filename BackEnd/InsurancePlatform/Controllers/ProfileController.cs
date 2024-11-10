
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InsurancePlatform.Data;
using InsurancePlatform.Models;
//using InsurancePlatform.Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProfileController(AppDbContext context)
    {
        _context = context;
    }

    // PUT: api/Profile/{id}/details
    [HttpPut("{id}/details")]
    public async Task<IActionResult> UpdatePersonalDetails(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        user.Title = updateUserDto.Title;
        user.Name = updateUserDto.Name;
        user.Surname = updateUserDto.Surname;
        user.PrimaryEmail = updateUserDto.PrimaryEmail;

        await _context.SaveChangesAsync();
        return Ok(user);
    }

    // POST: api/Profile/{id}/address
    [HttpPost("{id}/address")]
    public async Task<IActionResult> AddAddress(int id, [FromBody] AddressDto addressDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        var address = new Address
        {
            Street = addressDto.Street,
            City = addressDto.City,
            State = addressDto.State,
            ZipCode = addressDto.ZipCode,
            Country = addressDto.Country,
            UserId = id
        };

        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
        return Ok(address);
    }

    // DELETE: api/Profile/address/{addressId}
    [HttpDelete("address/{addressId}")]
    public async Task<IActionResult> DeleteAddress(int addressId)
    {
        var address = await _context.Addresses.FindAsync(addressId);
        if (address == null) return NotFound();

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
        return Ok();
    }

    // POST: api/Profile/{id}/phone
    [HttpPost("{id}/phone")]
    public async Task<IActionResult> AddPhoneNumber(int id, [FromBody] PhoneNumberDto phoneNumberDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        var phoneNumber = new PhoneNumber
        {
            Number = phoneNumberDto.Number,
            UserId = id
        };

        _context.PhoneNumbers.Add(phoneNumber);
        await _context.SaveChangesAsync();
        return Ok(phoneNumber);
    }

    // DELETE: api/Profile/phone/{phoneId}
    [HttpDelete("phone/{phoneId}")]
    public async Task<IActionResult> DeletePhoneNumber(int phoneId)
    {
        var phoneNumber = await _context.PhoneNumbers.FindAsync(phoneId);
        if (phoneNumber == null) return NotFound();

        _context.PhoneNumbers.Remove(phoneNumber);
        await _context.SaveChangesAsync();
        return Ok();
    }

    // POST: api/Profile/{id}/email
    [HttpPost("{id}/email")]
    public async Task<IActionResult> AddEmail(int id, [FromBody] EmailDto emailDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        var email = new Email
        {
            EmailAddress = emailDto.EmailAddress,
            UserId = id
        };

        _context.Emails.Add(email);
        await _context.SaveChangesAsync();
        return Ok(email);
    }

    // DELETE: api/Profile/email/{emailId}
    [HttpDelete("email/{emailId}")]
    public async Task<IActionResult> DeleteEmail(int emailId)
    {
        var email = await _context.Emails.FindAsync(emailId);
        if (email == null) return NotFound();

        _context.Emails.Remove(email);
        await _context.SaveChangesAsync();
        return Ok();
    }
}

