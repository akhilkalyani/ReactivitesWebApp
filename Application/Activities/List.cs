using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query:IRequest<Result<List<ActivityDTO>>> {}
        public class Handler : IRequestHandler<Query,Result<List<ActivityDTO>>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public Handler(DataContext dataContext,IMapper mapper)
            {
                _mapper = mapper;
                _dataContext = dataContext;
            }

            public async Task<Result<List<ActivityDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = await _dataContext.Activities
                .ProjectTo<ActivityDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

                return Result<List<ActivityDTO>>.Success(activities);
            }
        }
    }
}