using DateProviderContract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string BeforeNotesXaml { get; set; }
        public string DuringNotesXaml { get; set; }
        public string AfterNotesXaml { get; set; }
        public ObservableCollection<Contact> Contacts { get; set; }
        public string Subject { get; set; }

        private string DateFormated
        {
            get
            {
                return this.Date.ToString("yyy-MM-dd hh:mm");
            }
        }

        public string MeetingTitle
        {
            get
            {
                return string.Format($"{DateFormated} [{Subject}]");
            }
        }

        public Meeting()
        {
            this.Contacts = new ObservableCollection<Contact>();
        }

        public Meeting(bool fillvalues):this()
        {
            IDateProvider date = IoCManager.IoCManager.GetSinglenstance<IDateProvider>("DateProvider");
            this.Date = date.Now;
        }
    }
}
