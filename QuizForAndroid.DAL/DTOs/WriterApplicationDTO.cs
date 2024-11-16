using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizForAndroid.DAL.DTOs
{
    public class WriterApplicationDTO
    {
        public int ApplicationID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; } // 'Pending', 'Accepted', 'Rejected'
        public int? ReviewedBy { get; set; } // AdminID who reviewed
        public DateTime? ReviewedDate { get; set; }
        public string RejectionReason { get; set; }
    }

}
