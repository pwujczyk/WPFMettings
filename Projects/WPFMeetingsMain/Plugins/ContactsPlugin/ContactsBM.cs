using BusinessLogic.Contact;
using BusinessLogic.Meeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMeetingsWorkplacePlugin.Plugins.ContactsPlugin
{
    public class ContactsBM
    {
        ContactManager manager = new ContactManager();
        public List<MeetingsDTO.Contact> GetContacts()
        {
            return manager.GetContacts();
        }

        public void SaveContact()
        {

        }
    }
}
