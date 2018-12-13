﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NTUEvents.Models
{
    public class Cca
    {
        public Cca()
        {
            Ccamembership = new HashSet<CcaMembership>();
            Event = new HashSet<Event>();
        }

        [Key]
        public int CcaId { get; set; }
        public string CcaType { get; set; }
        public string Description { get; set; }
        public string Schedule { get; set; }
        public string Venue { get; set; }
        public string Contact { get; set; }
        public int? UserIdCcaFk { get; set; }

        public User UserIdCcaFkNavigation { get; set; }
        public ICollection<CcaMembership> Ccamembership { get; set; }
        public ICollection<Event> Event { get; set; }
    }
}
