using BlazorInputFile;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Firebase.Storage.Client;
namespace QUANLIVANBAN.Data
{ 
    public class FileUpload : IFileUpload
    {
        public async Task UploadAsync(IFileListEntry fileEntry)
        {
            var task = new FirebaseStorage("quanlivanban-26d21.appspot.com").Child("Vanbanden").Child(fileEntry.Name);
            await task.PutAsync(fileEntry.Data);
        }
    }
}