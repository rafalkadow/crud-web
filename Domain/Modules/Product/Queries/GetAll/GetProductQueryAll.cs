﻿using MediatR;
using Shared.Interfaces;

namespace Domain.Modules.Product.Queries
{
    [Serializable]
    public class GetProductQueryAll : GetProductBaseFilter, IRequest<IList<GetProductResultAll>>, IQuery
    {
    }
}