using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLocal.Tables
{
    public class Meeting
    {
        public int MeetingId { get; set; }
        public DateTime Date { get; set; }
        public string BeforeNotes { get; set; }
        public string DuringNotes { get; set; }
        public string AfterNotes { get; set; }
    }
}
