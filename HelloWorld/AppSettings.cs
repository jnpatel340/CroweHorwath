using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    /// <summary>
    /// Contains the Application Settings
    /// </summary>
    public class AppSettings : ConfigurationSection
    {

        private static AppSettings _settings = ConfigurationManager.GetSection("Settings") as AppSettings;

        public static AppSettings Settings
        {
            get { return _settings; }
        }
        /// <summary>
        /// Returns the Configured OutputDevice Type
        /// </summary>
        [ConfigurationProperty("OutputDevice", DefaultValue = "", IsRequired = false)]
        public string OutputDevice
        {
            get { return (this["OutputDevice"].ToString()); }
            set { this["OutputDevice"] = value; }
        }

    }
}
