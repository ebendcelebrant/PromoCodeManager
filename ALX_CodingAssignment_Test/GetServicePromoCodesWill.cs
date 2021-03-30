using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ALX_CodingAssignment.Data;
using ALX_CodingAssignment.Domain.Dtos;
using ALX_CodingAssignment.Domain.Models;
using ALX_CodingAssignment.Domain.Responders;
using ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Commands.AddServicePromoCode;
using ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Queries;
using ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Queries.GetServicePromoCodes;
using ALX_CodingAssignment_Test.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using static ALX_CodingAssignment.Domain.Enumerations;
using ALX_CodingAssignment.Helpers;
using FluentAssertions;

namespace ALX_CodingAssignment_Test
{
    public class GetServicePromoCodesWill
    {
        [Fact]
        public void ReturnCorrectPage()
        {
            //Arrange
            var aServicePromoCode1 = TestHelper.CreateMockServicePromoCode(TestHelper.MakeValidServiceName + "1",
                TestHelper.MakeValidPromoCode, TestHelper.MakeValidServiceDescription);
            var aServicePromoCode2 = TestHelper.CreateMockServicePromoCode(TestHelper.MakeValidServiceName + "2",
                TestHelper.MakeValidPromoCode, TestHelper.MakeValidServiceDescription);
            var aServicePromoCode3 = TestHelper.CreateMockServicePromoCode(TestHelper.MakeValidServiceName + "3",
                TestHelper.MakeValidPromoCode, TestHelper.MakeValidServiceDescription);
            var aServicePromoCode4 = TestHelper.CreateMockServicePromoCode(TestHelper.MakeValidServiceName + "4",
                TestHelper.MakeValidPromoCode, TestHelper.MakeValidServiceDescription);
            var aServicePromoCode5 = TestHelper.CreateMockServicePromoCode(TestHelper.MakeValidServiceName + "5",
                TestHelper.MakeValidPromoCode, TestHelper.MakeValidServiceDescription);
            var aServicePromoCode6 = TestHelper.CreateMockServicePromoCode(TestHelper.MakeValidServiceName + "6",
                TestHelper.MakeValidPromoCode, TestHelper.MakeValidServiceDescription);
            var aServicePromoCode7 = TestHelper.CreateMockServicePromoCode(TestHelper.MakeValidServiceName + "7",
                TestHelper.MakeValidPromoCode, TestHelper.MakeValidServiceDescription);
            var aServicePromoCode8 = TestHelper.CreateMockServicePromoCode(TestHelper.MakeValidServiceName + "8",
                TestHelper.MakeValidPromoCode, TestHelper.MakeValidServiceDescription);
            var aServicePromoCode9 = TestHelper.CreateMockServicePromoCode(TestHelper.MakeValidServiceName + "9",
                TestHelper.MakeValidPromoCode, TestHelper.MakeValidServiceDescription);
            var aServicePromoCode10 = TestHelper.CreateMockServicePromoCode(TestHelper.MakeValidServiceName + "10",
                TestHelper.MakeValidPromoCode, TestHelper.MakeValidServiceDescription);


            var mockServiceRepository = new Mock<Repository<ServicePromoCode, int>>();
            var mockUserPromoRepository = new Mock<Repository<UserPromoCode, int>>();
            var mockGetLogger = new Mock<ILogger<GetServicePromoCodesHandler>>();

            var existingRepo = TestHelper.MakeServicePromoCodeRepo(
              aServicePromoCode1, aServicePromoCode2, aServicePromoCode3, aServicePromoCode4, aServicePromoCode5,
              aServicePromoCode6, aServicePromoCode7, aServicePromoCode8, aServicePromoCode9, aServicePromoCode10
            );

            mockServiceRepository.Setup(repo => repo.GetAll())
                    .Returns(existingRepo);


            var sut = new GetServicePromoCodesHandler(mockServiceRepository.Object,
                mockUserPromoRepository.Object, mockGetLogger.Object);

            //Act
            var response = sut.Handle(new GetServicePromoCode()
            {
                Page = 2,
                PageSize = 4,
                OrderingType = OrderingBasis.DateAddedAsc
            });

            //Assert
            var aServicePromoCodeDto5 = new ServicePromoCodeDto()
            {
                DateAdded = aServicePromoCode5.DateAdded,
                Description = aServicePromoCode5.Description,
                HasUserActivatedCode = false,
                Id = 0,
                LastModifiedDate = aServicePromoCode5.LastModifiedDate,
                PromoCode = aServicePromoCode5.PromoCode,
                ServiceName = aServicePromoCode5.ServiceName
            };
            List<Variance> objectVariances = aServicePromoCodeDto5.DetailedCompare(response.Payload.ServicePromoCodes.First());
            Assert.Equal(4, response.Payload.ServicePromoCodes.Count());
            Assert.Empty(objectVariances);
            //Assert.Equal(aServicePromoCode8, response.Payload.ServicePromoCodes.Last());
        }

    }
}
