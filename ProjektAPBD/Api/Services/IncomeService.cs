using System.Text.Json;
using Api.Exceptions;
using Api.Interfaces;
using Api.Models;
using Api.ResponseModels;

namespace Api.Services;

public class IncomeService : IIncomeService
{
    private readonly IContractRepository _contractRepository;
    private readonly ISoftwareRepository _softwareRepository;

    public IncomeService(IContractRepository contractRepository, ISoftwareRepository softwareRepository)
    {
        _contractRepository = contractRepository;
        _softwareRepository = softwareRepository;
    }

    public async Task<IncomeDto> GetIncome(int? softwareId, bool anticipatedIncomes, string currency)
    {
        decimal currencyRate = 1;
        if (!currency.Equals("PLN"))
        {
            EnsureCurrenyIsValid(currency);
            currencyRate = await GetCurrencyRate(currency);
        }


        if (softwareId is null)
        {
            var income = await GetWholeIncome(anticipatedIncomes);
            return new IncomeDto
            {
                Amount = income  * currencyRate,
                Currency = currency
            };
        }
        else
        {
            var income = await GetIncomeForSoftware(softwareId.Value, anticipatedIncomes);
            return new IncomeDto
            {
                SoftwareId = softwareId,
                Amount = income * currencyRate,
                Currency = currency
            };
        }
    }

    private async Task<decimal> GetIncomeForSoftware(int softwareId, bool anticipatedIncomes)
    {
        Software? software = await _softwareRepository.GetSoftware(softwareId);
        EnsureSoftwareExists(software, softwareId);

        var income = await _contractRepository.GetIncomeForSoftware(softwareId, anticipatedIncomes);

        return income;
    }

    private async Task<decimal> GetWholeIncome(bool anticipatedIncomes)
    {
        var income = await _contractRepository.GetWholeIncome(anticipatedIncomes);

        return income;
    }

    private static void EnsureSoftwareExists(Software? software, int softwareId)
    {
        if (software is null)
        {
            throw new DomainException($"Software with id {softwareId} does not exist!");
        }
    }

    private static async Task<decimal> GetCurrencyRate(string currency)
    {
        var requestString = "https://api.exchangerate-api.com/v4/latest/PLN";
        var httpClient = new HttpClient();
        var response = await httpClient.GetStringAsync(requestString);
        if (response.Contains("error"))
        {
            throw new DomainException("Currency rate API error");
        }

        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
        if (data is null)
        {
            throw new DomainException("Currency rate API error");
        }
        var rates = JsonSerializer.Deserialize<Dictionary<string, decimal>>(data["rates"].ToString());
        if (rates is null)
        {
            throw new DomainException("Currency rate API error");
        }

        if (!rates.ContainsKey(currency))
        {
            throw new DomainException($"Currency {currency} is not supported!");
        }

        return rates[currency];
    }

    private static void EnsureCurrenyIsValid(string currency)
    {
        if (currency.Length != 3)
        {
            throw new DomainException("Currency code must be 3 characters long!");
        }
    }
}