using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using TravelAgentTim19.View;

namespace HelpSistem
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class JavaScriptControlHelper
    {
        AgentMainWindow prozor;
        public JavaScriptControlHelper(AgentMainWindow w)
        {
            prozor = w;
        }

        
    }
}
