using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query:IRequest<Result<ActivityDTO>>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<ActivityDTO>>
        {
            private readonly DataContext dataContext;
            private readonly IMapper _mapper;

            public Handler(DataContext dataContext,IMapper mapper)
            {
                _mapper = mapper;
                this.dataContext = dataContext;
            }

            public async Task<Result<ActivityDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await dataContext.Activities
                .ProjectTo<ActivityDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x=>x.Id==request.Id); 

                return Result<ActivityDTO>.Success(activity);
            }
        }
    }
}