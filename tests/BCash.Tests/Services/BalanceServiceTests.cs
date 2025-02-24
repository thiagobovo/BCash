using BCash.Domain.Repositories;
using BCash.Service.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BCash.Tests.Services
{
    public class BalanceServiceTests
    {
        private MockRepository mockRepository;

        private Mock<IBalanceRepository> mockBalanceRepository;

        public BalanceServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockBalanceRepository = this.mockRepository.Create<IBalanceRepository>();
        }

        private BalanceService CreateService()
        {
            return new BalanceService(
                this.mockBalanceRepository.Object);
        }

        [Fact]
        public async Task GetBalancePaged_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            DateTime initDate = default(global::System.DateTime);
            DateTime endDate = default(global::System.DateTime);
            int pageNumber = 1;
            int pageSize = 10;

            // Act
            var result = await service.GetBalancePaged(
                initDate,
                endDate,
                pageNumber,
                pageSize);

            // Assert
            Assert.True(true);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ProcessBalance_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            decimal amount = 100;
            DateTime date = default(global::System.DateTime);
            string type = "C";

            // Act
            var result = await service.ProcessBalance(
                amount,
                date,
                type);

            // Assert
            Assert.True(true);
            this.mockRepository.VerifyAll();
        }
    }
}
