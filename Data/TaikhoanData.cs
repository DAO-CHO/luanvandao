using Google.Cloud.Firestore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System;
using System.Security.Cryptography;
using System.Text;
using QUANLIVANBAN.MyClass;
using BlazorInputFile;
using Firebase.Storage.Client;
using Microsoft.AspNetCore.Hosting;
namespace QUANLIVANBAN.Data
{ 
    public class TaikhoanData: Taikhoans
    {
        public Taikhoans loginup = new Taikhoans();
        private readonly IWebHostEnvironment _env;
        public TaikhoanData(IWebHostEnvironment env)
        {
            _env = env;
        }
        public virtual Task Printvanban(Taikhoans vanban)
        { 
            loginup = vanban;
            return Task.FromResult(vanban);
        }
        public virtual Task<Taikhoans> shownoidung()
        {
            try
            {
                return Task.FromResult(loginup);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Taikhoans>> GetAllTaikhoan()
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                var list = new List<Taikhoans>();
                Query employeeQuery = db.Collection("Taikhoan");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Taikhoans st = documentSnapshot.ConvertTo<Taikhoans>();
                        st.TaikhoanId = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Taikhoans> sortedEmployeeList = list.OrderBy(x => x.TaikhoanId).ToList();
                return sortedEmployeeList;
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> UploadAsync(IFileListEntry fileEntry)
        {
            var task = new FirebaseStorage("quanlivanban-26d21.appspot.com").Child("Hinhanh").Child(fileEntry.Name);
            var a = await task.PutAsync(fileEntry.Data);
            return a;
        }
        public async Task AddTaikhoan(Taikhoans TaikhoanForecast)
        {
            try
            {
                DateTime id = DateTime.Now;
                string nn = id.ToString();
                string result = nn.Replace(" ", String.Empty).Replace("/", String.Empty).Replace(":", String.Empty);
                TaikhoanForecast.TaikhoanId=result;
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference colRef = db.Collection("Taikhoan").Document(TaikhoanForecast.TaikhoanId);
                TaikhoanForecast.TaikhoanId=colRef.Id.ToString();
                SHA256 sha256Hash = SHA256.Create();
                string hash = GetHash(sha256Hash, TaikhoanForecast.TaikhoanpassWorld);
                TaikhoanForecast.TaikhoanpassWorld=hash;
                await colRef.SetAsync(TaikhoanForecast);   
            }
            catch
            {
                throw;
            }
        }
        public async Task AddTaikhoan_gg(Taikhoans TaikhoanForecast)
        {
            try
            {
                DateTime id = DateTime.Now;
                string nn = id.ToString();
                string result = nn.Replace(" ", String.Empty).Replace("/", String.Empty).Replace(":", String.Empty);
                TaikhoanForecast.TaikhoanId=result;
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference colRef = db.Collection("Taikhoan").Document(TaikhoanForecast.TaikhoanId);
                TaikhoanForecast.TaikhoanId=colRef.Id.ToString();
                await colRef.SetAsync(TaikhoanForecast);   
            }
            catch
            {
                throw;
            }
        }
        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            for(int i = 0; i<data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public async Task UpdateTaikhoan(Taikhoans TaikhoanForecast)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Taikhoan").Document(TaikhoanForecast.TaikhoanId);
                await empRef.SetAsync(TaikhoanForecast, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Taikhoans> GetTaikhoanData(string id)  
        {  
            try  
            {  
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                DocumentReference docRef = db.Collection("Taikhoan").Document(id);  
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();  
                if (snapshot.Exists)  
                {  
                    Taikhoans emp = snapshot.ConvertTo<Taikhoans>();  
                    emp.TaikhoanId = snapshot.Id;  
                    return emp;  
                }  
                else  
                {  
                    return new Taikhoans();  
                }  
            }  
            catch  
            {  
                throw;  
            }  
        } 
        private string quyen;
        public async Task<Taikhoans> Getemail(string email)
        { 
            try  
            { 
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                Query employeeQuery = db.Collection("Taikhoan").WhereEqualTo("Taikhoanemail", email);
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();  
                if (employeeQuerySnapshot.Count>0)  
                {  
                    Taikhoans emp = new Taikhoans(); 
                    emp.Taikhoanemail = email; 
                    emp.Taikhoan_Quyen=quyen;
                    return emp;  
                }  
                else  
                {  
                    return new Taikhoans();  
                }  
            }  
            catch  
            {  
                throw;  
            } 
        }
        public async Task<List<Taikhoans>> getemail(string email)
        { 
            try  
            { 
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                var list = new List<Taikhoans>();
                Query employeeQuery = db.Collection("Taikhoan").WhereEqualTo("Taikhoanemail", email);
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();  
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    
                    if (documentSnapshot.Exists)
                    {
                        Taikhoans st = documentSnapshot.ConvertTo<Taikhoans>();
                        st.TaikhoanId = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Taikhoans> sortedEmployeeList = list.OrderBy(x => x.TaikhoanId).ToList();
                return sortedEmployeeList; 
            }  
            catch  
            {  
                throw;  
            } 
        }  
        public async Task Deletetaikhoan(string id)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Taikhoan").Document(id);
                await empRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }
    }  
}