using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace dieuxe.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        //#region Setting Constants

        //private const string SettingsKey = "settings_key";
        //private static readonly string SettingsDefault = string.Empty;

        //#endregion
        public static string email
        {
            get
            {
                return AppSettings.GetValueOrDefault("email", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("email", value);
            }
        }
        public static string password
        {
            get
            {
                return AppSettings.GetValueOrDefault("password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("password", value);
            }
        }

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessToken", value);
            }
        }
        public static string LienHeID
        {
            get
            {
                return AppSettings.GetValueOrDefault("LienHeID", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("LienHeID", value);
            }
        }
        public static string TenLienHe
        {
            get
            {
                return AppSettings.GetValueOrDefault("TenLienHe", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("TenLienHe", value);
            }
        }
        public static string LoailienHe
        {
            get
            {
                return AppSettings.GetValueOrDefault("LoailienHe", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("LoailienHe", value);
            }
        }
        public static string SdtLienLac
        {
            get
            {
                return AppSettings.GetValueOrDefault("SdtLienLac", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("SdtLienLac", value);
            }
        }
        public static string BoPhan
        {
            get
            {
                return AppSettings.GetValueOrDefault("BoPhan", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("BoPhan", value);
            }
        }
        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault("Email ", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Email ", value);
            }
        }
        public static string chucvu
        {
            get
            {
                return AppSettings.GetValueOrDefault("chucvu", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("chucvu", value);
            }
        }
    }
}
