using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rekrutacja.Api.Services;
using Rekrutacja.Application.Helpers.Contacts;
using Rekrutacja.Application.Mappers.Contacts;
using Rekrutacja.Application.Requests.Contacts;
using Rekrutacja.Infrastructure.Repositories.Contacts;

namespace Rekrutacja.Api.Controllers.Contacts;

[Route("api/contacts")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactRepository _repository;
    private readonly IPasswordHasher _passwordHasher;

    public ContactController(IContactRepository repository, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryContactHelper query)
    {
        var contacts = await _repository.GetAllContacts(query);

        var contactsDto = contacts.Select(x => x.ToContactDto());

        return Ok(contactsDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var contact = await _repository.GetByIdContact(id);

        if(contact is null)
        {
            return NotFound();
        }

        var contactsDto = contact.ToContactDto();

        return Ok(contactsDto);
    }

    [HttpPost()]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] ContactRequest request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newContact = request.ToContactFromRequests();

        // haszowanie hasła
        newContact.Password = _passwordHasher.Hash(newContact.Password);

        var result = await _repository.CreateAsync(newContact);

        return Ok(new { id = result.Id });
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] ContactRequest request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var contact = request.ToContactFromRequests();

        // haszowanie hasła
        contact.Password = _passwordHasher.Hash(contact.Password);

        var editedContact = await _repository.EditAsync(id, contact);

        if(editedContact is null)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var contact = await _repository.DeleteAsync(id);

        if(contact is null)
        {
            return NotFound();
        }

        return Ok();
    }
}