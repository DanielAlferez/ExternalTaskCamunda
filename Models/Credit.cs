using System.ComponentModel.DataAnnotations;

namespace MsgFoundation.Models
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Pago { get; set; }
        public string Enfermedad { get; set; }
        public string Proveedor { get; set; }
        public string Forma { get; set; }
        public string Material { get; set; }
    }
}
    