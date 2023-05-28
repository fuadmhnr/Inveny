using AutoMapper;
using Inveny.Models;
using Inveny.Services;
using Inveny.Collections;
using Microsoft.AspNetCore.Mvc;
using Inveny.Responses;
using Inveny.Requests;

namespace Inveny.Controllers;

[Route("api/suppliers")]
[ApiController]
public class SupplierController : Controller
{
  private const string? nullValue = (string)null;
  private readonly ISupplierService _supplierService;
  private readonly IMapper _mapper;

  public SupplierController(ISupplierService supplierService, IMapper mapper)
  {
    _mapper = mapper;
    _supplierService = supplierService;
  }

  [HttpGet]
  [ProducesResponseType(200, Type = typeof(IEnumerable<Supplier>))]
  public IActionResult Index(int page = 1, int limit = 10, string name = "")
  {
    IQueryable<Supplier> suppliers = _supplierService.GetSuppliers().AsQueryable();

    if (!string.IsNullOrEmpty(name))
    {
      suppliers = suppliers.Where(s => s.Name.Contains(name));
    }

    var total = suppliers.Count();
    var skip = (page - 1) * limit;
    suppliers = suppliers.Skip(skip).Take(limit);
    var totalPage = (int)Math.Ceiling((double)total / limit);

    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var response = new Dictionary<string, List<SupplierCollections>>();
    response["supplier"] = _mapper.Map<List<SupplierCollections>>(suppliers.ToList());


    return Ok(new ResponseListing<SupplierCollections>(response, true, 200, "Data Retrieved", total, limit, page, totalPage));
  }

  [HttpGet("{id}")]
  [ProducesResponseType(404)]
  [ProducesResponseType(200, Type = typeof(Supplier))]
  [ProducesResponseType(400)]
  public IActionResult Read(int id)
  {
    if(!_supplierService.IsSupplierExist(id))
    {
      return NotFound(new ResponseNotFound(false, "Data supplier not found", 404, new Supplier()));
    }

    var supplier = _supplierService.GetSupplier(id);

    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    return Ok(new { success = true, status = 200, data = supplier });
  }

  [HttpPost]
  [ProducesResponseType(204)]
  [ProducesResponseType(400)]
  [ProducesResponseType(422)]

  public IActionResult Create([FromBody] SupplierRequest request)
  {
    if (request is null)
    {
      return BadRequest(ModelState);
    }

    var existingSupplier = _supplierService.GetSuppliers().Where(x => x.Name.Trim().ToLower() == request.Name && x.Phone == request.Phone).SingleOrDefault();

    if (existingSupplier != null)
    {
      ModelState.AddModelError("payload", "Supplier already exists in database");
      return StatusCode(422, ModelState);
    }

    if(!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var result = _mapper.Map<Supplier>(request);

    if (!_supplierService.CreateSupplier(result))
    {
      ModelState.AddModelError("payload", "Something wrong while saving");
      return StatusCode(500, ModelState);
    }

    return Ok(new { status = 200, success = true, message = "Supplier created successfully" });
  }

  [HttpPut("{id}")]
  [ProducesResponseType(422)]
  [ProducesResponseType(404)]
  [ProducesResponseType(204)]
  public IActionResult Update(int id, SupplierRequest request)
  {
    if (request is null)
    {
      return BadRequest(ModelState);
    }

    if (!_supplierService.IsSupplierExist(id))
    {
      return NotFound(new ResponseNotFound(false, "Data supplier not found", 404, new Supplier()));
    }

    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var supplier = _supplierService.GetSupplier(id);
    _mapper.Map(request, supplier);

    if (!_supplierService.UpdateSupplier(supplier))
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
    if (!_supplierService.IsSupplierExist(id))
    {
      return NotFound(new ResponseNotFound(false, "Data supplier not found", 404, new Supplier()));
    }

    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var supplier = _supplierService.GetSupplier(id);
    
    if (!_supplierService.DeleteSupplier(supplier))
    {
      ModelState.AddModelError("", "Something went wrong deleting province");
      return StatusCode(500, ModelState);
    }

    return Ok(new { status = 200, success = true, data = nullValue });

  }
}