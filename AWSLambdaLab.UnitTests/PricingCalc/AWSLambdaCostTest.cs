using System;
using NUnit.Framework;
using AWSLambdaLab.PricingCalc.Models;
using AWSLambdaLab.PricingCalc.Business;

namespace AWSLambdaLab.UnitTests.PricingCalc
{
    [TestFixture]
    public class AWSLambdaCostTest
    {
        [Test]
        public void ShouldReturnOneInTheFirstTest()
        {
            var computationParameters = new ComputationParameters() 
            { 
                AllocatedMemoryByFunctionInMegaBytes = 128,
                NumberOfRequests = 1000,
                TotalComputeByFunctionInSeconds = 2
            };

            var awsLambdaCost = new AWSLambdaCost(computationParameters);

            Assert.AreEqual(1, awsLambdaCost.calculate());
        }

        [Test]
        public void ShouldReturnAnExceptionForAllocatedMemoryLesserEqualToZero()
        {
            var computationParameters = new ComputationParameters()
            {
                AllocatedMemoryByFunctionInMegaBytes = 0,
                NumberOfRequests = 1000,
                TotalComputeByFunctionInSeconds = 2
            };

            var exception = Assert.Throws<ApplicationException>(() => new AWSLambdaCost(computationParameters));

            Assert.That(exception.Message, Is.EqualTo("Allocated Memory parameter should be greater than zero."));
        }

     }
}
