using Sabio.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sabio.Models
{
    public class ScheduleAvailability
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int CreatedBy;
    }
}
