using Api.Data;
using Api.Exceptions;
using Api.Interfaces;
using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DatabaseContext _context;

    public CustomerRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<CompanyCustomerDto> CreateCompanyCustomer(NewCompanyCustomerDto newCompanyCustomerDto)
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

        return new CompanyCustomerDto
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

    public async Task<Customer?> GetCustomer(int id)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId.Equals(id));
    }

    public async Task<CompanyCustomerDto> UpdateCompanyCustomer(int id,
                                                                UpdateCustomerCompanyDto updateCustomerCompanyDto)
    {
        var customer = await _context.Customers
            .Include(c => c.Company)
            .FirstOrDefaultAsync(c => c.CustomerId.Equals(id));

        if (updateCustomerCompanyDto.Address is not null)
            customer!.Address = updateCustomerCompanyDto.Address;
        if (updateCustomerCompanyDto.Email is not null)
            customer!.Email = updateCustomerCompanyDto.Email;
        if (updateCustomerCompanyDto.PhoneNumber is not null)
            customer!.PhoneNumber = updateCustomerCompanyDto.PhoneNumber;
        if  (updateCustomerCompanyDto.Name is not null)
            customer!.Company!.Name = updateCustomerCompanyDto.Name;

        await _context.SaveChangesAsync();

        return new CompanyCustomerDto
        {
            CustomerId = customer!.CustomerId,
            Address = customer!.Address,
            Email = customer!.Email,
            PhoneNumber = customer!.PhoneNumber,
            CompanyId = customer!.Company!.CompanyId,
            Name = customer!.Company!.Name,
            Krs = customer!.Company!.Krs
        };
    }

    public async Task<PersonCustomerDto> CreatePersonCustomer(NewPersonCustomerDto newPersonCustomerDto)
    {
        var customer = new Customer
        {
            Address = newPersonCustomerDto.Address,
            Email = newPersonCustomerDto.Email,
            PhoneNumber = newPersonCustomerDto.PhoneNumber,
            Person = new Person
            {
                Pesel = newPersonCustomerDto.Pesel,
                FirstName = newPersonCustomerDto.FirstName,
                LastName = newPersonCustomerDto.LastName
            }
        };

        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        return new PersonCustomerDto
        {
            CustomerId = customer.CustomerId,
            Address = customer.Address,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            PersonId = customer.Person.PersonId,
            Pesel = customer.Person.Pesel,
            FirstName = customer.Person.FirstName,
            LastName = customer.Person.LastName
        };
    }

    public Task DeletePersonCustomer(Customer customer)
    {
        customer.IsDeleted = true;
        return _context.SaveChangesAsync();
    }
}