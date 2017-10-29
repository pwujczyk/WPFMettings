using DateProviderContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateProviderSimple
{
    public class DateProvider : IDateProvider
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
