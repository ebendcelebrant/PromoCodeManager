using System;
using System.Collections.Generic;
using System.Text;
using ALX_CodingAssignment.Data;
using ALX_CodingAssignment.Domain.Models;
using ALX_CodingAssignment.Domain.Responders;
using ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Commands.ActivateServicePromoCode;
using ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Commands.AddServicePromoCode;
using ALX_CodingAssignment_Test.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ALX_CodingAssignment_Test
{
    public class ActivateServicePromoCodeWill
    {
        [Fact]
        public void AllowValidServicePromoCodeId()
        {
            var mockActivateServicePromoCode = new ActivateServicePromoCode(1, 1);

            var mockRepository = new Mock<Repository<UserPromoCode, int>>();
            var mockLogger = new Mock<ILogger<ActivateServicePromoCodeHandler>>();

            var sut = new ActivateServicePromoCodeHandler(mockRepository.Object, mockLogger.Object);

            var response = sut.Handle(mockActivateServicePromoCode);

            Assert.True(!response.HasErrors());
        }
    }
}
