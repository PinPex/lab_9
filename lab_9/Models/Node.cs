using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab_9.Models
{
    public class Node : INotifyPropertyChanged
    {
        bool isSelected;
        private bool isHashed;
        public ObservableCollection<Node>? FilesAndFolders { get; set; }
        public string NodeName { get; }
        public string FullPath { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Node(string _FullPath, bool isDisk)
        {
            FilesAndFolders = new ObservableCollection<Node>();
            FullPath = _FullPath;
            if (!isDisk)
                NodeName = Path.GetFileName(_FullPath);
            else
                NodeName = "Disk " + _FullPath.Substring(0, _FullPath.IndexOf(":"));
            isHashed = false;
        }

        public void GetFilesAndFolders()
        {
                try
                {
                    IEnumerable<string> subdirs = Directory.EnumerateDirectories(FullPath, "*", SearchOption.TopDirectoryOnly);
                    foreach (string dir in subdirs)
                    {
                        Node thisnode = new Node(dir, false);
                        FilesAndFolders.Add(thisnode);
                    }

                    string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    IEnumerable<string> files = Directory.EnumerateFiles(FullPath)
                        .Where(file => allowedExtensions.Any(file.ToLower().EndsWith))
                        .ToList();

                    foreach (string file in files)
                    {
                        FilesAndFolders.Add(new Node(file, false));
                    }
                }
                catch
                {

                }
            }
        }
    }
