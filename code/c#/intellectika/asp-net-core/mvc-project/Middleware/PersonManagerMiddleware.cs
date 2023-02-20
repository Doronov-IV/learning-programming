using Microsoft.AspNetCore.Http.Features;
using System.Text.RegularExpressions;
using mvcproject.Models;
using mvcproject.Controllers;

namespace mvcproject
{
    public class PersonManagerMiddleware
    {

        private readonly RequestDelegate _next;


        private PersonListBusinessLogic personListLogic;


        public PersonManagerMiddleware(RequestDelegate next)
        {
            _next = next;
            personListLogic = new();
        }


        public async Task InvokeAsync(HttpContext context)
        {
            var response = context.Response;
            var request = context.Request;
            var path = request.Path;

            string expressionForGuid = @"^/api/users/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";
            string apiPath = "/api/users";

            if (path.Equals(apiPath) && request.Method.Equals("GET"))
            {
                await personListLogic.GetAllPeopleAsync(response);
            }

            else if (Regex.IsMatch(path, expressionForGuid) && request.Method.Equals("GET"))
            {
                string? id = path.Value.Split("/")[3];
                await personListLogic.GetPersonAsync(id, response);
            }

            else if (path.Equals(apiPath) && request.Method.Equals("POST"))
            {
                await personListLogic.CreatePersonAsync(response, request);
            }

            else if (path == apiPath && request.Method.Equals("PUT"))
            {
                await personListLogic.UpdatePersonAsync(response, request);
            }

            else if (Regex.IsMatch(path, expressionForGuid) && request.Method.Equals("DELETE"))
            {
                string? id = path.Value.Split("/")[3];
                await personListLogic.DeletePersonAsync(id, response);
            }

            else
            {
                await _next.Invoke(context);
            }
        }
    }
}