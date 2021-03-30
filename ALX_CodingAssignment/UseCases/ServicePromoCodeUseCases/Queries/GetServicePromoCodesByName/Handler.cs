using ALX_CodingAssignment.Data;
using ALX_CodingAssignment.Domain.Interfaces;
using ALX_CodingAssignment.Domain.Models;
using ALX_CodingAssignment.Domain.Responders;
using ALX_CodingAssignment.Domain.Validations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ALX_CodingAssignment.Domain.Enumerations;
using ALX_CodingAssignment.Helpers;
using ALX_CodingAssignment.Domain.Dtos;

namespace ALX_CodingAssignment.UseCases.ServicePromoCodeUseCases.Queries.GetServicePromoCodesByName
{
    public class GetServicePromoCodesByNameHandler : IQueryHandler<GetServicePromoCodeByName, ServicePromoCodePayload>
    {
        private readonly IRepository<ServicePromoCode, int> servicePromoCodeRepository;
        private readonly IRepository<UserPromoCode, int> userPromoCodeRepository;
        private readonly ILogger<GetServicePromoCodesByNameHandler> logger;

        public GetServicePromoCodesByNameHandler(IRepository<ServicePromoCode, int> aServicePromoCodeRepository,
            IRepository<UserPromoCode, int> aUserPromoCodeRepository,
            ILogger<GetServicePromoCodesByNameHandler> aLogger)
        {
            servicePromoCodeRepository = Guard.IsNotNull(aServicePromoCodeRepository, nameof(aServicePromoCodeRepository));
            userPromoCodeRepository = Guard.IsNotNull(aUserPromoCodeRepository, nameof(aUserPromoCodeRepository));
            logger = aLogger;
        }
        public QueryResponse<ServicePromoCodePayload> Handle(GetServicePromoCodeByName request)
        {
            Guard.IsNotNull(request, nameof(request));
            Guard.IsNotNull(request.Filter, nameof(request.Filter));

            var pageSize = request.PageSize > 0 ? request.PageSize : 5;
            var pageNumber = request.Page > 0 ? request.Page : 1;

            var ordering = ResolveOrdering(request);

            var servicePromoCodes = servicePromoCodeRepository
                            .GetAll(x => x.ServiceName.ToUpper().Contains(request.Filter.ToUpper()))
                            .Order(ordering.selector, ordering.ascending)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize + 1)
                            .Select(s => new ServicePromoCodeDto
                            {
                                DateAdded = s.DateAdded,
                                Description = s.Description,
                                Id = s.Id,
                                LastModifiedDate = s.LastModifiedDate,
                                PromoCode = s.PromoCode,
                                ServiceName = s.ServiceName,
                                HasUserActivatedCode = userPromoCodeRepository.GetAll()
                                                .Where(u => u.UserId == request.UserId && u.ServicePromoCodeId == s.Id)
                                                .Count() > 0
                            });

            var payload = new ServicePromoCodePayload()
            {
                Page = pageNumber,
                HasNextPage = servicePromoCodes.Count() > pageSize,
                HasPreviousPage = pageNumber > 1,
                ServicePromoCodes = servicePromoCodes.Take(pageSize)
            };

            return QueryResponse<ServicePromoCodePayload>.Success(payload);
        }

        private (bool ascending, Func<ServicePromoCode, object> selector) ResolveOrdering(GetServicePromoCodeByName request)
        {
            switch (request.OrderingType)
            {
                case OrderingBasis.AlphabeticalAsc:
                    return (true, servicePromoCode => servicePromoCode.ServiceName);

                case OrderingBasis.AlphabeticalDesc:
                    return (false, servicePromoCode => servicePromoCode.ServiceName);

                case OrderingBasis.DateAddedAsc:
                    return (true, servicePromoCode => servicePromoCode.DateAdded);

                default:
                    return (false, servicePromoCode => servicePromoCode.DateAdded);

            }
        }
    }
}
