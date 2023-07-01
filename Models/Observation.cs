using System.ComponentModel.DataAnnotations;

namespace MsgFoundation.Models
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime AppointmentDateTime { get; set; }

    }
}
