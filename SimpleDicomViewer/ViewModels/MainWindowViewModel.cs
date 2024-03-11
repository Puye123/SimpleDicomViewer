using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<DicomListElement> DicomListElements { get; private set; }

        public ObservableCollection<TagListElement> TagListElements { get; private set; }

        private DicomListElement _selectedDicomListElement;
        public DicomListElement SelectedDicomListElement
        {
            get => _selectedDicomListElement;
            set
            {
                SetProperty(ref _selectedDicomListElement, value);
                UpdateTagList();
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dialogMessage">ダイアログボックス表示クラス</param>
        public MainWindowViewModel(IDialogMessageService dialogMessage, IFilePickerService filePickerService)
        {
            DialogMessage = dialogMessage;
            FilePickerService = filePickerService;

            DicomListElements = new ObservableCollection<DicomListElement>();
            TagListElements = new ObservableCollection<TagListElement>();
        }

        #region Commands
        [RelayCommand]
        private async Task AddFile()
        {
            var filePath = await FilePickerService.FilePickAsync();
            try
            {
                var fileIO = new Infrastructure.File.DicomDataFileIO();
                var dicomData = fileIO.Read(filePath);

                DicomListElements.Add(new DicomListElement(dicomData));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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

        [RelayCommand]
        private void UpdateTagList()
        {
            TagListElements.Clear();
            foreach (var ve in SelectedDicomListElement.DicomDataEntity.Values)
            {
                TagListElements.Add(new TagListElement(ve));
            }
        }
        #endregion
    }
}
