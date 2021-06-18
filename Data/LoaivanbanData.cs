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
    public class LoaivanbanData
    {
        private readonly IWebHostEnvironment _env;
        public LoaivanbanData(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<List<Loaivanbans>> GetAllLoaivanban()
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                var list = new List<Loaivanbans>();
                Query employeeQuery = db.Collection("Loaivanban");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Loaivanbans st = documentSnapshot.ConvertTo<Loaivanbans>();
                        st.LoaiVB_Id = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Loaivanbans> sortedEmployeeList = list.OrderBy(x => x.LoaiVB_Id).ToList();
                return sortedEmployeeList;
            }
            catch
            {
                throw;
            }
        }
        public async Task AddLoaivanban(Loaivanbans LoaivanbanForecast)
        {
            try
            {
                DateTime id = DateTime.Now;
                string nn = id.ToString();
                string result = nn.Replace(" ", String.Empty).Replace("/", String.Empty).Replace(":", String.Empty);
                LoaivanbanForecast.LoaiVB_Id=result;
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference colRef = db.Collection("Loaivanban").Document(LoaivanbanForecast.LoaiVB_Id);
                LoaivanbanForecast.LoaiVB_Id=colRef.Id.ToString();
                await colRef.SetAsync(LoaivanbanForecast);
            }
            catch
            {
                throw;
            }
        }
        public async Task UpdateLoaivanban(Loaivanbans Loaivanban)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Loaivanban").Document(Loaivanban.LoaiVB_Id);
                await empRef.SetAsync(Loaivanban, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Loaivanbans> GetLoaivanbanData(string id)  
        {  
            try  
            { 
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                DocumentReference docRef = db.Collection("Loaivanban").Document(id);  
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();  
  
                if (snapshot.Exists)  
                {  
                    Loaivanbans emp = snapshot.ConvertTo<Loaivanbans>();  
                    emp.LoaiVB_Id = snapshot.Id;  
                    return emp;  
                }  
                else  
                {  
                    return new Loaivanbans();  
                }  
            }  
            catch  
            {  
                throw;  
            }  
        }  
        public async Task DeleteLoaivanban(string id)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Loaivanban").Document(id);
                await empRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
