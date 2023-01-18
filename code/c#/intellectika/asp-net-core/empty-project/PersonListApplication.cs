using System.Text.RegularExpressions;

namespace emptyproject
{
    public class PersonListApplication
    {

        private WebApplication _currentWebApplication;

        public WebApplication CurrentWebApplication
        {
            get { return _currentWebApplication; }
            set { _currentWebApplication = value; }
        }


        private PersonListBusinessLogic personListLogic;

        public void RunCustomApplication()
        {
            AddRestMiddleware();
        }


        private void AddRestMiddleware()
        {
            CurrentWebApplication.Run(async(context) =>
            {
                var response = context.Response;
                var request = context.Request;
                var path = request.Path;

                string expressionForGuid = @"^/api/users/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";

                if (path.Equals("/api/users") && request.Method.Equals("GET"))
                {
                    await personListLogic.GetAllPeopleAsync(response);
                }

                else if (Regex.IsMatch(path, expressionForGuid) && request.Method.Equals("GET")) 
                {
                    string? id = path.Value.Split("/")[3];
                    await personListLogic.GetPersonAsync(id, response);
                }

                else if (path.Equals("/api/users") && request.Method.Equals("POST"))
                {
                    await personListLogic.CreatePersonAsync(response, request);
                }

                else if (path == "/api/users" && request.Method.Equals("PUT"))
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
                    response.ContentType = "text/html; charset=utf-8";
                    await response.SendFileAsync("html/index.html");
                }
            });
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public PersonListApplication(WebApplication app)
        {
            _currentWebApplication = app;
            personListLogic = new();
        }

    }
}
