using System.Collections.Generic;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using App1.Models;
using System;

namespace App1.Helpers
{
    public class FireBaseAppHelper
    {
        public static string FirebaseClient = "https://barberdb-22630-default-rtdb.europe-west1.firebasedatabase.app/";
        public static string FrebaseSecret = "K7i1aQ8RBFPsNHg65c6smhpSqlKYg4aG5VsLeQGb";

        public FirebaseClient firebase = new FirebaseClient(FirebaseClient,
                           new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FrebaseSecret) });

        public async Task<List<User>> GetAllPersons()
        {
            return (await firebase
                .Child("User")
                .OnceAsync<User>()).Select(item => new User
                {
                    Name = item.Object.Name,
                    UserId = item.Object.UserId,
                    Email = item.Object.Email,
                    Phone = item.Object.Phone,
                    Image = item.Object.Image
                }).ToList();
        }

        public async Task AddPerson(string personId, string name, string email, string phone, string Image)
        {
            await firebase
                .Child("User")
                .PostAsync(new User() { UserId = personId, Name = name, Email = email, Phone = phone, Image = Image });
        }

        public async Task<User> GetPerson(string userid)
        {
            var allAppointment = await GetAllPersons();
            await firebase
                .Child("User")
                .OnceAsync<User>();
            return allAppointment.Where(a => a.UserId == userid).FirstOrDefault();
        }

        public async Task UpdatePerson(string userid, string name, string email, string phone, string image)
        {
            var toUpdatePerson = (await firebase
                .Child("User")
                .OnceAsync<User>()).Where(a => a.Object.UserId == userid).FirstOrDefault();

            await firebase
                .Child("User")
                .Child(toUpdatePerson.Key)
                .PutAsync(new User() { UserId = userid, Name = name, Email = email, Phone = phone, Image = image });
        }

        public async Task DeletePerson(string userid)
        {
            var toDeletePerson = (await firebase
                .Child("User")
                .OnceAsync<User>()).Where(a => a.Object.UserId == userid).FirstOrDefault();
            await firebase.Child("User").Child(toDeletePerson.Key).DeleteAsync();
        }
    }
}
