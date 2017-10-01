using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFRibbonWorkplaceContracts;

namespace WPFMeetingsWorkplacePlugin.Plugins.ContactsPlugin.Functions
{
    class New : BaseFunction, IFunction
    {

        private const string NewName = "New";
        private const string NewSection = "Contact";
        private const string NewTab = "Contact";

        ContactsVM VM;

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

        public New(ContactsVM vm)
        {
            VM = vm;
        }

        void IFunction.Method()
        {
            VM.AddNew();
            //VM.AddNew();
            //VM.ClearFields();
        }
    }
}
