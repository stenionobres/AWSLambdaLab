using System;
using System.Net;
using Newtonsoft.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using AWSLambdaLab.PricingCalc.Models;
using AWSLambdaLab.PricingCalc.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambdaLab.PricingCalc
{
    public class CalculateCostsFunction
    {
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var computationParameters = new ComputationParameters()
                {
                    AllocatedMemoryByFunctionInMegaBytes = Convert.ToDecimal(request.QueryStringParameters["AllocatedMemoryByFunctionInMegaBytes"]),
                    NumberOfRequests = Convert.ToInt32(request.QueryStringParameters["NumberOfRequests"]),
                    TotalComputeByFunctionInSeconds = Convert.ToDecimal(request.QueryStringParameters["TotalComputeByFunctionInSeconds"])
                };

                var serviceLambdaCost = new ServiceAWSLambdaCost();
                var costResult = serviceLambdaCost.Calculate(computationParameters);

                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = JsonConvert.SerializeObject(costResult)
                };
            }
            catch (Exception)
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Body = "Internal Server Error"
                };
            }
            
        }
    }
}
