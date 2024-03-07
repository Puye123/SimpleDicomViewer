using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDicomViewer.CommonServices
{
    public interface IDialogMessage
    {
        public Task ShowDialogMessageAsync(XamlRoot root,  string title, string content);
    }
}
