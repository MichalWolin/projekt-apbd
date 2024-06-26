using Api.Data;
using Api.Interfaces;
using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DatabaseContext _context;

    public CustomerRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<CreatedCompanyCustomerDto> CreateCompanyCustomer(NewCompanyCustomerDto newCompanyCustomerDto)
    {
        var customer = new Customer
        {
            Address = newCompanyCustomerDto.Address,
            Email = newCompanyCustomerDto.Email,
            PhoneNumber = newCompanyCustomerDto.PhoneNumber,
            Company = new Company
            {
                Name = newCompanyCustomerDto.Name,
                Krs = newCompanyCustomerDto.Krs
            }
        };

        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        return new CreatedCompanyCustomerDto
        {
            CustomerId = customer.CustomerId,
            Address = customer.Address,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            CompanyId = customer.Company.CompanyId,
            Name = customer.Company.Name,
            Krs = customer.Company.Krs
        };
    }
}