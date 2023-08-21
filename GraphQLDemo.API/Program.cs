using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQLServer()
    .AddQueryType<GraphQLDemo.API.Schema.Querys.Query>()
    .AddMutationType<GraphQLDemo.API.Schema.Mutations.Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();


string connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddPooledDbContextFactory<SchoolDBContext>(o => o.UseSqlite(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    IDbContextFactory<SchoolDBContext> dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SchoolDBContext>>();
    using(SchoolDBContext context = dbContextFactory.CreateDbContext()){
        context.Database.Migrate();
    }
}

app.UseWebSockets();

//app.MapGet("/", () => "Hello World!");
app.MapGraphQL();

app.Run();
