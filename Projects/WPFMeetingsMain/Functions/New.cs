using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFRibbonWorkplaceContracts;

namespace WPFMeetingsWorkplacePlugin.Functions
{
    class New : IFunction
    {
            
        private const string NewName = "New";
        private const string NewSection = "Basic";
        private const string NewTab = "Basic";

        MetingsMainWindowVM VM;

        string IFunction.FunctionName
        {
            get
            {
                return NewName;
            }
        }

        string IFunction.SectionName
        {
            get
            {
                return NewSection;
            }
        }

        string IFunction.TabName
        {
            get
            {
                return NewTab;
            }
        }

        public New(MetingsMainWindowVM vm)
        {
            VM = vm;
        }

        void IFunction.Method()
        {
           
            VM.AddNew();
            VM.ClearFields();
        }
    }
}
