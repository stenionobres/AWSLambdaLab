using System;
using AWSLambdaLab.PricingCalc.Models;

namespace AWSLambdaLab.PricingCalc.Business
{
    public class AWSLambdaCost
    {
        public decimal MemoryInMegabytes { get; set; }
        public int NumberOfRequests { get; set; }
        public decimal TotalComputeInSeconds { get; set; }

        public AWSLambdaCost(ComputationParameters computationParameters)
        {
            if (computationParameters.AllocatedMemoryByFunctionInMegaBytes <= 0)
                throw new ApplicationException("Allocated Memory parameter should be greater than zero.");

            MemoryInMegabytes = computationParameters.AllocatedMemoryByFunctionInMegaBytes;
            NumberOfRequests = computationParameters.NumberOfRequests;
            TotalComputeInSeconds = computationParameters.TotalComputeByFunctionInSeconds;
        }

        public decimal calculate()
        {
            return 1;
        }
    }
}
