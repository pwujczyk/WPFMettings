using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateProviderContract
{
    public interface IDateProvider
    {
        DateTime Now { get; }
    }
}
