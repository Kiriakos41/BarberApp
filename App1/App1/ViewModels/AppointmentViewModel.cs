using App1.Models;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using System;
using App1.Helpers;
using System.IO;

namespace App1.ViewModels
{
    public class AppointmentViewModel : BaseViewModel
    {
        private DateTime daymonth = DateTime.Now;
        private TimeSpan time = TimeSpan.Zero;
        public Command Save { get; }

        public DateTime DayMonth
        {
            get => daymonth;
            set => SetProperty(ref daymonth, value);
        }
        public TimeSpan Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }

        public DateTime AppointmentDate => Convert.ToDateTime($"{DayMonth:yyyy-MM-dd} {Time:g}");

        public AppointmentViewModel()
        {
            Save = new Command(onSave);
        }

        public async void onSave()
        {
            var fb = new FireBaseAppointmentHelper();
            var persons = await fb.GetAllPersons();
            await fb.AddPerson(Guid.NewGuid().ToString(), "", AppointmentDate, "", "haircut");
        }
    }
}
