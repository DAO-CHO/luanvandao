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
    public class ChucvuData
    {
        private readonly IWebHostEnvironment _env;
        public ChucvuData(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<List<Chucvus>> GetAllChucvu()
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                var list = new List<Chucvus>();
                Query employeeQuery = db.Collection("Chucvu");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Chucvus st = documentSnapshot.ConvertTo<Chucvus>();
                        st.ChucVu_Id = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Chucvus> sortedEmployeeList = list.OrderBy(x => x.ChucVu_Id).ToList();
                return sortedEmployeeList;
            }
            catch
            {
                throw;
            }
        }
        public async Task AddChucvu(Chucvus ChucvuForecast)
        {
            try
            {
                DateTime id = DateTime.Now;
                string nn = id.ToString();
                string result = nn.Replace(" ", String.Empty).Replace("/", String.Empty).Replace(":", String.Empty);
                ChucvuForecast.ChucVu_Id=result;
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference colRef = db.Collection("Chucvu").Document(ChucvuForecast.ChucVu_Id);
                ChucvuForecast.ChucVu_Id=colRef.Id.ToString();
                await colRef.SetAsync(ChucvuForecast);
            }
            catch
            {
                throw;
            }
        }
        public async Task UpdateChucVu(Chucvus Chucvu)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Chucvu").Document(Chucvu.ChucVu_Id);
                await empRef.SetAsync(Chucvu, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Chucvus> GetChucvuData(string id)  
        {  
            try  
            { 
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                DocumentReference docRef = db.Collection("Chucvu").Document(id);  
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();  
  
                if (snapshot.Exists)  
                {  
                    Chucvus emp = snapshot.ConvertTo<Chucvus>();  
                    emp.ChucVu_Id = snapshot.Id;  
                    return emp;  
                }  
                else  
                {  
                    return new Chucvus();  
                }  
            }  
            catch  
            {  
                throw;  
            }  
        }  
        public async Task DeleteChucvu(string id)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Chucvu").Document(id);
                await empRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
