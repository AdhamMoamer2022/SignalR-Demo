using Microsoft.AspNetCore.SignalR;
using SignalRDemo.EFModels;
using SignalRDemo.HubModels;

namespace SignalRDemo.HubConfig
{

    public class MyHub : Hub
    {
        private readonly SignalRContext ctx;


        public MyHub(SignalRContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task authMe(PersonInfo personInfo)
        {
            string currSignalrID = Context.ConnectionId;
            Person tempPerson = ctx.Person.Where
            (p => p.Username == personInfo.userName && p.Password == personInfo.password).SingleOrDefault();

            if (tempPerson != null) 
            {
                Console.WriteLine("\n" + tempPerson.Name + " logged in" + "\nSignalrID: " + currSignalrID);

                Connections currUser = new Connections
                {
                    PersonId = tempPerson.Id,
                    SignalrId = currSignalrID,
                    TimeStamp = DateTime.Now
                };
                await ctx.Connections.AddAsync(currUser);
                await ctx.SaveChangesAsync();

                await Clients.Caller.SendAsync("authMeResponseSuccess", tempPerson);
            }

            else 
            {
                await Clients.Caller.SendAsync("authMeResponseFail");
            }
        }





        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("MessageResponse", message);
            //await Clients.Client(Context.ConnectionId).SendAsync("MessageResponse", message);
        }
    }
}
