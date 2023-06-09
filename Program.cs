using System;
using Microsoft.AspNetCore.Http;
using Routing.CustomConstraint;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    WebRootPath = "myroot"
});

builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthCustomConstraint));
});


var app = builder.Build();

// to use static files (css, js, images) -> by default it takes wwwroot
app.UseStaticFiles(); // works with 'myroot'

app.UseStaticFiles(new StaticFileOptions() {
    FileProvider = new PhysicalFileProvider(
               Path.Combine(builder.Environment.ContentRootPath, "mywebroot")
            )
}); // works with 'mywebroot'


// enabling routing
app.UseRouting();

// we are defining our middlewares which map our end points to a specific url
// i.e; creating End Points
#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.Map("/files/{filename}.{extension=txt}", async context => {

        string? filename = Convert.ToString(context.Request.RouteValues["filename"]);

        await context.Response.WriteAsync($"In files -> {filename}");

    });


    endpoints.Map("employess/profile/{empname?}", async (context) =>
    {

        if (context.Request.RouteValues.ContainsKey("empname"))
        {
            string? empName = Convert.ToString(context.Request.RouteValues["empname"]);

            await context.Response.WriteAsync($"In files -> {empName}");
        }
        else
        {
            await context.Response.WriteAsync("Employee name is not supplied");
        }
    });

    endpoints.Map("products/details/{id:int?}", async (context) =>
    {
        int id = Convert.ToInt32(context.Request.RouteValues["id"]);
        await context.Response.WriteAsync($"Product details -> {id}");
    });

    endpoints.Map("cities/{cityid:guid}", async (context) =>
    {
        Guid cityId = Guid.Parse(Convert.ToString(context.Request.RouteValues["cityid"]!));
        await context.Response.WriteAsync($"City Information -> {cityId}");
    });

    endpoints.Map("sales-report/{year:int:min(1900)}/{month:regex(^(apr|jul|may)$)}", async (context) =>
    {
        int year =Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);

        await context.Response.WriteAsync($"sales report - {month} - {year}");
    });


    endpoints.Map("sales-report-2/{year:int:min(2000)}/month:months", async (context) =>
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);

        await context.Response.WriteAsync($"sales report - {month} - {year}");
    });

    // this has more preference than above url
    endpoints.Map("sales-report/2023/may", async (context) =>
    {
        await context.Response.WriteAsync($"sales report - may- 2023s");
    });

});
#pragma warning restore ASP0014 // Suggest using top level route registrations


// if it doesn't match any path, by default this middleware will execute
app.Run(async context => {
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

app.Run();
