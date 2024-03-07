using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDicomViewer.CommonServices
{
    internal class DialogMessage : IDialogMessage
    {
        public async Task ShowDialogMessageAsync(XamlRoot root, string title, string content)
        {
            ContentDialog myDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK"
            };

            myDialog.XamlRoot = root;
            ContentDialogResult result = await myDialog.ShowAsync();
        }
    }
}
