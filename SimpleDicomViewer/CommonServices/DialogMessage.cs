using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace SimpleDicomViewer.CommonServices
{
    internal class DialogMessage : IDialogMessageService
    {
        private readonly Page _Handle;

        public DialogMessage(Page handle) {
            _Handle = handle;
        }
        public async Task ShowDialogMessageAsync(string title, string content)
        {
            ContentDialog myDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK"
            };

            myDialog.XamlRoot = _Handle.Content.XamlRoot;
            ContentDialogResult result = await myDialog.ShowAsync();
        }
    }
}
