using BlazorInputFile;
using System.Threading.Tasks;
namespace QUANLIVANBAN.Data
{ 
    public interface IFileUpload
    {
        Task UploadAsync(IFileListEntry file);
    }
}