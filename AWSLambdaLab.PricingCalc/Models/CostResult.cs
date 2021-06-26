
namespace AWSLambdaLab.PricingCalc.Models
{
    public class CostResult
    {
        public int FreeComputeChargeInGigabytePerSecond { get; set; }
        public int FreeRequestsCharge { get; set; }
        public decimal ComputePricePerGigabyteSecond { get; set; }
        public decimal RequestsPricePerMillion { get; set; }
        public decimal MonthlyComputeCost { get; set; }
        public decimal MonthlyRequestCost { get; set; }
        public decimal MonthlyTotalCost { get; set; }
    }
}
