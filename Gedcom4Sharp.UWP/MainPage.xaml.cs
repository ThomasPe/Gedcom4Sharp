using Gedcom4Sharp.Parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Gedcom4Sharp.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CancellationTokenSource _cancelToken;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void LoadGedcomBtn_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(".ged");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var gp = new GedcomParser();
                var progressHandler = new Progress<int>(value =>
                {
                    ProgressTB.Text = $"Lines read: {value}";
                });
                _cancelToken = new CancellationTokenSource();
                try
                {
                    await gp.Load(await file.OpenStreamForReadAsync(), null, progressHandler, _cancelToken.Token);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                
            }
        }

        private void CancelLoadBtn_Click(object sender, RoutedEventArgs e)
        {
            _cancelToken.Cancel();
        }
    }
}
