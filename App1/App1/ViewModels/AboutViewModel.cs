using App1.Models;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using System;
using App1.Helpers;
using System.IO;

namespace App1.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ObservableCollection<Appointment> Items { get; set; } = new ObservableCollection<Appointment>();
        public Command<Appointment> ItemTapped { get; }

        public async void OnAppearing()
        {
            Items.Clear();
            var fb = new FireBaseAppointmentHelper();
            var persons = await fb.GetAllPersons();
            if (persons.Count == 0)
            {
                await fb.AddPerson("1", "", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 4, 30, 0), "", "haircut");
                await fb.AddPerson("2", "", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 30, 0), "", "haircut");
                await fb.AddPerson("3", "", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 30, 0), "", "haircut");
                foreach (var item in persons)
                {
                    if (item.Name == "")
                    {
                        item.Name = "ΑΝΟΙΧΤΗ ΘΕΣΗ";
                        item.Phone = "Barber Phone";
                    }
                    Items.Add(item);
                }
            }
        }

        public AboutViewModel()
        {
            ItemTapped = new Command<Appointment>(OnItemSelected);
        }
        public async void LoadItems()
        {
            Items.Clear();
            var fb = new FireBaseAppointmentHelper();
            var persons = await fb.GetAllPersons();
            var personId = Preferences.Get("UserID", "");
            if (personId == "xli4nmKr66NPDeKIvkqF6qBrbVL2")
            {
                foreach (var person in persons)
                {
                    if (person.Image != "haircut")
                    {
                        var img = Convert.FromBase64String(person.Image);
                        Stream stream = new MemoryStream(img);
                        person.ImageByte = ImageSource.FromStream(() => { return stream; });
                    }
                    else
                    {
                        person.Image = "haircut";
                    }
                    Items.Add(person);
                }
            }
            else
            {
                foreach (var item in persons)
                {
                    if (item.Name == "")
                    {
                        item.Name = "ΑΝΟΙΧΤΗ ΘΕΣΗ";
                        item.Image = "haircut";
                        item.Phone = "Barber Phone";

                        Items.Add(item);
                    }
                }
            }
        }

        async void OnItemSelected(Appointment item)
        {
            if (item == null)
                return;
            var fb = new FireBaseAppHelper();
            var personId = Preferences.Get("UserID", "");
            var t = await fb.GetPerson(personId);
            if (item.Name == "ΑΝΟΙΧΤΗ ΘΕΣΗ")
            {
                item.Image = t.Image;
                item.Name = t.Name;
                item.Phone = t.Phone;
                var tb = new FireBaseAppointmentHelper();
                await tb.UpdatePerson(item.AppId, item.Name, item.Phone, item.Created, item.Image);
                LoadItems();
            };
        }
    }
}