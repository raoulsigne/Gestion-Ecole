using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Management;

namespace Ecole.Utilitaire
{
    class GestionRepertoire
    {
        public static void CreateDirectory(string path)
        {
            // Specify the directory you want to manipulate.
            //string path = @"E:\MyDir";

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    //Console.WriteLine("That path exists already.");
                    //MessageBox.Show("That path exists already.");
                    //return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                //Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));
                //MessageBox.Show("The directory "+ path +" was created successfully at " + Directory.GetCreationTime(path));

            }
            catch (Exception e)
            {
                //Console.WriteLine("The process failed: {0}", e.ToString());
                //MessageBox.Show("The process failed: "+ e.ToString());

            }
            finally { }
        }

        public static bool existDirectory(string path)
        {
            // Specify the directory you want to manipulate.
            //string path = @"E:\MyDir";

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;

            }
            finally { }
        }


        public static void DeleteDirectory(string path)
        {
            // Specify the directory you want to manipulate.

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    //Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

                    //// Delete the directory.
                    di.Delete(true);
                    ////Console.WriteLine("The directory was deleted successfully.");
                    //MessageBox.Show("The directory " + path + " was deleted successfully.");
                    return;
                }
                else {
                    //MessageBox.Show("The directory " + path + " doesn't exist");
                    return;
                }
                

            }
            catch (Exception e)
            {
                //Console.WriteLine("The process failed: {0}", e.ToString());
                //MessageBox.Show("The process failed: " + e.ToString());

            }
            finally { }
        }

        //fonction qui permet de copier le contenu d'un repertoire ainsi 
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public static void QshareFolder(string FolderPath, string ShareName, string Description)
        {
            try
            {
                /*ManagementClass managementClass = new ManagementClass("Win32_Share");
                ManagementBaseObject inParams = managementClass.GetMethodParameters("Create");
                ManagementBaseObject outParams;

                inParams["Description"] = Description;
                inParams["Name"] = ShareName;
                inParams["Path"] = FolderPath;
                inParams["MaximumAllowed"] = null;
                inParams["Password"] = null;
                inParams["Access"] = null;
                inParams["Type"] = 0x0; // Disk Drive

                // Invoke the method on the ManagementClass object
                outParams = managementClass.InvokeMethod("Create", inParams, null);

                // Check to see if the method invocation was successful
                if ((uint)(outParams.Properties["ReturnValue"].Value) != 0)
                {
                    throw new Exception("Unable to share directory.");
                }*/



                ManagementClass mc = new ManagementClass("win32_share");
                ManagementBaseObject inParams = mc.GetMethodParameters("Create");
                inParams["Description"] = "My Shared Folder";
                inParams["Name"] = "Shared Folder Name";
                inParams["Path"] = FolderPath;
                inParams["Type"] = 0x0;
                inParams["MaximumAllowed"] = null;
                inParams["Password"] = null;
                inParams["Access"] = null; // Make Everyone has full control access.
                ManagementBaseObject outParams = mc.InvokeMethod("Create", inParams, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error!");
            }
        }

        //public static void DeleteDirectory(string path)
        //{
        //    DirectoryInfo directory = new DirectoryInfo(path);

        //    // Recherche tous les éléments (fichiers et sous-dossiers) situé dans le dossier path
        //    foreach (DirectoryInfo info in directory.GetFileSystemInfos("*", SearchOption.AllDirectories))
        //    {
        //        if ((info.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
        //        {
        //            // Change l'attribut de l'objet
        //            info.Attributes = FileAttributes.Normal;
        //        }
        //    }

        //    // Supprime le dossier et son contenu
        //    directory.Delete(true);
        //}


    }
}
