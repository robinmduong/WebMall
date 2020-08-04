using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sabio.Models.Domain.ScheduleAvailability
{
    public class ScheduleAvailabilityUpdateRequest: ScheduleAvailabilityAddRequest, IModelIdentifier
    {
        [Required (ErrorMessage = "Id is required")]
        [Range(1, Int32.MaxValue)]
        public int Id { get; set; }
    }
}
