using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleDicomViewer.CommonServices;
using SimpleDicomViewer.Views;

namespace SimpleDicomViewer.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        /// <summary>
        /// ダイアログボックス表示クラス
        /// </summary>
        IDialogMessage DialogMessage { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel() {
            DialogMessage = new DialogMessage();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dialogMessage">ダイアログボックス表示クラス</param>
        public MainWindowViewModel(IDialogMessage dialogMessage)
        {
            DialogMessage = dialogMessage;
        }

        #region Commands
        [RelayCommand]
        private async Task AddFile()
        {
            await DialogMessage.ShowDialogMessageAsync(MainWindow.Handle.Content.XamlRoot, "[未実装] ファイルの追加", "test test");
        }

        [RelayCommand]
        private async Task AddFolder()
        {
            await DialogMessage.ShowDialogMessageAsync(MainWindow.Handle.Content.XamlRoot, "[未実装] フォルダの追加", "test test");
        }

        [RelayCommand]
        private async Task Save()
        {
            await DialogMessage.ShowDialogMessageAsync(MainWindow.Handle.Content.XamlRoot, "[未実装] DICOMデータの保存", "test test");
        }

        [RelayCommand]
        private async Task Exit()
        {
            await DialogMessage.ShowDialogMessageAsync(MainWindow.Handle.Content.XamlRoot, "[未実装] アプリの終了", "test test");
        }

        [RelayCommand]
        private async Task Help()
        {
            await DialogMessage.ShowDialogMessageAsync(MainWindow.Handle.Content.XamlRoot, "[未実装] このアプリについて", "test test");
        }
        #endregion
    }
}
