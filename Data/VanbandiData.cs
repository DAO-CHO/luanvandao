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
    public class VanbandiData
    {
        private readonly IWebHostEnvironment _env;
        public VanbandiData(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<List<Vanbandis>> GetAllVanbandi()
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                var list = new List<Vanbandis>();
                Query employeeQuery = db.Collection("Vanbandi");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Vanbandis st = documentSnapshot.ConvertTo<Vanbandis>();
                        st.VBDi_id = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Vanbandis> sortedEmployeeList = list.OrderBy(x => x.VBDi_id).ToList();
                return sortedEmployeeList;
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> UploadAsync(IFileListEntry fileEntry)
        {
            var task = new FirebaseStorage("quanlivanban-26d21.appspot.com").Child("Vanbandi").Child(fileEntry.Name);
            var a = await task.PutAsync(fileEntry.Data);
            return a;
        }
        public async Task AddVanbandi(Vanbandis VanbandiForecast)
        {
            try
            {
                DateTime id = DateTime.Now;
                string nn = id.ToString();
                string result = nn.Replace(" ", String.Empty).Replace("/", String.Empty).Replace(":", String.Empty);
                VanbandiForecast.VBDi_id = result;
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference colRef = db.Collection("Vanbandi").Document(VanbandiForecast.VBDi_id);
                VanbandiForecast.VBDi_id = colRef.Id.ToString();
                await colRef.SetAsync(VanbandiForecast);
            }
            catch
            {
                throw;
            }
        }
        public async Task UpdateVanbandi(Vanbandis VanbandiForecast)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Vanbandi").Document(VanbandiForecast.VBDi_id);
                await empRef.SetAsync(VanbandiForecast, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Vanbandis> GetVanbandiData(string id)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference docRef = db.Collection("Vanbandi").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                if (snapshot.Exists)
                {
                    Vanbandis emp = snapshot.ConvertTo<Vanbandis>();
                    emp.VBDi_id = snapshot.Id;
                    return emp;
                }
                else
                {
                    return new Vanbandis();
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Vanbandis>> GetVanbandis_PB(string pb)
        { 
            try  
            { 
                var ss = "true";
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                var list = new List<Vanbandis>();
                Query employeeQuery = db.Collection("Vanbandi").WhereEqualTo("VBDi_phongban", pb).WhereEqualTo("VBDi_public", "false");
                Query employeeQuerys = db.Collection("Vanbandi").WhereEqualTo("VBDi_public", ss);
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                QuerySnapshot employeeQuerySnapshots = await employeeQuerys.GetSnapshotAsync();  
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    
                    if (documentSnapshot.Exists)
                    {
                        Vanbandis st = documentSnapshot.ConvertTo<Vanbandis>();
                        st.VBDi_id = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshots.Documents)
                {
                    
                    if (documentSnapshot.Exists)
                    {
                        Vanbandis st = documentSnapshot.ConvertTo<Vanbandis>();
                        st.VBDi_id = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Vanbandis> sortedEmployeeList = list.OrderBy(x => x.VBDi_TieuDe).ToList();
                return sortedEmployeeList; 
            }  
            catch  
            {  
                throw;  
            } 
        }

        public async Task<List<Vanbandis>> GetVanbandis_loaivb(string vbdi)
        { 
            try  
            { 
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                var list = new List<Vanbandis>();
                Query employeeQuery = db.Collection("Vanbandi").WhereEqualTo("VBDi_TieuDe", vbdi);
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();  
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    
                    if (documentSnapshot.Exists)
                    {
                        Vanbandis st = documentSnapshot.ConvertTo<Vanbandis>();
                        st.VBDi_id = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Vanbandis> sortedEmployeeList = list.OrderBy(x => x.VBDi_TieuDe).ToList();
                return sortedEmployeeList; 
            }  
            catch  
            {  
                throw;  
            } 
        } 


        public async Task<List<Vanbandis>> GetVanbandis_ndvanban(string ndvanban)
        { 
            try  
            { 
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                var list = new List<Vanbandis>();
                Query employeeQuery = db.Collection("Vanbandi").WhereGreaterThan("VBDi_NoiDung", ndvanban);
                //Query employeeQuery = db.Collection("Vanbanden");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();  
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    
                    if (documentSnapshot.Exists)
                    {
                        Vanbandis st = documentSnapshot.ConvertTo<Vanbandis>();
                         st.VBDi_id = documentSnapshot.Id;
                            list.Add(st);
                        //if(st.VBDen_NoiDung.Contains(ndvanban)){}
                        
                    }
                }
                List<Vanbandis> sortedEmployeeList = list.OrderBy(x => x.VBDi_NguoiGui).ToList();
                return sortedEmployeeList; 
            }  
            catch  
            {  
                throw;  
            } 
        } 
        public async Task DeleteVanbandi(string id)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Vanbandi").Document(id);
                await empRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}