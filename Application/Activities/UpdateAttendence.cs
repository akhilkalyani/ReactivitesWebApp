using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class UpdateAttendence
    {
        public class Command:IRequest<Result<Unit>>{
            public Guid ID { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext dataContext;
            private readonly IUserAccessor userAccessor;

            public Handler(DataContext dataContext,IUserAccessor userAccessor)
            {
                this.dataContext = dataContext;
                this.userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await dataContext.Activities
                .Include(a => a.Attendees)
                .ThenInclude(u => u.AppUser)
                .SingleOrDefaultAsync(x => x.Id == request.ID);

                if (activity == null) return null;

                var user = await dataContext.Users.FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUserName());

                if (user == null) return null;

                var hostName = activity.Attendees.FirstOrDefault(x => x.IsHost)?.AppUser?.UserName;

                var attendence = activity.Attendees.FirstOrDefault(x => x.AppUser.UserName == user.UserName);

                if(attendence!=null && hostName==user.UserName) activity.IsCancelled = !activity.IsCancelled;

                if(attendence!=null && hostName!=user.UserName) activity.Attendees.Remove(attendence);

                if(attendence==null){
                    attendence = new ActivityAttendee
                    {
                        AppUser=user,
                        Activity=activity,
                        IsHost=false
                    };

                    activity.Attendees.Add(attendence);
                }

                var result = await dataContext.SaveChangesAsync()>0;

                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to update attendence");
            }
        }
    }
}