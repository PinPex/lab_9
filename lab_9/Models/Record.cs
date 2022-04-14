using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.IO;

namespace lab_9.Models
{
    public class Record
    {
        public ObservableCollection<Record>? Files { get; set; }
        public string Name { get; }
        public string _Path { get; }

        public static string[] exp = new[] { ".jpg", ".jpeg", ".png" };
        public Record(string path, bool disk)
        {
            Files = new ObservableCollection<Record>();
            _Path = path;
            Name = "Disk " + path.Substring(0, path.IndexOf(":"));
        }
        public Record(string path)
        {
            Files = new ObservableCollection<Record>();
            _Path = path;
            Name = Path.GetFileName(path);
        }

        public void get_files()
        {
            try
            {
                IEnumerable<string> dirs = Directory.EnumerateDirectories(_Path, "*", SearchOption.TopDirectoryOnly);
                foreach (string dir in dirs)
                {
                    Record current_record = new Record(dir);
                    Files.Add(current_record);
                }
                IEnumerable<string> files = Directory.EnumerateFiles(_Path).Where(file => exp.Any(file.ToLower().EndsWith)).ToList();
                foreach (string file in files)
                {
                    Files.Add(new Record(file));
                }
            }
            catch
            {

            }
        }
    }
}
