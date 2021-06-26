
namespace AWSLambdaLab.PricingCalc.Models
{
    public class ComputationParameters
    {
        public decimal AllocatedMemoryByFunctionInMegaBytes { get; set; }
        public int NumberOfRequests { get; set; }
        public decimal TotalComputeByFunctionInSeconds { get; set; }
    }
}
