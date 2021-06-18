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
    public class VanbancanhanData
    {
        private readonly IWebHostEnvironment _env;
        public VanbancanhanData(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<List<Vanbancanhans>> GetAllVanbancanhan()
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                var list = new List<Vanbancanhans>();
                Query employeeQuery = db.Collection("Vanbancanhan");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Vanbancanhans st = documentSnapshot.ConvertTo<Vanbancanhans>();
                        st.VbcnId = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Vanbancanhans> sortedEmployeeList = list.OrderBy(x => x.VbcnId).ToList();
                return sortedEmployeeList;
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> UploadAsync(IFileListEntry fileEntry)
        {
            var task = new FirebaseStorage("quanlivanban-26d21.appspot.com").Child("Vanbancanhan").Child(fileEntry.Name);
            var a = await task.PutAsync(fileEntry.Data);
            return a;
        }
        public async Task AddVanbancanhan(Vanbancanhans VanbancanhanForecast)
        {
            try
            {
                TimeSpan t = DateTime.Now - new DateTime(1970, 1, 1);
                int secondsSinceEpoch = (int)t.TotalSeconds;
                string idstring = secondsSinceEpoch.ToString();
                VanbancanhanForecast.VbcnId=idstring;
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference colRef = db.Collection("Vanbancanhan").Document(VanbancanhanForecast.VbcnId);
                VanbancanhanForecast.VbcnId=colRef.Id.ToString();
                await colRef.SetAsync(VanbancanhanForecast);
            }
            catch
            {
                throw;
            }
        }
        public async Task UpdateVanbancanhan(Vanbancanhans VanbancanhanForecast)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Vanbancanhan").Document(VanbancanhanForecast.VbcnId);
                await empRef.SetAsync(VanbancanhanForecast, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Vanbancanhans> GetVanbancanhanData(string id)  
        {  
            try  
            {  
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                DocumentReference docRef = db.Collection("Vanbancanhan").Document(id);  
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();  
  
                if (snapshot.Exists)  
                {  
                    Vanbancanhans emp = snapshot.ConvertTo<Vanbancanhans>();  
                    emp.VbcnId = snapshot.Id;  
                    return emp;  
                }  
                else  
                {  
                    return new Vanbancanhans();  
                }  
            }  
            catch  
            {  
                throw;  
            }  
        }  
        public async Task DeleteVanbancanhan(string id)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Vanbancanhan").Document(id);
                await empRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }
    } 
}