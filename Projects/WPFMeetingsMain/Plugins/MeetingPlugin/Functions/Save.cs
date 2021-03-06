﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFMeetingsWorkplacePlugin.Plugins.MeetingPlugin.Meetings;
using WPFRibbonWorkplaceContracts;

namespace WPFMeetingsWorkplacePlugin.Plugins.MeetingPlugin.Functions
{
    public class Save : BaseFunction, IFunction
    {
        MetingsVM VM; 
        private const string AddName = "Save";
        private const string AddSection = "Meeting";
        private const string AddTab = "Meeting";

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

        public Type ButtonType
        {
            get
            {
                return typeof(Button);
            }
        }

        public Save(MetingsVM vm)
        {
            this.VM = vm;
        }

        public void Method()
        {
            this.VM.SaveMeeting();
        }
    }
}
