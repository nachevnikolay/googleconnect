using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;

namespace GoogleConnect
{
    public class Utility
    {

        #region HelpFunctions
        public string GetFullPath(string filePath)
        {
            string solutionParentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            return Path.Combine(solutionParentDirectory, filePath);
        }


        public enum Browser
        {
            Chrome = 1,
            Firefox,
            InternetExplorer
        }

        /// <summary>
        /// Read in a key - value from the test configuration settings
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReadConfigurationSetting(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            return appSettings[key];
        }

        #endregion
    }
}
