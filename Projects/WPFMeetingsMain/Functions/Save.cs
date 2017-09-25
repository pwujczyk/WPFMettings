using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFRibbonWorkplaceContracts;

namespace WPFMeetingsWorkplacePlugin.Functions
{
    public class Save : BaseFunction, IFunction
    {
        MetingsMainWindowVM VM; 
        private const string AddName = "Save";
        private const string AddSection = "Basic";
        private const string AddTab = "Basic";

        public string FunctionName
        {
            get
            {
                return AddName;
            }
        }

        public string SectionName
        {
            get
            {
                return AddSection;
            }
        }

        public string TabName
        {
            get
            {
                return AddTab;
            }
        }

        public Save(MetingsMainWindowVM vm)
        {
            this.VM = vm;
        }

        public void Method()
        {
            this.VM.SaveMeeting();
        }
    }
}
