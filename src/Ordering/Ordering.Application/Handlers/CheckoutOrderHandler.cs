using AutoMapper;
using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.Mapper;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, OrderResponse>
    {
        private IOrderRepository _repo;

        public CheckoutOrderHandler(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<OrderResponse> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = OrderMapper.Mapper.Map<Order>(request);
            if (orderEntity == null)
            {
                throw new ApplicationException("Not Mapped");
            }
            var newOrder = await _repo.AddAsync(orderEntity);
            return OrderMapper.Mapper.Map<OrderResponse>(newOrder);
        }
    }
}
