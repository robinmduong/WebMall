using Sabio.Models.Domain.ScheduleAvailability;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sabio.Models.Domain
{
    public class ScheduleAvailabilityAddRequest
    {
        [Required(ErrorMessage = "ScheduleId is required")]
        [Range(1, Int32.MaxValue)]
        public int ScheduleId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

    }
}
