using DALDataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Contact
{
    public class ContactManager
    {
        DALDataManager manager;
        DALDataManager Manager
        {
            get
            {
                if (manager==null)
                {
                    manager = new DALDataManager();
                }
                return manager;
            }
        }

        public List<MeetingsDTO.Contact> GetContacts()
        {
            return manager.GetContacts();
        }

        public void SaveContact(MeetingsDTO.Contact contact)
        {

        }
    }
}
