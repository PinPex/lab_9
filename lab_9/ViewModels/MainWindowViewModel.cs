using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using lab_9.Models;
using Avalonia.Media.Imaging;

namespace lab_9.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public class Picture
        {
            public Bitmap picture { get; set; }
            public string _path { get; set; }
            public Picture(string path)
            {
                    picture = new Bitmap(path);
                    _path = path;
            }
        }
        public ObservableCollection<Picture> Direct { get; set; }
        public ObservableCollection<Record> Items { get; }

        List<string> Drives;

        public MainWindowViewModel()
        {
            Drives = new List<string>();
            Items = new ObservableCollection<Record>();
            Direct = new ObservableCollection<Picture>();
            DriveInfo[] drives = DriveInfo.GetDrives();

            for(int i = 0; i < drives.Length; ++i)
            {
                Drives.Add(drives[i].Name);
                Record root_record = new Record(drives[i].Name, true);
                Items.Add(root_record);
            }
        }

        public void RefreshImageList(List<string> paths_of_img, string current_image)
        {
            Direct.Clear();
            Direct.Add(new Picture(current_image));
            for(int i = 0; i < paths_of_img.Count; ++i)
            {
                Direct.Add(new Picture(paths_of_img[i]));
            }
        }
    }
}
