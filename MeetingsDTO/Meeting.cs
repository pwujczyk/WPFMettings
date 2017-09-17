using DateProviderContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsDTO
{
    public class Meeting
    {
        public int? MeetingId { get; set; }
        public DateTime Date { get; set; }
        public string BeforeNotes { get; set; }
        public string DuringNotes { get; set; }
        public string AfterNotes { get; set; }

        public string DateFormated
        {
            get
            {
                return this.Date.ToString("yyy-MM-dd hh:mm");
            }
        }

        public Meeting()
        {
     
        }

        public Meeting(bool fillvalues):this()
        {
            IDateProvider date = IoCManager.IoCManager.GetSinglenstance<IDateProvider>("DateProvider");
            this.Date = date.Now;
        }
    }
}
