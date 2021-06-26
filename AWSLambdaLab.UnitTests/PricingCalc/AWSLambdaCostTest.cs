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
        public void ShouldReturnAnExceptionForAllocatedMemoryLessThanOrEqualToZero()
        {
            var computationParameters = CreateComputationParameters(allocatedMemory: 0, numberOfRequests: 1000, computationInSeconds: 2);
            var exception = Assert.Throws<ApplicationException>(() => new AWSLambdaCost(computationParameters));

            Assert.That(exception.Message, Is.EqualTo("Allocated Memory parameter should be greater than zero."));
        }

        [Test]
        public void ShouldReturnAnExceptionForNumberOfRequestsLessThanOrEqualToZero()
        {
            var computationParameters = CreateComputationParameters(allocatedMemory: 128, numberOfRequests: 0, computationInSeconds: 2);
            var exception = Assert.Throws<ApplicationException>(() => new AWSLambdaCost(computationParameters));

            Assert.That(exception.Message, Is.EqualTo("Number of requests parameter should be greater than zero."));
        }

        [Test]
        public void ShouldReturnAnExceptionForComputationInSecondsLessThanOrEqualToZero()
        {
            var computationParameters = CreateComputationParameters(allocatedMemory: 128, numberOfRequests: 1000, computationInSeconds: 0);
            var exception = Assert.Throws<ApplicationException>(() => new AWSLambdaCost(computationParameters));

            Assert.That(exception.Message, Is.EqualTo("Computation in seconds parameter should be greater than zero."));
        }

        private ComputationParameters CreateComputationParameters(decimal allocatedMemory, int numberOfRequests, decimal computationInSeconds)
        {
            return new ComputationParameters()
            {
                AllocatedMemoryByFunctionInMegaBytes = allocatedMemory,
                NumberOfRequests = numberOfRequests,
                TotalComputeByFunctionInSeconds = computationInSeconds
            };
        }
    }
}
