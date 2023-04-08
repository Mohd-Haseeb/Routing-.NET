# __Routing:__
-----------------------------------
Routing is a process of matching incoming HTTP requests by checking the HTTP method and url. And invoking corresponding end points (middlewares).

In ASP.NET Core, we use __UseRouting()__ and __UseEndPoints()__ middlewares to enable routing

```cs
    app.UseRouting();

    app.UseEndPoinst(=> {
        endpoints.Map(...);
        endpoints.MapGet(...);
        endpoints.MapPost(...;)
    })
```
- ___UseRouting___: Enables routing and selects an appropriate end point based on the url path and HTTP method.

- ___UseEndPoinst___: Execuetes the appropriate endpoint based on the endpoint selected by the above UseRouting() method.

### Map()
- This middleware is executes if the path matches, irrespective of the HTTP method (GET, POST, PUT, DELETE). 

### MapGet()
- Only for `GET` requests.
s
### MapPost()
- Specific for `POST` requests only.


### GetEndPoint()
- Returns an instance of `Microsoft.AspNetCore.Http.Endpoint` type, which represents an endpoint.
- This instance contains two important properties : `DisplayName`, `RequestDelegate`.
```cs
    app.GetEndPoint()
```
- If we call  GetEndPoint() before calling the `Routing`, it returns ___null___.

# Route Parameters:

- A route parameter can match with any value
    - /emp/{name}

- Route parameters are case insensitive

- Route parameter with ___dafault value___
    - A route parameter with default value matches with any value. It also matches with empty value.
    - "{parameter=default_value}"


- Route paramters with __optional parameters__
    - "?" indicates an optional parameter
    - "parameter?"


- Route Constraints
    - int
    - bool
    - datetime
    - decimal
    - long
    - guid
    - minlength(value)
    - maxlength(value)
    - length(min,max)
    - length(value)
    - min(value)
    - max(value)
    - range(min,max)
    - alpha => matches with a string that contains only alphabets
    - regex(expression)


- __Custom Constraint__

    - We can also have a custom constraint class

```cs
    public class ClassName : IRouteConstraint
    {
        public bool Match(
            HttpContext? httpContext,
            IRouter? route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            // logic
             
        }
    }

    // in program.cs
    builder.Services.AddRouting(options =>
    {
        options.ConstraintMap.Add("months", typeof(MonthCustomConstraint));
    });

```
