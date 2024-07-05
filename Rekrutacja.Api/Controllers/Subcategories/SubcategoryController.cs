using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rekrutacja.Domain.Entities;
using Rekrutacja.Infrastructure.Repositories;

namespace Rekrutacja.Api.Controllers.Subcategories;

[Route("api/categories/{id:int}/subcategory")]
[ApiController]
public class SubcategoryController : ControllerBase
{
    private readonly IRepository<Subcategory> _repository;

    public SubcategoryController(IRepository<Subcategory> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var subcategories = await _repository.GetAll().ToListAsync();

        return Ok(subcategories);
    }
}