using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;


namespace loginDb.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public enum EditMode { Add, Edit }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int ReadUserIdFromFile()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string logiDBFolderPath = Path.Combine(documentsPath, "Peace Of Mind");
            string filePath = Path.Combine(logiDBFolderPath, "Temp.txt");

            // Check if file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The file Temp.txt does not exist.");
            }

            // Read the ID from the file
            using (StreamReader reader = new StreamReader(filePath))
            {
                string content = reader.ReadToEnd();
                return int.Parse(content);
            }
        }
    }
}
