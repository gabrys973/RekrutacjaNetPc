using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rekrutacja.Domain.Entities;
using Rekrutacja.Infrastructure.Repositories;

namespace Rekrutacja.Api.Controllers.Categories;

[Route("api/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IRepository<Category> _repository;

    public CategoryController(IRepository<Category> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await _repository.GetAll().ToListAsync();

        return Ok(categories);
    }
}