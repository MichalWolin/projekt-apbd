﻿namespace Api.RequestModels;

public class UpdateCustomerCompanyDto
{
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Name { get; set; }
}