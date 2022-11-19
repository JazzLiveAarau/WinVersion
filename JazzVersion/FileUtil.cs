using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JazzVersion
{
    /// <summary>File utility functions</summary>
    static public class FileUtil
    {
        /// <summary>Get the path to a subdirectory. Create the directory if not existing.</summary>
        public static string SubDirectory(string i_subdir_name, string i_exe_directory)
        {
            string sub_directory = Path.Combine(i_exe_directory, i_subdir_name);

            if (!Directory.Exists(sub_directory))
            {
                Directory.CreateDirectory(sub_directory);
            }

            return sub_directory;
        } // SubDirectory

        /// <summary>
        /// Get files with given extensions
        /// </summary>
        /// <param name="i_extension">Array of extensions (with point)</param>
        /// <param name="i_directory">Search directory</param>
        /// <param name="o_file_names">Array of found files with paths</param>
        /// <returns>false if directory not exists or the input array of extensions is empty</returns>
        static public bool GetFilesDirectory(string[] i_extensions, string i_directory, out string[] o_file_names)
        {
            ArrayList files_string_array = new ArrayList();
            o_file_names = (string[])files_string_array.ToArray(typeof(string));

            for (int i_ext = 0; i_ext < i_extensions.Length; i_ext++)
            {
                string current_ext = i_extensions[i_ext];

                string[] files_ext = Directory.GetFiles(i_directory, "*" + current_ext);

                foreach (string file_ext in files_ext)
                {
                    files_string_array.Add(file_ext);
                }
            }

            files_string_array.Reverse();

            o_file_names = (string[])files_string_array.ToArray(typeof(string));

            return true;
        } // GetFilesDirectory


    } // FileUtil

} // namespace
