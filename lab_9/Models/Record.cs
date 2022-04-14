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
        public Record(string path, bool pass)
        {
            Files = new ObservableCollection<Record>();
            _Path = path;
            if (!pass)
                Name = Path.GetFileName(path);
            else
                Name = "Disk " + path.Substring(0, path.IndexOf(":"));
        }

        public void get_files()
        {
                try
                {
                    IEnumerable<string> dirs = Directory.EnumerateDirectories(_Path, "*", SearchOption.TopDirectoryOnly);
                    foreach (string dir in dirs)
                    {
                        Record thisnode = new Record(dir, false);
                        Files.Add(thisnode);
                    }

                    
                    IEnumerable<string> files = Directory.EnumerateFiles(_Path).Where(file => exp.Any(file.ToLower().EndsWith)).ToList();

                    foreach (string file in files)
                    {
                        Files.Add(new Record(file, false));
                    }
                }
                catch
                {

                }
            }
        }
    }
