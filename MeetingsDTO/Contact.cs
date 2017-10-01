using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsDTO
{
    public class Contact
    {
        public int? ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Name
        {
            get
            {
                return String.Concat(FirstName, " ", LastName);
            }
        }
    }
}
