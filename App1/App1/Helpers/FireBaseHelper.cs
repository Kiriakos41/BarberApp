using System.Collections.Generic;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using App1.Models;


namespace App1.Helpers
{
    public class FireBaseHelper
    {

        public static string FirebaseClient = "https://barberdb-22630-default-rtdb.europe-west1.firebasedatabase.app/";
        public static string FrebaseSecret = "K7i1aQ8RBFPsNHg65c6smhpSqlKYg4aG5VsLeQGb";

        public FirebaseClient firebase = new FirebaseClient(FirebaseClient,
                           new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FrebaseSecret) });

        public async Task<List<Person>> GetAllPersons()
        {
            return (await firebase
                .Child("Person")
                .OnceAsync<Person>()).Select(item => new Person
                {
                    Name = item.Object.Name,
                    PersonId = item.Object.PersonId,
                }).ToList();
        }

        public async Task AddPerson(string personId, string name)
        {
            await firebase
                .Child("Person")
                .PostAsync(new Person() { PersonId = personId, Name = name });
        }

        public async Task<Person> GetPerson(string personId)
        {
            var allPersons = await GetAllPersons();
            await firebase
                .Child("Persons")
                .OnceAsync<Person>();
            return allPersons.Where(a => a.PersonId == personId).FirstOrDefault();
        }

        public async Task UpdatePerson(string personId, string name)
        {
            var toUpdatePerson = (await firebase
                .Child("Persons")
                .OnceAsync<Person>()).Where(a => a.Object.PersonId == personId).FirstOrDefault();

            await firebase
                .Child("Persons")
                .Child(toUpdatePerson.Key)
                .PutAsync(new Person() { PersonId = personId, Name = name });
        }

        public async Task DeletePerson(string personId)
        {
            var toDeletePerson = (await firebase
                .Child("Persons")
                .OnceAsync<Person>()).Where(a => a.Object.PersonId == personId).FirstOrDefault();
            await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();
        }
    }
}
