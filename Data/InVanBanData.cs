using Google.Cloud.Firestore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System;
using QUANLIVANBAN.MyClass;
using Microsoft.AspNetCore.Hosting;
namespace QUANLIVANBAN.Data
{ 
    public class InVanBanData: InVanBan
    {
        public InVanBan invb = new InVanBan();
        private readonly IWebHostEnvironment _env;
        public InVanBanData(IWebHostEnvironment env)
        {
            _env = env;
        }
        //private List<InVanBan> todos = new List<InVanBan>();
        public virtual Task Printvanban(InVanBan vanban)
        { 
            invb = vanban;
            return Task.FromResult(vanban);
        }
        public virtual Task<InVanBan> shownoidung()
        {
            try
            {
                return Task.FromResult(invb);
            }
            catch
            {
                throw;
            }
        }
         public async Task AddXuatvanban(InVanBan vanban)
        {
            try
            {
                DateTime id = DateTime.Now;
                string nn = id.ToString();
                string result = nn.Replace(" ", String.Empty).Replace("/", String.Empty).Replace(":", String.Empty);
                vanban.TLVB_Id=result;
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference colRef = db.Collection("TaoLapVanBan").Document(vanban.TLVB_Id);
                vanban.TLVB_Id=colRef.Id.ToString();
                await colRef.SetAsync(vanban);
            }
            catch
            {
                throw;
            }
        }
    }
}