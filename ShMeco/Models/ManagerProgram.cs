using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Exhibition.Models
{
    public static class ManagerProgram
    {
        public static List<ProgramInfoDataSource> Programs;

        /// <summary>
        /// Считывание всех программ из xml файла в список программ programs
        /// </summary>
        public static void ReadPrograms()
        {
            string file = "progs.xml";

            XmlSerializer xml = new XmlSerializer(typeof(List<ProgramInfoDataSource>));
            try
            {
                using (Stream fStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    Programs = (List<ProgramInfoDataSource>)xml.Deserialize(fStream);
                    fStream.Close();
                }
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("Поврежденный файл Программ. " + e);
            }

            catch (FileNotFoundException e)
            {
                MessageBox.Show(file + " не существует. " + e);
            }
        }
    }
}
