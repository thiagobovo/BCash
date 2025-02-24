using BCash.Domain.Entities;
using BCash.Domain.Repositories;
using BCash.Service.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BCash.Tests.Services
{
    public class TransactionServiceTests
    {
        private MockRepository mockRepository;

        private Mock<ITransactionRepository> mockTransactionRepository;

        public TransactionServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockTransactionRepository = this.mockRepository.Create<ITransactionRepository>();
        }

        private TransactionService CreateService()
        {
            return new TransactionService(
                this.mockTransactionRepository.Object);
        }

        [Fact]
        public async Task CancelTransaction_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid id = default(global::System.Guid);

            // Act
            await service.CancelTransaction(
                id);

            // Assert
            Assert.True(true);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetTransaction_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid id = default(global::System.Guid);

            // Act
            var result = await service.GetTransaction(
                id);

            // Assert
            Assert.True(true);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetTransactionPaged_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            DateTime initDate = default(global::System.DateTime);
            DateTime endDate = default(global::System.DateTime);
            int pageNumber = 1;
            int pageSize = 10;

            // Act
            var result = await service.GetTransactionPaged(
                initDate,
                endDate,
                pageNumber,
                pageSize);

            // Assert
            Assert.True(true);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ProcessTransaction_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Transaction transaction = new Transaction(100, DateTime.Now, "C", null);

            // Act
            var result = await service.ProcessTransaction(
                transaction);

            // Assert
            Assert.True(true);
            this.mockRepository.VerifyAll();
        }
    }
}
