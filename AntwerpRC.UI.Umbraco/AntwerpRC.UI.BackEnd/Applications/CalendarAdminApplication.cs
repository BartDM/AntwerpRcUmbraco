using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco.businesslogic;
using umbraco.interfaces;

namespace AntwerpRC.UI.BackEnd.Applications
{
    [Application("CalendarAdmin", "Calendar Admin", ".traydeveloper", sortOrder: 7)]
    public class CalendarAdminApplication : IApplication
    {
    }

/*
    [Application("developer", "Developer", ".traydeveloper", sortOrder: 3)]
    public class DeveloperApplicationDefinition : IApplication
    { }
*/
}