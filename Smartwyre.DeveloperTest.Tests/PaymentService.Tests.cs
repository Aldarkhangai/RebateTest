using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;


public class PaymentServiceTests
{
    private readonly Mock<IRebateDataStore> _rebateDatatStore;
    private readonly Mock<IProductDataStore> _productStore;

    public PaymentServiceTests() 
    {
        _rebateDatatStore = new Mock<IRebateDataStore>();
        _productStore = new Mock<IProductDataStore>();
    }

    [Fact]
    public void FixedRateRebate_test()
    {
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "1",
            RebateIdentifier = "1",
            Volume = 5m
        };
        //arrange 
        var rebateService = new RebateService(_productStore.Object, _rebateDatatStore.Object);
        _rebateDatatStore.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(new Rebate() { Amount = 3, Identifier = "1", Incentive = IncentiveType.FixedRateRebate, Percentage = 5 });
        _productStore.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(new Product() { Id = 1, Identifier = "1", Price = 30m, SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Uom = "" });

        //act

        var resp = rebateService.Calculate(request);

        //assert

        Assert.NotNull(resp);
        Assert.True(resp.Success);
    }


    [Fact]
    public void AmountPerUom_test()
    {
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "1",
            RebateIdentifier = "1",
            Volume = 5m
        };
        //arrange 
        var rebateService = new RebateService(_productStore.Object, _rebateDatatStore.Object);
        _rebateDatatStore.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(new Rebate() { Amount = 3, Identifier = "1", Incentive = IncentiveType.AmountPerUom, Percentage = 5 });
        _productStore.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(new Product() { Id = 1, Identifier = "1", Price = 30m, SupportedIncentives = SupportedIncentiveType.AmountPerUom, Uom = "" });

        //act

        var resp = rebateService.Calculate(request);

        //assert

        Assert.NotNull(resp);
        Assert.True(resp.Success);

    }

    [Fact]
    public void FixedCashAmount_test()
    {
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "1",
            RebateIdentifier = "1",
            Volume = 5m
        };
        //arrange 
        var rebateService = new RebateService(_productStore.Object, _rebateDatatStore.Object);
        _rebateDatatStore.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(new Rebate() { Amount = 3, Identifier = "1", Incentive = IncentiveType.FixedCashAmount, Percentage = 5 });
        _productStore.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(new Product() { Id = 1, Identifier = "1", Price = 30m, SupportedIncentives = SupportedIncentiveType.FixedCashAmount, Uom = "" });

        //act

        var resp = rebateService.Calculate(request);

        //assert

        Assert.NotNull(resp);
        Assert.True(resp.Success);
    }
}
