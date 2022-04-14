using Avalonia.Controls;
using lab_9.Models;
using lab_9.ViewModels;
using System.Linq;
using System.IO;
using Avalonia.Input;
using Avalonia.Controls.Primitives;

namespace lab_9.Views
{
    public partial class MainWindow : Window
    {
        private Carousel slide;
        private Button back_button;
        private Button next_button;

        private void Init()
        {
            slide = this.FindControl<Carousel>("Slider");
            back_button = this.FindControl<Button>("Back");
            next_button = this.FindControl<Button>("Next");
        }
        public MainWindow()
        {
            InitializeComponent();
            Init();
            back_button.Click += (s, e) => slide.Previous();
            next_button.Click += (s, e) => slide.Next();
        }

        private void Change(object send, PointerReleasedEventArgs point)
        {
            TreeViewItem Item = send as TreeViewItem;
            Record selectedNode = Item.DataContext as Record;

            if (Record.exp.Any(selectedNode.Name.ToLower().EndsWith))
            {
                string path = selectedNode._Path.Substring(0, selectedNode._Path.IndexOf(selectedNode.Name));
                var files = Directory.EnumerateFiles(path).Where(file => Record.exp.Any(file.ToLower().EndsWith)).ToList();

                files.Remove(selectedNode._Path);
                var context = this.DataContext as MainWindowViewModel;

                context.RefreshImageList(files, selectedNode._Path);
            }
        }

        private void Click_load(object send, TemplateAppliedEventArgs e)
        {
            ContentControl treeViewItem = send as ContentControl;
            Record current_record = treeViewItem.DataContext as Record;
            current_record.get_files();
        }
    }
}
