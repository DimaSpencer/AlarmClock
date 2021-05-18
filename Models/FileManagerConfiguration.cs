using System.Windows.Forms;

namespace WindowsFormsApp1.Model
{
    public class FileManagerConfiguration
    {
        public string FilePath { get; set; }

        public FileManagerConfiguration(string filePath)
        {
            FilePath = filePath;
        }
    }
}