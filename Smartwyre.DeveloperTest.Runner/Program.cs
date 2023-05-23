using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {

        var serviceProvider = new ServiceCollection()
            .AddSingleton<IProductDataStore, ProductDataStore>()
            .AddSingleton<IRebateDataStore, RebateDataStore>()
            .AddSingleton<IRebateService, RebateService>()
            .BuildServiceProvider();


        var request = new Types.CalculateRebateRequest()
        {
            ProductIdentifier = "1",
            RebateIdentifier = "1",
            Volume = 1m
        };
        var respose = serviceProvider.GetService<IRebateService>().Calculate(request);
       
    }
}
