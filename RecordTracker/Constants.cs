﻿using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTracker
{
    public static class Constants
    {
        //private static Logger _logger = LogManager.GetCurrentClassLogger();
        public static string GetDbFilePath()
        {

           // _logger.Info("DB file path set = " + ConfigurationManager.AppSettings["DbFilePath"]);
            return "MainApplication.db";
        }
    }
}