using System.Threading.Tasks;

namespace SimpleDicomViewer.CommonServices
{
    public interface IDialogMessageService
    {
        public Task ShowDialogMessageAsync(string title, string content);
    }
}
