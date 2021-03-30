using ALX_CodingAssignment.Domain.Interfaces;
using ALX_CodingAssignment.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ALX_CodingAssignment.Domain.Enumerations;

namespace ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Queries
{
    public class GetServicePromoCode : IQueryFor<ServicePromoCodePayload>
    {
        public int UserId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public OrderingBasis OrderingType { get; set; } = OrderingBasis.DateAddedDesc;
    }
    public class GetServicePromoCodeByName : IQueryFor<ServicePromoCodePayload>
    {
        public int UserId { get; set; }
        public string Filter { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public OrderingBasis OrderingType { get; set; } = OrderingBasis.DateAddedDesc;
    }

    public class ServicePromoCodePayload
    {
        public IEnumerable<ServicePromoCodeDto> ServicePromoCodes { get; set; }
        public int Page { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}

