using App1.Models;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using System;
using App1.Helpers;
using System.IO;
using System.Linq;

namespace App1.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ObservableCollection<Appointment> Items { get; set; } = new ObservableCollection<Appointment>();
        public Command<Appointment> ItemTapped { get; }

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
            var b = new FireBaseAppHelper();
            var pr = Preferences.Get("UserID", "");
            var t = await b.GetPerson(pr);
            if (personId == "xli4nmKr66NPDeKIvkqF6qBrbVL2")
            {
                foreach (var person in persons)
                {
                    if (person.Created >= DateTime.Now)
                    {
                        if (person.Image != "haircut")
                        {
                            var img = Convert.FromBase64String(person.Image);
                            Stream stream = new MemoryStream(img);
                            person.ImageByte = ImageSource.FromStream(() => { return stream; });
                        }
                        Items.Add(person);
                    }
                }
            }
            else
            {
                foreach (var item in persons)
                {
                    bool ok = false;
                    if (item.Name == t.Name && item.Created >= DateTime.Now)
                    {
                        var img = Convert.FromBase64String(item.Image);
                        Stream stream = new MemoryStream(img);
                        item.ImageByte = ImageSource.FromStream(() => { return stream; });
                        Items.Add(item);
                        ok = true;
                    }
                    else if (ok)
                    {
                        if (item.Created >= DateTime.Now)
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