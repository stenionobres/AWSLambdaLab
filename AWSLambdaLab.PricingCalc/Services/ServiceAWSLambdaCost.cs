using AWSLambdaLab.PricingCalc.Models;
using AWSLambdaLab.PricingCalc.Business;

namespace AWSLambdaLab.PricingCalc.Services
{
    public class ServiceAWSLambdaCost
    {
        public CostResult Calculate(ComputationParameters computationParameters)
        {
            var awsLambdaCost = new AWSLambdaCost(computationParameters);

            return new CostResult()
            {
                FreeComputeChargeInGigabytePerSecond = awsLambdaCost.FreeTierComputeChargesInGigabytePerSecond,
                FreeRequestsCharge = awsLambdaCost.FreeTierRequestsChargesInMillions,
                ComputePricePerGigabyteSecond = awsLambdaCost.ComputePriceByGigabytePerSecond,
                RequestsPricePerMillion = awsLambdaCost.RequestsPricePerMillion,
                MonthlyComputeCost = awsLambdaCost.MonthlyComputeCharge,
                MonthlyRequestCost = awsLambdaCost.MonthlyRequestCharge,
                MonthlyTotalCost = awsLambdaCost.Calculate()
            };
        }
    }
}
