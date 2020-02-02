using Indeed.Test.Models;
using Indeed.Test.Models.Requests;
using Indeed.Test.Models.Workers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Indeed.Test.DataAccess
{
    internal static class Context
    {
        internal static bool isActive = false;

        internal static List<Worker> Workers;
        internal static List<Request> Requests;
        internal static Settings Settings;

    }
}
