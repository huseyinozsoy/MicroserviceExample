using MediatR;
using Ordering.Application.Mapper;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class GetOrderByUserNameHandler : IRequestHandler<GetOrderByUserNameQuery, IEnumerable<OrderResponse>>
    {
        private IOrderRepository _repo;
        public GetOrderByUserNameHandler(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrderByUserNameQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _repo.GetOrdesByUserName(request.UserName);
            var orderResponse = OrderMapper.Mapper.Map<IEnumerable<OrderResponse>>(orderList);
            return orderResponse;
        }
    }
}
