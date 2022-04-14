using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using lab_9.Models;
using Avalonia.Media.Imaging;

namespace lab_9.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public class Image
        {
            public Bitmap Img { get; set; }
            public string Path { get; set; }
            public Image(string path)
            {
                    Img = new Bitmap(path);
                    Path = path;
            }
        }
        public ObservableCollection<Image> Direct { get; set; }
        public ObservableCollection<Record> Items { get; }

        List<string> Drives;

        public MainWindowViewModel()
        {
            Drives = new List<string>();
            Items = new ObservableCollection<Record>();
            Direct = new ObservableCollection<Image>();
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
            Direct.Add(new Image(current_image));
            for(int i = 0; i < paths_of_img.Count; ++i)
            {
                Direct.Add(new Image(paths_of_img[i]));
            }
        }
    }
}
