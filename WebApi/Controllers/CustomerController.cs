using Microsoft.AspNetCore.Mvc;
using WebApi.ControllerOperations.CustomerOperations;
using WebApi.Dtos.CustomerDtos.PostCustomerDtos;
using WebApi.Dtos.CustomerDtos.PutCustomerDtos;

namespace WebApi.Controller;

[ApiController]
[Route("[controller]s")]
public class CustomerController : ControllerBase
{
    readonly CustomerOperation _operation;

    public CustomerController(CustomerOperation operation)
    {
        _operation = operation;
    }

    [HttpGet]
    public IActionResult GetCustomersQuery()
    {
        return Ok(_operation.GetCustomersQuery());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetCustomerCommandById(int id)
    {
        return Ok(_operation.GetCustomerCommandById(id));
    }

    [HttpPost]
    public IActionResult AddCustomerCommand([FromBody] AddCustomerDto addCustomerDto)
    {
        _operation.AddCustomerCommand(addCustomerDto);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateCustomerCommand(int id, [FromBody] UpdateCustomerDto updateCustomerDto)
    {
        _operation.UpdateCustomerCommand(id, updateCustomerDto);
        return Ok();
    }
}