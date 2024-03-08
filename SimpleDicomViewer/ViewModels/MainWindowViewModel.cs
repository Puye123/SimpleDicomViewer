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
        IDialogMessageService DialogMessage { get; }

        /// <summary>
        /// ファイルピッカー表示クラス
        /// </summary>
        IFilePickerService FilePickerService { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dialogMessage">ダイアログボックス表示クラス</param>
        public MainWindowViewModel(IDialogMessageService dialogMessage, IFilePickerService filePickerService)
        {
            DialogMessage = dialogMessage;
            FilePickerService = filePickerService;
        }

        #region Commands
        [RelayCommand]
        private async Task AddFile()
        {
            var filePath = await FilePickerService.FilePickAsync();
            await DialogMessage.ShowDialogMessageAsync("[未実装] ファイルの追加", filePath);
        }

        [RelayCommand]
        private async Task AddFolder()
        {
            var folderPath = await FilePickerService.FolderPickAsync();
            await DialogMessage.ShowDialogMessageAsync("[未実装] フォルダの追加", folderPath);
        }

        [RelayCommand]
        private async Task Save()
        {
            await DialogMessage.ShowDialogMessageAsync("[未実装] DICOMデータの保存", "test test");
        }

        [RelayCommand]
        private async Task Exit()
        {
            await DialogMessage.ShowDialogMessageAsync("[未実装] アプリの終了", "test test");
        }

        [RelayCommand]
        private async Task Help()
        {
            await DialogMessage.ShowDialogMessageAsync("[未実装] このアプリについて", "test test");
        }
        #endregion
    }
}
