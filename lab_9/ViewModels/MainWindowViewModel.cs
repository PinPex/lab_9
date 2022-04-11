using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using System.Windows.Input;
using ReactiveUI;
using System.IO;
using System.Diagnostics;
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
        public ObservableCollection<Image> DirectoryImages { get; set; }
        public ObservableCollection<Node> Items { get; }

        List<string> allDrivesNames;

        public MainWindowViewModel()
        {
            allDrivesNames = new List<string>();
            Items = new ObservableCollection<Node>();
            DirectoryImages = new ObservableCollection<Image>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            for(int i = 0; i < allDrives.Length; i++)
            {
                allDrivesNames.Add(allDrives[i].Name);
                Node rootNode = new Node(allDrives[i].Name, true);
                rootNode.GetFilesAndFolders();
                Items.Add(rootNode);
            }
        }

        public void RefreshImageList(List<string> imagesPaths, string selectedImage)
        {
            DirectoryImages.Clear();
            DirectoryImages.Add(new Image(selectedImage));
            for(int i = 0; i < imagesPaths.Count; i++)
            {
                DirectoryImages.Add(new Image(imagesPaths[i]));
            }
        }
    }
}
