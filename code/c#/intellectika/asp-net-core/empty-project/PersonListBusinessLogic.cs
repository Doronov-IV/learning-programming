using Microsoft.AspNetCore.Identity;

namespace emptyproject
{
    /// <summary>
    /// An instance containing all Person List REST business logic.
    /// <br />
    /// Объект, содержащий всю REST логику списка пользователей.
    /// </summary>
    public class PersonListBusinessLogic
    {


        #region Data Access



        /// <inheritdoc cref="PersonList"/>
        private List<Person> _personList;


        /// <summary>
        /// A list of people.
        /// <br />
        /// Список пользователей.
        /// </summary>
        public List<Person> PersonList
        {
            get { return _personList; }
            set { _personList = value; }
        }



        #endregion Data Access




        #region Logic



        /// <summary>
        /// Send whole user list json response.
        /// <br />
        /// Отправить json всего списка в response.
        /// </summary>
        public async Task GetAllPeopleAsync(HttpResponse response)
        {
            await response.WriteAsJsonAsync(PersonList);
        }



        /// <summary>
        /// Get a person by id.
        /// <br />
        /// Получить пользователя по id.
        /// </summary>
        public async Task GetPersonAsync(string id, HttpResponse response)
        {
            Person? user = PersonList.FirstOrDefault(u => u.Id.Equals(id));

            if (user is not null) await response.WriteAsJsonAsync(user);

            else WriteUserNotFOundMessageAsync(response);
        }



        /// <summary>
        /// Delete a person by id.
        /// <br />
        /// Удалить пользователя по id.
        /// </summary>
        public async Task DeletePersonAsync(string id, HttpResponse response)
        {
            Person? user = PersonList.FirstOrDefault(u => u.Id.Equals(id));

            if (user is not null)
            {
                PersonList.Remove(user);

                await response.WriteAsJsonAsync(user);
            }

            else WriteUserNotFOundMessageAsync(response);
        }



        /// <summary>
        /// Create a new person in the list.
        /// <br />
        /// Создать нового пользователя в списке.
        /// </summary>
        public async Task CreatePersonAsync(HttpResponse response, HttpRequest request)
        {
            try
            {
                var user = await request.ReadFromJsonAsync<Person>();

                if (user is not null)
                {
                    user.Id = Guid.NewGuid().ToString();

                    PersonList.Add(user);

                    await response.WriteAsJsonAsync(user);
                }

                else throw new InvalidDataException("[Manual] Invalid input data.");
            }

            catch(InvalidDataException ex)
            {
                await WriteIncorrectDataMessageAsync(response);
            }
        }



        /// <summary>
        /// Update person data.
        /// <br />
        /// Изменить пользовательские данные.
        /// </summary>
        public async Task UpdatePersonAsync(HttpResponse response, HttpRequest request) 
        {
            try
            {
                Person? userData = await request.ReadFromJsonAsync<Person>();

                if (userData is not null)
                {
                    var user = PersonList.FirstOrDefault(p => p.Id.Equals(userData.Id));

                    if (user is not null)
                    {
                        user.Age = userData.Age;
                        user.Name = userData.Name;
                        await response.WriteAsJsonAsync(user);
                    }

                    else await WriteUserNotFOundMessageAsync(response);
                }

                else throw new InvalidDataException("[Manual] Invalid input data.");
            }

            catch (InvalidDataException ex)
            {
                await WriteIncorrectDataMessageAsync(response);
            }
        }





        /// <summary>
        /// Write message about specified person search failure to the response.
        /// <br />
        /// Записать сообщение о нудачном поиске заданного пользователя в response.
        /// </summary>
        private async Task WriteUserNotFOundMessageAsync(HttpResponse response)
        {
            response.StatusCode = 404;

            await response.WriteAsJsonAsync(new { message = "User not found." });
        }


        /// <summary>
        /// Write message about incorrect input data to the response.
        /// <br />
        /// Записать сообщение о некорректном вводе в response.
        /// </summary>
        private async Task WriteIncorrectDataMessageAsync(HttpResponse response)
        {
            response.StatusCode = 400;

            await response.WriteAsJsonAsync(new {message = "Incorrect data."} );
        }



        #endregion Logic




        #region Construction


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public PersonListBusinessLogic()
        {
            PersonList = new();
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public PersonListBusinessLogic(List<Person> userList)
        {
            PersonList = userList;
        }


        #endregion Construction

    }
}
