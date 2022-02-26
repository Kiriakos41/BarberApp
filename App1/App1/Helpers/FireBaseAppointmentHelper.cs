using System.Collections.Generic;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using App1.Models;
using System;
namespace App1.Helpers
{
    public class FireBaseAppointmentHelper
    {
        public static string FirebaseClient = "https://barberdb-22630-default-rtdb.europe-west1.firebasedatabase.app/";
        public static string FrebaseSecret = "K7i1aQ8RBFPsNHg65c6smhpSqlKYg4aG5VsLeQGb";

        public FirebaseClient firebase = new FirebaseClient(FirebaseClient,
                           new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FrebaseSecret) });

        public async Task<List<Appointment>> GetAllPersons()
        {
            return (await firebase
                .Child("Appointment")
                .OnceAsync<Appointment>()).Select(item => new Appointment
                {
                    Name = item.Object.Name,
                    AppId = item.Object.AppId,
                    Created = item.Object.Created,
                    Phone = item.Object.Phone,
                }).ToList();
        }

        public async Task AddPerson(string personId, string name, DateTime Created, string phone)
        {
            await firebase
                .Child("Appointment")
                .PostAsync(new Appointment() { AppId = personId, Name = name, Created = Created, Phone = phone});
        }

        public async Task<Appointment> GetPerson(string userid)
        {
            var allAppointment = await GetAllPersons();
            await firebase
                .Child("Appointment")
                .OnceAsync<Appointment>();
            return allAppointment.Where(a => a.AppId == userid).FirstOrDefault();
        }

        public async Task UpdatePerson(string userid, string name, string phone, DateTime Created)
        {
            var toUpdatePerson = (await firebase
                .Child("Appointment")
                .OnceAsync<Appointment>()).Where(a => a.Object.AppId == userid).FirstOrDefault();

            await firebase
                .Child("Appointment")
                .Child(toUpdatePerson.Key)
                .PutAsync(new Appointment() { AppId = userid, Name = name, Phone = phone, Created = Created});
        }

        public async Task DeletePerson(string userid)
        {
            var toDeletePerson = (await firebase
                .Child("Appointment")
                .OnceAsync<Appointment>()).Where(a => a.Object.AppId == userid).FirstOrDefault();
            await firebase.Child("Appointment").Child(toDeletePerson.Key).DeleteAsync();
        }
    }
}
