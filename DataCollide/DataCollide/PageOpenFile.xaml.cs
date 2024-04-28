using DataCollide.Data;
using DataCollide.Data.Reader;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using WinRT.Interop;
using System.Text;
using Windows.UI.Xaml.Interop;
using System.ComponentModel;
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DataCollide
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageOpenFile : Page, INotifyPropertyChanged
    {
        private TableFile tableFileA, tableFileB;
        public event PropertyChangedEventHandler PropertyChanged;
        private List<string> encodingListA, headerListA, glanceListA, encodingListB, headerListB, glanceListB;

        public TableFile TableFileA
        {
            get => tableFileA;
            set
            {
                tableFileA = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TableFileA"));
            }
        }

        public TableFile TableFileB
        {
            get => tableFileB;
            set
            {
                tableFileB = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TableFileB"));
            }
        }

        public List<string> EncodingListA 
        { 
            get => encodingListA; 
            set {
                encodingListA = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EncodingListA"));
            }
        }

        public List<string> HeaderListA 
        { 
            get => headerListA;
            set
            {
                headerListA = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("HeaderListA"));
            }
        }

        public List<string> GlanceListA 
        { 
            get => glanceListA;
            set
            {
                glanceListA = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("GlanceListA"));
            }
        }

        public List<string> EncodingListB 
        { 
            get => encodingListB;
            set
            {
                encodingListB = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EncodingListB"));
            }
        }

        public List<string> HeaderListB 
        { 
            get => headerListB;
            set
            {
                headerListB = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("HeaderListB"));
            }
        }

        public List<string> GlanceListB 
        { 
            get => glanceListB;
            set
            {
                glanceListB = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("GlanceListB"));
            }
        }

        public PageOpenFile()
        {
            InitTable();
            this.DataContext = this;
            this.InitializeComponent();
        }

        

        private async void BtnOpenFileA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TableFileA = await GetTableFile();
            } catch (Exception ex)
            {
                GdLoading.Visibility = Visibility.Collapsed;
                var dialog = new ContentDialog()
                {
                    XamlRoot = this.XamlRoot,
                    Title = "打开文件错误",
                    Content = ex.Message.Trim() == "" ? "Excel文件可能被占用": ex.Message,
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
            
        }



        private void InitTable()
        {
            TableFileA = GetEmptyTableFile();
            TableFileB = GetEmptyTableFile();
            EncodingListA = new List<string>();
            HeaderListA = new List<string>();
            GlanceListA = new List<string>();
            EncodingListB = new List<string>();
            HeaderListB = new List<string>();
            GlanceListB = new List<string>();
        }

        private TableFile GetEmptyTableFile()
        {
            return new TableFile()
            {
                FilePath = "",
                Name = "",
                Format = TableFormat.CSV,
                SheetList = new List<string>(),
                Encoding = Encoding.UTF8,
                TableList = new List<Table>()
            };
        }

        private async void BtnOpenFileB_Click(object sender, RoutedEventArgs e)
        {
            TableFileB = await GetTableFile();
        }

        private async Task<TableFile> GetTableFile()
        {
            var filePicker = new FileOpenPicker();
            foreach (var format in Enum.GetValues(typeof(TableFormat)))
            {
                filePicker.FileTypeFilter.Add("." + format.ToString().ToLower());
            }
            InitializeWithWindow.Initialize(filePicker, StaticField.MainHandler);
            var file = await filePicker.PickSingleFileAsync();
            GdLoading.Visibility = Visibility.Visible;
            var reader = ReaderFactory.ReadFile(file);
            var tableFile = await reader.GetTableFileAsync();
            GdLoading.Visibility = Visibility.Collapsed;
            return tableFile;
        }
    }
}
