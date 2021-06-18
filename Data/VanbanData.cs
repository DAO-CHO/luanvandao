using Google.Cloud.Firestore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System;
using QUANLIVANBAN.MyClass;
using BlazorInputFile;
using Firebase.Storage.Client;
using Microsoft.AspNetCore.Hosting;
namespace QUANLIVANBAN.Data
{
    public class VanbanData
    {
        private readonly IWebHostEnvironment _env;
        public VanbanData(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<List<Vanbans>> GetAllVanban()
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                var list = new List<Vanbans>();
                Query employeeQuery = db.Collection("Vanban");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Vanbans st = documentSnapshot.ConvertTo<Vanbans>();
                        st.VanBan_id = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Vanbans> sortedEmployeeList = list.OrderBy(x => x.VanBan_id).ToList();
                return sortedEmployeeList;
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> UploadAsync(IFileListEntry fileEntry)
        {
            var task = new FirebaseStorage("quanlivanban-26d21.appspot.com").Child("Vanban").Child(fileEntry.Name);
            var a = await task.PutAsync(fileEntry.Data);
            return a;
        }
        public async Task AddVanban(Vanbans VanbanForecast)
        {
            try
            {
                TimeSpan t = DateTime.Now - new DateTime(1970, 1, 1);
                int secondsSinceEpoch = (int)t.TotalSeconds;
                string idstring = secondsSinceEpoch.ToString();
                VanbanForecast.VanBan_id = idstring;
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference colRef = db.Collection("Vanban").Document(VanbanForecast.VanBan_id);
                VanbanForecast.VanBan_id = colRef.Id.ToString();
                await colRef.SetAsync(VanbanForecast);
            }
            catch
            {
                throw;
            }
        }
        public async Task UpdateVanban(Vanbans VanbanForecast)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Vanban").Document(VanbanForecast.VanBan_id);
                await empRef.SetAsync(VanbanForecast, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Vanbans> GetVanbanData(string id)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("Vanban").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                if (snapshot.Exists)
                {
                    Vanbans emp = snapshot.ConvertTo<Vanbans>();
                    emp.VanBan_id = snapshot.Id;
                    return emp;
                }
                else
                {
                    return new Vanbans();
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task DeleteVanban(string id)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Vanban").Document(id);
                await empRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}