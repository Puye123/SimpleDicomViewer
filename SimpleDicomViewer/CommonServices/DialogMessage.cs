using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace SimpleDicomViewer.CommonServices
{
    internal class DialogMessage : IDialogMessageService
    {
        private readonly Window _windowHandle;

        public DialogMessage(Window windowHandle) {
            _windowHandle = windowHandle;
        }
        public async Task ShowDialogMessageAsync(string title, string content)
        {
            ContentDialog myDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK"
            };

            myDialog.XamlRoot = _windowHandle.Content.XamlRoot;
            ContentDialogResult result = await myDialog.ShowAsync();
        }
    }
}
