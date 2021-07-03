using System;
using System.Net;
using Newtonsoft.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using AWSLambdaLab.PricingCalc.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambdaLab.PricingCalc
{
    public class CalculateCostsFunction
    {
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var computationParameters = new ComputationParameters()
            {
                AllocatedMemoryByFunctionInMegaBytes = Convert.ToDecimal(request.QueryStringParameters["AllocatedMemoryByFunctionInMegaBytes"]),
                NumberOfRequests = Convert.ToInt32(request.QueryStringParameters["NumberOfRequests"]),
                TotalComputeByFunctionInSeconds = Convert.ToDecimal(request.QueryStringParameters["TotalComputeByFunctionInSeconds"])
            };

            return new APIGatewayProxyResponse() 
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(computationParameters)
            };
        }
    }
}
