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

        [Test]
        public void ShouldReturnZeroMonthlyComputeChargeForComputationLessThanFreeTier()
        {
            var computationParameters = CreateComputationParameters(allocatedMemory: 128, numberOfRequests: 3_000_000, computationInSeconds: 1);
            var awsLambdaCost = new AWSLambdaCost(computationParameters);

            Assert.AreEqual(0, awsLambdaCost.MonthlyComputeCharge);
        }

        [Test]
        public void ShouldReturnZeroMonthlyRequestChargeForRequestsLessThanFreeTier()
        {
            var computationParameters = CreateComputationParameters(allocatedMemory: 128, numberOfRequests: 500_000, computationInSeconds: 1);
            var awsLambdaCost = new AWSLambdaCost(computationParameters);

            Assert.AreEqual(0, awsLambdaCost.MonthlyRequestCharge);
        }

        [Test]
        public void ShouldReturnZeroMonthlyComputeChargeForComputationEqualFreeTier()
        {
            var computationParameters = CreateComputationParameters(allocatedMemory: 128, numberOfRequests: 3_200_000, computationInSeconds: 1);
            var awsLambdaCost = new AWSLambdaCost(computationParameters);

            Assert.AreEqual(0, awsLambdaCost.MonthlyComputeCharge);
        }

        [Test]
        public void ShouldReturnZeroMonthlyRequestChargeForRequestsEqualFreeTier()
        {
            var computationParameters = CreateComputationParameters(allocatedMemory: 128, numberOfRequests: 1_000_000, computationInSeconds: 1);
            var awsLambdaCost = new AWSLambdaCost(computationParameters);

            Assert.AreEqual(0, awsLambdaCost.MonthlyRequestCharge);
        }

        [Test]
        public void ShouldReturnEighteenDollarsAndThirtyFourCentsOfMonthlyComputeCharge()
        {
            var computationParameters = CreateComputationParameters(allocatedMemory: 512, numberOfRequests: 3_000_000, computationInSeconds: 1);
            var awsLambdaCost = new AWSLambdaCost(computationParameters);

            Assert.AreEqual(18.34, awsLambdaCost.MonthlyComputeCharge);
        }

        [Test]
        public void ShouldReturnFortyCentsOfMonthlyRequestCharge()
        {
            var computationParameters = CreateComputationParameters(allocatedMemory: 512, numberOfRequests: 3_000_000, computationInSeconds: 1);
            var awsLambdaCost = new AWSLambdaCost(computationParameters);

            Assert.AreEqual(0.4, awsLambdaCost.MonthlyRequestCharge);
        }

        [Test]
        public void ShouldReturnFiveDollarsAndEightyCentsOfMonthlyRequestCharge()
        {
            var computationParameters = CreateComputationParameters(allocatedMemory: 128, numberOfRequests: 30_000_000, computationInSeconds: 0.2m);
            var awsLambdaCost = new AWSLambdaCost(computationParameters);

            Assert.AreEqual(5.8, awsLambdaCost.MonthlyRequestCharge);
        }

        [Test]
        public void ShouldReturnSixDollarsAndThirtyCentsOfMonthlyRequestCharge()
        {
            var computationParameters = CreateComputationParameters(allocatedMemory: 256, numberOfRequests: 32_500_000, computationInSeconds: 1);
            var awsLambdaCost = new AWSLambdaCost(computationParameters);

            Assert.AreEqual(6.3, awsLambdaCost.MonthlyRequestCharge);
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
