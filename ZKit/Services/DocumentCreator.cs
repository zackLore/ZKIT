using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using ZKit.Models;

namespace ZKit.Services
{
    public static class DocumentCreator
    {
        public static string AddFile(string sourcePath, string destinationPath, string fileName)
        {
            string status = "";
            try
            {
                if (!Directory.Exists(sourcePath))
                {
                    throw new Exception("Could not find Directory: " + sourcePath);
                }

                if (!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                }

                if (!File.Exists(destinationPath + "/" + fileName))
                {
                    File.Copy(sourcePath + "/" + fileName, destinationPath + "/" + fileName);
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }

        public static string CreateDocument(string path, string docName, string docType, string docContent, bool overwrite)
        {
            string status = "";
            string fullPath = path + "\\" + docName + "." + docType;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (File.Exists(fullPath))
                {
                    if (overwrite)
                    {
                        File.Delete(fullPath);
                    }
                }

                using (StreamWriter writer = File.CreateText(fullPath))
                {
                    writer.Write(docContent);
                }
            }
            catch(Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }

        //https://web.archive.org/web/20130921190426/http://tech.pro/tutorial/798/csharp-tutorial-xml-serialization
        public static string SaveProject(Project project)
        {
            string status = project.Name + " Saved!";

            if (!Directory.Exists("Projects"))
            {
                Directory.CreateDirectory("Projects");
            }

            if (!File.Exists(project.Name))
            {
                File.Delete(project.Name);
            }

            try
            {
                //XmlSerializer serializer = new XmlSerializer(typeof(Project));
                //TextWriter textWriter = new StreamWriter(@"Projects\" + project.Name + ".xml");
                //serializer.Serialize(textWriter, project);
                //textWriter.Close();
                XMLGenerator xml = new XMLGenerator(project);
                CreateDocument("Projects", xml.DocumentName, "xml", xml.GenerateDoc(), true);
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }

            return status;
        }
    }
}
