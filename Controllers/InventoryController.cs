using AutoMapper;
using Inveny.Models;
using Inveny.Services;
using Inveny.Collections;
using Microsoft.AspNetCore.Mvc;
using Inveny.Responses;
using Inveny.Requests;
using Microsoft.EntityFrameworkCore;

namespace Inveny.Controllers;

[Route("api/inventories")]
[ApiController]
public class InventoryController : Controller
{
  private const string? nullValue = (string)null;
  private readonly IInventoryService _inventoryService;
  private readonly IMapper _mapper;

  public InventoryController(IInventoryService inventoryService, IMapper mapper)
  {
    _mapper = mapper;
    _inventoryService = inventoryService;
  }

  [HttpGet]
  [ProducesResponseType(200, Type = typeof(IEnumerable<Inventory>))]
  public IActionResult Index(int page = 1, int limit = 10, string name = "")
  {
    IQueryable<Inventory> inventories = _inventoryService.GetInventories().AsQueryable();

    if (!string.IsNullOrEmpty(name))
    {
      inventories = inventories.Where(s => s.Name.Contains(name));
    }

    var total = inventories.Count();
    var skip = (page - 1) * limit;
    inventories = inventories.Skip(skip).Take(limit);
    var totalPage = (int)Math.Ceiling((double)total / limit);

    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var response = new Dictionary<string, List<InventoryCollection>>();
    // response["inventory"] = _mapper.Map<List<InventoryCollection>>(inventories.Include(s => s.Supplier).ToList());
    response["inventory"] = _mapper.Map<List<InventoryCollection>>(inventories.Include(s => s.Supplier).ToList());


    return Ok(new ResponseListing<InventoryCollection>(response, true, 200, "Data Retrieved", total, limit, page, totalPage));
  }

  [HttpGet("{id}")]
  [ProducesResponseType(404)]
  [ProducesResponseType(200, Type = typeof(Inventory))]
  [ProducesResponseType(400)]
  public IActionResult Read(int id)
  {
    if(!_inventoryService.IsInventoryExist(id))
    {
      return NotFound(new ResponseNotFound(false, "Data inventory not found", 404, new Inventory()));
    }

    var inventory = _inventoryService.GetInventory(id);

    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    return Ok(new { success = true, status = 200, data = inventory });
  }

  [HttpPost]
  [ProducesResponseType(204)]
  [ProducesResponseType(400)]
  [ProducesResponseType(422)]

  public IActionResult Create([FromBody] InventoryRequest request)
  {
    if (request is null)
    {
      return BadRequest(ModelState);
    }

    var existingInventory = _inventoryService.GetInventories().Where(x => x.Name.Trim().ToLower() == request.Name ).SingleOrDefault();

    if (existingInventory != null)
    {
      ModelState.AddModelError("payload", "Inventory already exists in database");
      return StatusCode(422, ModelState);
    }

    if(!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var result = _mapper.Map<Inventory>(request);

    if (!_inventoryService.CreateInventory(result))
    {
      ModelState.AddModelError("payload", "Something wrong while saving");
      return StatusCode(500, ModelState);
    }

    return Ok(new { status = 200, success = true, message = "Inventory created successfully" });
  }

  [HttpPut("{id}")]
  [ProducesResponseType(422)]
  [ProducesResponseType(404)]
  [ProducesResponseType(204)]
  public IActionResult Update(int id, InventoryRequest request)
  {
    if (request is null)
    {
      return BadRequest(ModelState);
    }

    if (!_inventoryService.IsInventoryExist(id))
    {
      return NotFound(new ResponseNotFound(false, "Data inventory not found", 404, new Inventory()));
    }

    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var inventory = _inventoryService.GetInventory(id);
    _mapper.Map(request, inventory);

    if (!_inventoryService.UpdateInventory(inventory))
    {
      ModelState.AddModelError("payload", "Something went wrong while updating");
      return StatusCode(500, ModelState);
    }

    return NoContent();
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(200)]
  [ProducesResponseType(500)]
  [ProducesResponseType(404)]
  public IActionResult Destroy(int id)
  {
    if (!_inventoryService.IsInventoryExist(id))
    {
      return NotFound(new ResponseNotFound(false, "Data inventory not found", 404, new Inventory()));
    }

    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var inventory = _inventoryService.GetInventory(id);
    
    if (!_inventoryService.DeleteInventory(inventory))
    {
      ModelState.AddModelError("", "Something went wrong deleting province");
      return StatusCode(500, ModelState);
    }

    return Ok(new { status = 200, success = true, data = nullValue });

  }
}