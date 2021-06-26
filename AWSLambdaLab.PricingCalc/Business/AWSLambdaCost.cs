﻿using System;
using AWSLambdaLab.PricingCalc.Models;

namespace AWSLambdaLab.PricingCalc.Business
{
    public class AWSLambdaCost
    {
        public decimal MemoryInMegabytes { get; private set; }
        public int NumberOfRequests { get; private set; }
        public decimal TotalComputeInSeconds { get; private set; }
        public int FreeTierComputeChargesInGigabytePerSecond => 400_000;
        public decimal ComputePriceByGigabytePerSecond => 0.00001667m;
        public int FreeTierRequestsChargesInMillions => 1_000_000;
        public decimal RequestsPricePerMillion => 0.2m;


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

        public decimal Calculate()
        {
            return 1;
        }

        public decimal MonthlyRequestCharge
        {
            get
            {
                var requestsToBeCharged = NumberOfRequests - FreeTierRequestsChargesInMillions;

                if (requestsToBeCharged <= 0) return 0;

                return requestsToBeCharged / 1_000_000 * RequestsPricePerMillion;
            }
        }
        
    }
}
