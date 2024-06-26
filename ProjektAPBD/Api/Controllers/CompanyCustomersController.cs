using Api.Interfaces;
using Api.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyCustomersController : ControllerBase
{
    private readonly ICompanyCustomerService _companyCustomerService;

    public CompanyCustomersController(ICompanyCustomerService companyCustomerService)
    {
        _companyCustomerService = companyCustomerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompanyCustomer(NewCompanyCustomerDto newCompanyCustomerDto)
    {
        return Ok(await _companyCustomerService.CreateCompanyCustomer(newCompanyCustomerDto));
    }
}