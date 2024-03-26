using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using SimpleDicomViewer.CommonServices;
using SimpleDicomViewer.Domain.Services.ImageConverter;
using SimpleDicomViewer.Domain.ValueObjects;
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

        //[ObservableProperty]
        //public BitmapImage bitmapImage;
        [ObservableProperty]
        public string showFilePath = "/Assets/lena.jpg";

        private DicomListElement _selectedDicomListElement;
        public DicomListElement SelectedDicomListElement
        {
            get => _selectedDicomListElement;
            set
            {
                if(!SetProperty(ref _selectedDicomListElement, value))
                {
                    return;
                }
                UpdateTagList();

                try
                {
                    var imageArray = SelectedDicomListElement.DicomDataEntity.Values.Where(x => x.Tag == new Domain.ValueObjects.Tag(0x7fe0, 0x0010)).ToArray();
                    if (imageArray.Length > 0)
                    {
                        // Todo: Facadeクラスを作る
                        int height = Convert.ToInt32(SelectedDicomListElement.DicomDataEntity.Values.Where(x => x.Tag == new Domain.ValueObjects.Tag(0x0028, 0x0010)).ToArray()[0].GetValueObject());
                        int width = Convert.ToInt32(SelectedDicomListElement.DicomDataEntity.Values.Where(x => x.Tag == new Domain.ValueObjects.Tag(0x0028, 0x0011)).ToArray()[0].GetValueObject());
                        int bit = Convert.ToInt32(SelectedDicomListElement.DicomDataEntity.Values.Where(x => x.Tag == new Domain.ValueObjects.Tag(0x0028, 0x0101)).ToArray()[0].GetValueObject());

                        string photometricInterpretationStr = (string)SelectedDicomListElement.DicomDataEntity.Values.Where(x => x.Tag == new Domain.ValueObjects.Tag(0x0028, 0x0004)).ToArray()[0].GetValueObject();
                        PhotometricInterpretation photometric = new PhotometricInterpretation(photometricInterpretationStr.Trim(' ', '\0'));

                        string transferSyntaxStr = (string)SelectedDicomListElement.DicomDataEntity.Values.Where(x => x.Tag == new Domain.ValueObjects.Tag(0x0002, 0x0010)).ToArray()[0].GetValueObject();
                        TransferSyntax transferSyntax = new TransferSyntax(transferSyntaxStr.Trim(' ', '\0'));
                        
                        IImageConverterFactory factory = new ImageConverterFactory();
                        IImageConverter converter = factory.CreateImageConverter(transferSyntax.TransferSyntaxName);

                        // BitmapImage = converter.Convert(imageArray[0].Value, height, width, bit, photometric).Result;
                        string stCurrentDir = System.IO.Directory.GetCurrentDirectory();
                        string tempFilePath = Path.GetTempFileName();
                        tempFilePath = Path.ChangeExtension(tempFilePath, ".png");
                        ShowFilePath = converter.Save(tempFilePath, imageArray[0].Value, height, width, bit, photometric);
                    }
                }
                catch(Exception ex)
                {
                    DialogMessage.ShowDialogMessageAsync("Error", ex.Message);
                }
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
