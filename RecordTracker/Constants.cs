using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWpf
{
    public static class Constants
    {
        public static string GetDbFilePath()
        {
            return ConfigurationManager.AppSettings["DbFilePath"];
        }
    }
}
