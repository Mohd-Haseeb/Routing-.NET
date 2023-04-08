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