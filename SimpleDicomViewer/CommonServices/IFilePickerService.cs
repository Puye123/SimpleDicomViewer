using System.Threading.Tasks;

namespace SimpleDicomViewer.CommonServices
{
    public interface IFilePickerService
    {
        public Task<string?> FilePickAsync();

        public Task<string?> FolderPickAsync();
    }
}
