using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class Appointment
    {
        public string AppId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
    }
}
