using GraphQLDemo.API.Schema.Subscriptions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQLServer()
    .AddQueryType<GraphQLDemo.API.Schema.Querys.Query>()
    .AddMutationType<GraphQLDemo.API.Schema.Mutations.Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();

var app = builder.Build();

app.UseWebSockets();

//app.MapGet("/", () => "Hello World!");
app.MapGraphQL();

app.Run();
