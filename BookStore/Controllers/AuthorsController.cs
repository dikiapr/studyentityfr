using BookStore.DTOs.Requests;
using BookStore.DTOs.Responses;
using BookStore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorsController(IAuthorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorResponse>>> GetAll()
    {
        var authors = await _service.GetAllAsync();
        return Ok(authors);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorResponse>> GetById(int id)
    {
        var author = await _service.GetByIdAsync(id);
        return author is null ? NotFound() : Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<AuthorResponse>> Create([FromBody] CreateAuthorRequest request)
    {
        var created = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AuthorResponse>> Update(int id, [FromBody] UpdateAuthorRequest request)
    {
        var updated = await _service.UpdateAsync(id, request);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
