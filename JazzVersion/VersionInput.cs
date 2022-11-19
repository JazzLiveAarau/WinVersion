using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JazzVersion
{
    /// <summary>Input data for the class Version
    /// <para>Connection, file and folder data must be set</para>
    /// <para>Version file strings, messages and error messages have default strings</para>
    /// </summary>
    public class VersionInput
    {
        #region Connection data

        /// <summary>FTP host</summary>
        private string m_ftp_host = "www.jazzliveaarau.ch";

        /// <summary>Get or set the FTP host</summary>
        public string FtpHost
        { get { return m_ftp_host; } set { m_ftp_host = value; } }

        /// <summary>FTP user</summary>
        private string m_ftp_user = "jazzliv1";

        /// <summary>Get or set the FTP user</summary>
        public string FtpUser
        { get { return m_ftp_user; } set { m_ftp_user = value; } }

        /// <summary>FTP password for the download of the version info file</summary>
        private string m_ftp_password = "42SN4bX9";

        /// <summary>Get or set the FTP password</summary>
        public string FtpPassword
        { get { return m_ftp_password; } set { m_ftp_password = value; } }

        #endregion // Connection data

        #region File and folder data

        /// <summary>Name of the server directory for the version info file</summary>
        private string m_server_directory = @"/setup/Newsletter/LatestVersionInfo/";

        /// <summary>Get or set the name of the server directory for the version info file</summary>
        public string ServerDirectory
        { get { return m_server_directory; } set { m_server_directory = value; } }

        /// <summary>Name of the local (exe) directory for the version info file</summary>
        private string m_local_directory = @"LatestVersionInfo";

        /// <summary>Get or set the name of the local directory for the version info file</summary>
        public string LocalDirectory
        { get { return m_local_directory; } set { m_local_directory = value; } }

        /// <summary>Path to the exe directory.</summary>
        private string m_exe_directory = @"";

        /// <summary>Get or set the name of the exe directory</summary>
        public string ExeDirectory
        { get { return m_exe_directory; } set { m_exe_directory = value; } }

        /// <summary>Version string</summary>
        private string m_version_string = @"";

        /// <summary>Get or set the version string
        /// <para>Use System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()</para>
        /// </summary>
        public string VersionString
        { get { return m_version_string; } set { m_version_string = value; } }

        // string version_str
        #endregion // File and folder data

        #region Messages

        /// <summary>Message: Version info file is downloaded</summary>
        private string m_msg_version_info_downloaded = @"Versionsdatei ist heruntergeladen";

        /// <summary>Get or set the message: Data have been checked out </summary>
        public string MsgDataAreCheckedOut
        { get { return m_msg_version_info_downloaded; } set { m_msg_version_info_downloaded = value; } }

        /// <summary>Message: Update the version info file on the server before release</summary>
        private string m_msg_update_version_info_file_on_server = @"Warnung: Ändere der Version Info Datei Name beim Release: ";

        /// <summary>Get or set the message: Update the version info file on the server before release</summary>
        public string MsgUpdateVersionInfoFileOnServer
        { get { return m_msg_update_version_info_file_on_server; } set { m_msg_update_version_info_file_on_server = value; } }

        // MsgUpdateVersionInfoFileOnServer



        #endregion // Messages

        #region Error messages

        /// <summary>Error message: No connection to Internet is available</summary>
        private string m_error_msg_no_internet_connection = @"Keine Verbindung zu Internet ist vorhanden";

        /// <summary>Get or set the error message: No connection to Internet is available </summary>
        public string ErrMsgNoInternetConnection
        { get { return m_error_msg_no_internet_connection; } set { m_error_msg_no_internet_connection = value; } }

        /// <summary>Error message: Failure downloading version info file</summary>
        private string m_error_failure_downloading_version_info_file = @"Die Versinsinformationsdatei ist nicht heruntergeladen";

        /// <summary>Get or set the error message: Failure downloading version info file</summary>
        public string ErrMsgFailureDownloadingVersionInfoFile
        { get { return m_error_failure_downloading_version_info_file; } set { m_error_failure_downloading_version_info_file = value; } }

        #endregion // Error messages

        /// <summary>Check the input data</summary>
        public bool CheckInput(out string o_error)
        {
            bool ret_check = true;
            o_error = @"";

            if (m_exe_directory.Trim().Equals(@""))
            {
                o_error = @"VersionInput.CheckInput Programming error: Exe directory is not set";
                return false;
            }

            if (m_version_string.Trim().Equals(@""))
            {
                o_error = @"VersionInput.CheckInput Programming error: Version string is not set";
                return false;
            }


            return ret_check;

        } // CheckInput


    } // VersionInput
} // namespace
