using ALX_CodingAssignment.Domain.Dtos;
using ALX_CodingAssignment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALX_CodingAssignment_Test.Helpers
{
    public class TestHelper
    {
        public const string MakeValidServiceName = "TestServiceName";
        public const string MakeValidPromoCode = "TestPromoCode";
        public const string MakeValidServiceDescription = "TestDesc";
        public static ServicePromoCode CreateMockServicePromoCode(string serviceName, string promoCode, string description)
        {
            return new ServicePromoCode()
            {
                ServiceName = serviceName,
                PromoCode = promoCode,
                Description = description,
                DateAdded = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };
        }
        public static ServicePromoCodeDto CreateMockServicePromoCodeDto(string serviceName, string promoCode, string description)
        {
            return new ServicePromoCodeDto()
            {
                ServiceName = serviceName,
                PromoCode = promoCode,
                Description = description,
                DateAdded = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                 HasUserActivatedCode = false
            };
        }
        public static IQueryable<ServicePromoCodeDto> MakeServicePromoCodeDtoRepo(params ServicePromoCodeDto[] servicePromoCodeDtos)
        {
            return new List<ServicePromoCodeDto>(servicePromoCodeDtos).AsQueryable();
        }
        public static IQueryable<ServicePromoCode> MakeServicePromoCodeRepo(params ServicePromoCode[] servicePromoCodes)
        {
            return new List<ServicePromoCode>(servicePromoCodes).AsQueryable();
        }
    }
}
