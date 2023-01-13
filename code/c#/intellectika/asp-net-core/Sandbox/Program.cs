global using System;
global using Newtonsoft.Json;
using System.Text.Json;

namespace emptyproject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");


            app.Run(async (context) =>
            {
                var jsonOptions = new JsonSerializerOptions();
                jsonOptions.Converters.Add(new PersonConverter());

                var response = context.Response;
                var request = context.Request;
                if (request.Path == "/api/user")
                {
                    var message = "������������ ������";   // ���������� ��������� �� ���������
                    try
                    {
                        // �������� �������� ������ json
                        var person = await request.ReadFromJsonAsync<Person>(jsonOptions);
                        if (person != null) // ���� ������ ��������������� � Person
                            message = $"Name: {person.Name}  Age: {person.Age}";
                    }
                    catch { }
                    // ���������� ������������ ������
                    await response.WriteAsJsonAsync(new { text = message });
                }
                else
                {
                    response.ContentType = "text/html; charset=utf-8";
                    await response.SendFileAsync("html/index.html");
                }
            });

            app.Run();
        }
    }
}