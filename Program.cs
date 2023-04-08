using Microsoft.AspNetCore.Http;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// returns null
app.Use(async (context, next) => {
    Microsoft.AspNetCore.Http.Endpoint endpoint = context.GetEndpoint();
    await next(context);
});

// enabling routing
app.UseRouting();

// we are defining our middlewares which map our end points to a specific url
// i.e; creating End Points
#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.Map("/map1", async (context) => {
        await context.Response.WriteAsync("Map-1 end point is hit!!!");
    });

    endpoints.Map("/map2", async (context) => {
        await context.Response.WriteAsync("Map-2 end point is hit!!!");
    });

    endpoints.Map("/map3", async (context) => {
        await context.Response.WriteAsync("Map-3 end point is hit!!!");
    });
    // endpoints.MapControllers();
});
#pragma warning restore ASP0014 // Suggest using top level route registrations


app.Use(async (context, next) => {
    Endpoint endpoint = context.GetEndpoint();
    await next(context);
});

app.Run(async context => {
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

app.Run();
