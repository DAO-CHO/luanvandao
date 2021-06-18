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
    public class VanbandenData
    {
        private readonly IWebHostEnvironment _env;
        public VanbandenData(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<List<Vanbandens>> GetAllVanbanden()
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                var list = new List<Vanbandens>();
                Query employeeQuery = db.Collection("Vanbanden");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Vanbandens st = documentSnapshot.ConvertTo<Vanbandens>();
                        st.VBDenId = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Vanbandens> sortedEmployeeList = list.OrderBy(x => x.VBDenId).ToList();
                return sortedEmployeeList;
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> UploadAsync(IFileListEntry fileEntry)
        {
            var task = new FirebaseStorage("quanlivanban-26d21.appspot.com").Child("Vanbanden").Child(fileEntry.Name);
            var a = await task.PutAsync(fileEntry.Data);
            return a;
        }
        public async Task AddVanbanden(Vanbandens VanbandenForecast)
        {
            try
            {
                DateTime id = DateTime.Now;
                string nn = id.ToString();
                string result = nn.Replace(" ", String.Empty).Replace("/", String.Empty).Replace(":", String.Empty);
                VanbandenForecast.VBDenId=result;
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference colRef = db.Collection("Vanbanden").Document(VanbandenForecast.VBDenId);
                VanbandenForecast.VBDenId=colRef.Id.ToString();
                await colRef.SetAsync(VanbandenForecast);
            }
            catch
            {
                throw;
            }
        }
        public async Task UpdateVanbanden(Vanbandens VanbandenForecast)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Vanbanden").Document(VanbandenForecast.VBDenId);
                await empRef.SetAsync(VanbandenForecast, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Vanbandens> GetVanbandenData(string id)  
        {  
            try  
            {  
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                DocumentReference docRef = db.Collection("Vanbanden").Document(id);  
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();  
                if (snapshot.Exists)  
                {  
                    Vanbandens emp = snapshot.ConvertTo<Vanbandens>();  
                    emp.VBDenId = snapshot.Id;  
                    return emp;  
                }  
                else  
                {  
                    return new Vanbandens();  
                }  
            }  
            catch  
            {  
                throw;  
            }  
        } 
        public async Task<List<Vanbandens>> GetVanbandens_PB(string pb)
        { 
            try  
            { 
                var ss = "true";
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                var list = new List<Vanbandens>();
                Query employeeQuery = db.Collection("Vanbanden").WhereEqualTo("VBDen_phongban", pb).WhereEqualTo("VBDen_public", "false");
                Query employeeQuerys = db.Collection("Vanbanden").WhereEqualTo("VBDen_public", ss);
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                QuerySnapshot employeeQuerySnapshots = await employeeQuerys.GetSnapshotAsync();  
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    
                    if (documentSnapshot.Exists)
                    {
                        Vanbandens st = documentSnapshot.ConvertTo<Vanbandens>();
                        st.VBDenId = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshots.Documents)
                {
                    
                    if (documentSnapshot.Exists)
                    {
                        Vanbandens st = documentSnapshot.ConvertTo<Vanbandens>();
                        st.VBDenId = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Vanbandens> sortedEmployeeList = list.OrderBy(x => x.VBDen_TieuDe).ToList();
                return sortedEmployeeList; 
            }  
            catch  
            {  
                throw;  
            } 
        }
        
        public async Task<List<Vanbandens>> GetVanbandens_loaivb(string vbden)
        { 
            try  
            { 
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                var list = new List<Vanbandens>();
                Query employeeQuery = db.Collection("Vanbanden").WhereEqualTo("VBDen_TieuDe", vbden).WhereEqualTo("VBDen_public", "false");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();  
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    
                    if (documentSnapshot.Exists)
                    {
                        Vanbandens st = documentSnapshot.ConvertTo<Vanbandens>();
                        st.VBDenId = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Vanbandens> sortedEmployeeList = list.OrderBy(x => x.VBDen_TieuDe).ToList();
                return sortedEmployeeList; 
            }  
            catch  
            {  
                throw;  
            } 
        } 
        public async Task<List<Vanbandens>> GetVanbandens_cqbanhanh(string cqbanhanh)
        { 
            try  
            { 
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                var list = new List<Vanbandens>();
                Query employeeQuery = db.Collection("Vanbanden").WhereEqualTo("VBDen_NoiNhan", cqbanhanh);
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();  
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    
                    if (documentSnapshot.Exists)
                    {
                        Vanbandens st = documentSnapshot.ConvertTo<Vanbandens>();
                        st.VBDenId = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Vanbandens> sortedEmployeeList = list.OrderBy(x => x.VBDen_NoiNhan).ToList();
                return sortedEmployeeList; 
            }  
            catch  
            {  
                throw;  
            } 
        } 
        public async Task<List<Vanbandens>> GetVanbandens_ndvanban(string ndvanban)
        { 
            try  
            { 
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                var list = new List<Vanbandens>();
                Query employeeQuery = db.Collection("Vanbanden").WhereGreaterThan("VBDen_NoiDung", ndvanban);
                //Query employeeQuery = db.Collection("Vanbanden");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();  
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    
                    if (documentSnapshot.Exists)
                    {
                        Vanbandens st = documentSnapshot.ConvertTo<Vanbandens>();
                         st.VBDenId = documentSnapshot.Id;
                            list.Add(st);
                        //if(st.VBDen_NoiDung.Contains(ndvanban)){}
                        
                    }
                }
                List<Vanbandens> sortedEmployeeList = list.OrderBy(x => x.VBDen_NoiNhan).ToList();
                return sortedEmployeeList; 
            }  
            catch  
            {  
                throw;  
            } 
        } 
        public async Task DeleteVanbanden(string id)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Vanbanden").Document(id);
                await empRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }
    }  
}