using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFRibbonWorkplaceContracts;

namespace WPFMeetingsWorkplacePlugin.Plugins.ContactsPlugin.Functions
{
    class Save : BaseFunction, IFunction
    {
        private const string NewName = "Save";
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

        public Save(ContactsVM vm)
        {
            VM = vm;
        }

        void IFunction.Method()
        {
            VM.Save(); ;
            //VM.AddNew();
            //VM.ClearFields();
        }
    }
}
