using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JazzVersion
{
    /// <summary>determines if a new version of the application is available
    /// <para></para>
    /// </summary>
    public static class VersionUtil
    {
        #region Member variables

        /// <summary>Input data</summary>
        static private VersionInput m_version_input = null;

        #endregion // Member variables

        /// <summary>Initialization
        /// <para></para>
        /// </summary>
        /// <param name="i_version_input">Input data for version check</param>
        /// <param name="o_error">Error message</param>
        static public bool Init(VersionInput i_version_input, out string o_error)
        {
            bool ret_init = true;
            o_error = "";

            if (!_CheckInput(i_version_input, out o_error))
            {
                return false;
            }

            m_version_input = i_version_input;

            return ret_init;

        } // Init


        /// <summary>Checks if a new version for the application is available
        /// <para>1. Gets the major and minor version of the running application.</para>
        /// <para>2. Downloads the VersionInfo file from the server and gets the major and minor version</para>
        /// <para>3. Compares the versions major and minor</para>
        /// <para>There is remind message (error) that the programmer must change name of </para>
        /// <para>the VersionInfo file before the release of a new version</para>
        /// </summary>
        /// <param name="o_new_version">True if a newer version is available</param>
        /// <param name="o_version">The version that is available for download</param>
        /// <param name="o_error">Error description</param>
        static public bool NewVersionIsAvailable(out bool o_new_version, out string o_version, out string o_error)
        {
            o_new_version = false;
            o_version = @"";
            o_error = @"";

            int major = -12345;
            int minor = -12345;
            string error_message = @"";

            if (!GetMajorMinorVersion(out major, out minor, out error_message))
            {
                o_error = @"VersionUtil.NewVersionIsAvailable Programming error: " + error_message;
                return false;
            }

            int major_new = -12345;
            int minor_new = -12345;

            DownLoad down_load = new DownLoad();

            if (!down_load.DownloadVersionInfoFile(m_version_input, out major_new, out minor_new, out error_message))
            {
                o_error = @"VersionUtil.GetApplicationVersion Programming error: " + error_message;
                return false;
            }

            o_version = major_new.ToString() + "." + minor_new.ToString();

            if (major_new < major)
            {
                o_error = m_version_input.MsgUpdateVersionInfoFileOnServer + @"major_new < major";
                return false;
            }

            if (major_new > major)
            {
                o_new_version = true;
                return true;
            }

            if (minor_new < minor)
            {
                o_error = m_version_input.MsgUpdateVersionInfoFileOnServer + @"minor_new < minor";
                return false;
            }

            if (minor_new > minor)
            {
                o_new_version = true;
                return true;
            }

            return true;
        } // NewVersionIsAvailable


        /// <summary>Returns the current version
        /// <para>1. Gets the major and minor version of the running application.</para>
        /// </summary>
        /// <param name="o_version">The current version</param>
        /// <param name="o_error">Error description</param>
        static public bool GetCurrentVersion(out string o_version, out string o_error)
        {
            o_version = @"";
            o_error = @"";

            int major = -12345;
            int minor = -12345;
            string error_message = @"";

            if (!GetMajorMinorVersion(out major, out minor, out error_message))
            {
                o_error = @"VersionUtil.NewVersionIsAvailable Programming error: " + error_message;
                return false;
            }

            o_version = major.ToString() + @"." + minor.ToString();

            return true;
        } // GetCurrentVersion


        /// <summary>Returns the major minor version as integers</summary>
        static private bool GetMajorMinorVersion(out int o_major, out int o_minor, out string o_error)
        {
            o_error = "";
            o_major = -12345;
            o_minor = -12345;

            string version_str = m_version_input.VersionString;

            int index_sep = version_str.IndexOf('.');
            if (0 == index_sep)
            {
                o_error = "VersionUtil.GetMajorMinorVersion Programming error: No . (point for major) in the version string ";
                return false;
            }

            string major_str = version_str.Substring(0, index_sep);

            string rest_string = version_str.Substring(index_sep + 1);

            int index_sep_one = rest_string.IndexOf('.');
            if (0 == index_sep_one)
            {
                o_error = "VersionUtil.GetMajorMinorVersion Programming error: No . (point for minor one) in the version string ";
                return false;
            }

            string minor_str = rest_string.Substring(0, index_sep_one);

            if (!Int32.TryParse(major_str, out o_major))
            {
                o_error = "DownloadVersionInfoFile Programming error: Converting major string to int failed ";
                return false;
            }
            if (!Int32.TryParse(minor_str, out o_minor))
            {
                o_error = "DownloadVersionInfoFile Programming error: Converting minor string to int failed ";
                return false;
            }

            return true;
        } // GetMajorMinorVersion		

        #region Private functions

        /// <summary>Check the input data object</summary>
        static private bool _CheckInput(VersionInput i_version_input, out string o_error)
        {
            o_error = "";

            if (null == i_version_input)
            {
                o_error = "VersionUtil._CheckInput Programming error: Input object is null (Init has not been called)";
                return false;
            } // CheckInput

            string err_msg_input = @"";
            if (!i_version_input.CheckInput(out err_msg_input))
            {
                o_error = err_msg_input;
                return false;
            }

            return true;

        } // _CheckInput

        #endregion // Private functions

    } // Version
} // namespace
