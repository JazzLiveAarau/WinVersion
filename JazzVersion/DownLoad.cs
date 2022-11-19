using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JazzVersion
{
    class DownLoad
    {
        /// <summary>Download the VersionInfo file from the server with FTP
        /// <para>It is recommended that the calling function checks if there is Internet connection</para>
        /// <para>1. The file is downloaded with function Getfiles in class Ftp.DownLoad.</para>
        /// <para>2. The file is saved in a subdirectory (LatestVersionInfo) to the exe directory.</para>
        /// </summary>
        /// <param name="o_major">Major version</param>
        /// <param name="o_minor">Minor version</param>
        /// <param name="o_error">Error description</param>
        public bool DownloadVersionInfoFile(VersionInput i_version_input, out int o_major, out int o_minor, out string o_error)
        {
            o_error = "";

            o_major = -12345;
            o_minor = -12345;

            /*QQQQQ Does not always work check must be done by the calling function with JazzFtp function
            string str_uri = @"http://" + i_version_input.FtpHost;
            if (!Ftp.InternetUtil.IsConnectionAvailable(str_uri, out o_error))
            {
                o_error = i_version_input.ErrMsgNoInternetConnection;
                return false;
            }
            QQQ*/

            bool b_down_load = true;

            Ftp.DownLoad ftp_download = new Ftp.DownLoad(i_version_input.FtpHost, i_version_input.FtpUser, i_version_input.FtpPassword);

            string server_address_directory = i_version_input.ServerDirectory;
            string local_address_directory = FileUtil.SubDirectory(i_version_input.LocalDirectory, i_version_input.ExeDirectory);

            if (!ftp_download.GetFiles(server_address_directory, local_address_directory, out o_error))
            {
                b_down_load = false;
            }

            if (!b_down_load)
            {
                o_error = i_version_input.ErrMsgFailureDownloadingVersionInfoFile;
                return b_down_load;
            }

            ArrayList array_list_extensions = new ArrayList();
            array_list_extensions.Add("txt");

            string[] list_extensions = (string[])array_list_extensions.ToArray(typeof(string));

            string[] ret_file_names = null;
            if (!FileUtil.GetFilesDirectory(list_extensions, local_address_directory, out ret_file_names))
            {
                o_error = "DownloadVersionInfoFile FileUtil.GetFilesDirectory failed. Programming error";
                b_down_load = false;
                return b_down_load;
            }

            if (null == ret_file_names)
            {
                o_error = "DownloadVersionInfoFile Programming error: Returned string array is null ";
                b_down_load = false;
                return b_down_load;
            }


            if (ret_file_names.Length != 1)
            {
                o_error = "DownloadVersionInfoFile Programming error: Number of files is " + ret_file_names.Length.ToString();
                b_down_load = false;
                return b_down_load;
            }

            string version_info_file_name = ret_file_names[0];

            string file_name_no_extension = Path.GetFileNameWithoutExtension(version_info_file_name);

            File.Delete(version_info_file_name);

            int index_sep = file_name_no_extension.IndexOf('_');
            if (0 == index_sep)
            {
                o_error = "DownloadVersionInfoFile Programming error: No _ (underscore) in the file name ";
                b_down_load = false;
                return b_down_load;
            }

            string major_str = file_name_no_extension.Substring(0, index_sep);
            string minor_str = file_name_no_extension.Substring(index_sep + 1);


            if (!Int32.TryParse(major_str, out o_major))
            {
                o_error = "DownloadVersionInfoFile Programming error: Converting major string to int failed ";
                b_down_load = false;
                return b_down_load;
            }
            if (!Int32.TryParse(minor_str, out o_minor))
            {
                o_error = "DownloadVersionInfoFile Programming error: Converting minor string to int failed ";
                b_down_load = false;
                return b_down_load;
            }

            return b_down_load;
        } // DownloadVersionInfoFile

    } // DownLoad
    } // namespace
