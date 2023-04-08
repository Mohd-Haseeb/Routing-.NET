using System;
using System.Text.RegularExpressions;

namespace Routing.CustomConstraint;



public class MonthCustomConstraint : IRouteConstraint
{
    public bool Match(
        HttpContext? httpContext,
        IRouter? route,
        string routeKey,
        RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        if (!values.ContainsKey(routeKey)) // key is month
            return false; // not a match

        Regex regex = new Regex($"^(apr|aug|may)$");

        string? monthValue = Convert.ToString(values[routeKey]);

        if (regex.IsMatch(monthValue))
            return true; // match

        return false; // not a match
             
    }
}


