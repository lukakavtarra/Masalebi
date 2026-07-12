using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebAPIApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(_inventoryService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _inventoryService.GetById(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpGet("expensive")]
    public ActionResult<IEnumerable<Product>> GetExpensive([FromQuery] decimal threshold = 100)
    {
        return Ok(_inventoryService.GetByPrice(threshold));
    }

    [HttpPost]
    public ActionResult<Product> Add([FromBody] Product product)
    {
        var created = _inventoryService.Add(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var success = _inventoryService.Delete(id);
        if (!success) return NotFound();
        return NoContent();
    }
}