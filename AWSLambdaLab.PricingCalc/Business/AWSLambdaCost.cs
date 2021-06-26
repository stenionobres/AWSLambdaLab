using System;
using AWSLambdaLab.PricingCalc.Models;

namespace AWSLambdaLab.PricingCalc.Business
{
    public class AWSLambdaCost
    {
        public decimal MemoryInMegabytes { get; private set; }
        public int NumberOfRequests { get; private set; }
        public decimal TotalComputeInSeconds { get; private set; }

        public AWSLambdaCost(ComputationParameters computationParameters)
        {
            if (computationParameters.AllocatedMemoryByFunctionInMegaBytes <= 0)
                throw new ApplicationException("Allocated Memory parameter should be greater than zero.");

            if (computationParameters.NumberOfRequests <= 0)
                throw new ApplicationException("Number of requests parameter should be greater than zero.");

            if (computationParameters.TotalComputeByFunctionInSeconds <= 0)
                throw new ApplicationException("Computation in seconds parameter should be greater than zero.");

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
