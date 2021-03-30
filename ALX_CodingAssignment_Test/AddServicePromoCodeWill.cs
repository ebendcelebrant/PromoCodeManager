using System;
using System.Collections.Generic;
using System.Text;
using ALX_CodingAssignment.Data;
using ALX_CodingAssignment.Domain.Models;
using ALX_CodingAssignment.Domain.Responders;
using ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Commands.AddServicePromoCode;
using ALX_CodingAssignment_Test.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ALX_CodingAssignment_Test
{
    public class AddServicePromoCodeWill
    {
        [Fact]
        public void NotAllowBlankServiceName()
        {
            var mockServicePromoCode = TestHelper.CreateMockServicePromoCode("", "itpromocode", "mock description");
            var mockAddServicePromoCode = new AddServicePromoCode(mockServicePromoCode);

            var mockRepository = new Mock<Repository<ServicePromoCode, int>>();
            var mockLogger = new Mock<ILogger<AddServicePromoCodeHandler>>();

            var sut = new AddServicePromoCodeHandler(mockRepository.Object, mockLogger.Object);

            var response = sut.Handle(mockAddServicePromoCode);

            Assert.True(response.HasErrors());
        }
        [Fact]
        public void AllowValidServiceName()
        {
            var mockServicePromoCode = TestHelper.CreateMockServicePromoCode("Amazon.com", "itpromocode", "mock description");
            var mockAddServicePromoCode = new AddServicePromoCode(mockServicePromoCode);

            var mockRepository = new Mock<Repository<ServicePromoCode, int>>();
            var mockLogger = new Mock<ILogger<AddServicePromoCodeHandler>>();

            var sut = new AddServicePromoCodeHandler(mockRepository.Object, mockLogger.Object);

            var response = sut.Handle(mockAddServicePromoCode);

            Assert.True(!response.HasErrors());
        }

    }
}
