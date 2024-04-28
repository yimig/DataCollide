using DataCollide.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DataCollide
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            RegistEpplus();
            contentFrame.Navigate(RouterDict.First().Value);
            StaticField.MainHandler = WinRT.Interop.WindowNative.GetWindowHandle(this);
        }

        private void RegistEpplus()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        private Dictionary<string, Type> RouterDict = new Dictionary<string, Type>() 
        {
            {"Page1", typeof(PageOpenFile)},
            {"Page2", typeof(PageMatch)},
            {"Page3", typeof(PageResult)},
            {"Page4", typeof(PageAbout)}
        };

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = (NavigationViewItem)args.SelectedItem;
            contentFrame.Navigate(RouterDict[selectedItem.Tag.ToString()]);
        }

    }
}
