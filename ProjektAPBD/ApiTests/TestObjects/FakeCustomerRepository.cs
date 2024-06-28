using Api.Interfaces;
using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;

namespace ApiTests.TestObjects;

public class FakeCustomerRepository : ICustomerRepository
{
    private List<Customer> _customers;

    public FakeCustomerRepository()
    {
        _customers = new List<Customer>
        {
            new Customer
            {
                CustomerId = 1,
                Address = "Address",
                Email = "email@email.com",
                PhoneNumber = "123456789",
                IsDeleted = false,
                CompanyId = 1,
                Company = new Company
                {
                    CompanyId = 1,
                    Name = "Name",
                    Krs = "1234567890"
                }
            },
            new Customer
            {
                CustomerId = 2,
                Address = "Address",
                Email = "email@email.com",
                PhoneNumber = "123456789",
                IsDeleted = false,
                PersonId = 1
            }
        };
    }
    public Task<CompanyCustomerDto> CreateCompanyCustomer(NewCompanyCustomerDto newCompanyCustomerDto,
        CancellationToken cancellationToken)
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

        _customers.Add(customer);

        return Task.FromResult(new CompanyCustomerDto
        {
            CustomerId = customer.CustomerId,
            Address = customer.Address,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            CompanyId = customer.Company.CompanyId,
            Name = customer.Company.Name,
            Krs = customer.Company.Krs
        });
    }

    public Task<Customer?> GetCustomer(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_customers.FirstOrDefault(c => c.CustomerId.Equals(id)));
    }

    public Task<CompanyCustomerDto> UpdateCompanyCustomer(int id, UpdateCustomerCompanyDto updateCustomerCompanyDto,
        CancellationToken cancellationToken)
    {
        var customer = _customers.FirstOrDefault(c => c.CustomerId.Equals(id));

        if (updateCustomerCompanyDto.Address is not null)
            customer!.Address = updateCustomerCompanyDto.Address;
        if (updateCustomerCompanyDto.Email is not null)
            customer!.Email = updateCustomerCompanyDto.Email;
        if (updateCustomerCompanyDto.PhoneNumber is not null)
            customer!.PhoneNumber = updateCustomerCompanyDto.PhoneNumber;
        if  (updateCustomerCompanyDto.Name is not null)
            customer!.Company!.Name = updateCustomerCompanyDto.Name;

        _customers.Remove(customer!);
        _customers.Add(customer!);

        return Task.FromResult(new CompanyCustomerDto
        {
            CustomerId = customer!.CustomerId,
            Address = customer!.Address,
            Email = customer!.Email,
            PhoneNumber = customer!.PhoneNumber,
            CompanyId = customer!.Company!.CompanyId,
            Name = customer!.Company!.Name,
            Krs = customer!.Company!.Krs
        });
    }

    public Task<PersonCustomerDto> CreatePersonCustomer(NewPersonCustomerDto newPersonCustomerDto,
        CancellationToken cancellationToken)
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

        _customers.Add(customer);

        return Task.FromResult(new PersonCustomerDto
        {
            CustomerId = customer.CustomerId,
            Address = customer.Address,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Pesel = customer.Person.Pesel,
            FirstName = customer.Person.FirstName,
            LastName = customer.Person.LastName
        });
    }

    public Task DeletePersonCustomer(Customer customer, CancellationToken cancellationToken)
    {
        _customers.Remove(customer);
        return Task.CompletedTask;
    }

    public Task<PersonCustomerDto> UpdatePersonCustomer(int id, UpdatePersonCustomerDto updatePersonCustomerDto,
        CancellationToken cancellationToken)
    {
        var customer = _customers.FirstOrDefault(c => c.CustomerId.Equals(id));

        if (updatePersonCustomerDto.Address is not null)
            customer!.Address = updatePersonCustomerDto.Address;
        if (updatePersonCustomerDto.Email is not null)
            customer!.Email = updatePersonCustomerDto.Email;
        if (updatePersonCustomerDto.PhoneNumber is not null)
            customer!.PhoneNumber = updatePersonCustomerDto.PhoneNumber;
        if (updatePersonCustomerDto.FirstName is not null)
            customer!.Person!.FirstName = updatePersonCustomerDto.FirstName;
        if (updatePersonCustomerDto.LastName is not null)
            customer!.Person!.LastName = updatePersonCustomerDto.LastName;

        _customers.Remove(customer!);
        _customers.Add(customer!);

        return Task.FromResult(new PersonCustomerDto
        {
            CustomerId = customer!.CustomerId,
            Address = customer!.Address,
            Email = customer!.Email,
            PhoneNumber = customer!.PhoneNumber,
            PersonId = customer!.Person!.PersonId,
            FirstName = customer!.Person!.FirstName,
            LastName = customer!.Person!.LastName,
            Pesel = customer!.Person!.Pesel
        });
    }
}