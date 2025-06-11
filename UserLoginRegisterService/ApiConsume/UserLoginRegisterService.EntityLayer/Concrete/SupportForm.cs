using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLoginRegisterService.EntityLayer.Concrete
{

    public class SupportForm : BaseEntity
    {
        [Key]
        public int SupportFormID { get; set; } 
        public int UserId { get; set; }
        public string Subject { get; set; } // Formun konusu
        public string Message { get; set; } // Formun mesajı
        public SupportFormStatus Status { get; set; } // Formun durumu (işlem yapıldı, yapılıyor, silindi)
    }

    public enum SupportFormStatus
    {
        Processing = 0, // İşlem yapılıyor
        Processed = 1,  // İşlem yapıldı
        Deleted = 2     // Silindi
    }
}
