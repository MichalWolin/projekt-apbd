using Api.Exceptions;
using Api.Interfaces;
using Api.Services;
using ApiTests.TestObjects;
using Shouldly;

namespace ApiTests;

public class IncomeServiceTests
{
    private readonly IIncomeService _incomeService;

    public IncomeServiceTests()
    {
        _incomeService = new IncomeService(new FakeContractRepository(), new FakeSoftwareRepository());
    }

    [Fact]
    public async Task Shoud_ThrowException_WhenCurrencyIsInvalid()
    {
        int? softwareId = null;
        bool anticipatedIncomes = false;
        string currency = "XDDD";

        await Should.ThrowAsync<DomainException>
            (_incomeService.GetIncome(softwareId, anticipatedIncomes, currency,
                CancellationToken.None));
    }

    [Fact]
    public async Task Should_ReturnDecimal_WhenCurrencyIsValid()
    {
        int? softwareId = null;
        bool anticipatedIncomes = false;
        string currency = "USD";

        var result = await _incomeService.GetIncome(softwareId, anticipatedIncomes, currency,
            CancellationToken.None);

        result.Amount.ShouldBeOfType<decimal>();
    }

    [Fact]
    public async Task Should_ThrowException_WhenSoftwareDoesntExist()
    {
        int? softwareId = 3;
        bool anticipatedIncomes = false;
        string currency = "PLN";

        await Should.ThrowAsync<DomainException>
            (_incomeService.GetIncome(softwareId, anticipatedIncomes, currency, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ReturnDecimal_WhenSoftwareExists()
    {
        int? softwareId = 1;
        bool anticipatedIncomes = false;
        string currency = "PLN";

        var result = await _incomeService.GetIncome(softwareId, anticipatedIncomes, currency,
            CancellationToken.None);

        result.Amount.ShouldBeOfType<decimal>();
    }
}