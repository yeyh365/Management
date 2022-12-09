using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace ManagementApi.Helper
{
    public class IOHelper
    {
        string pUrl = ConfigurationManager.AppSettings["reslturl"].ToString();
    }
}