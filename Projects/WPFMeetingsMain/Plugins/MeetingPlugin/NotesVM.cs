using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMeetingsWorkplacePlugin.Plugins.MeetingPlugin
{
    public class NotesVM : VMBase, INotifyPropertyChanged
    {

        private MeetingsDTO.Notes note;
        public MeetingsDTO.Notes Note
        {
            get
            {
                return this.note;
            } set
            {
                this.note = value;
                NotifyPropertyChanged(nameof(Note));
            }
        }

        internal void BulletChanged()
        {
            NotifyPropertyChanged(nameof(Note));
           // NotifyPropertyChanged("Note.SelectedText");
        }
    }
}
