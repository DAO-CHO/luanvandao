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
    public class PhongbanData
    {
        private readonly IWebHostEnvironment _env;
        public PhongbanData(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<List<Phongbans>> GetAllPhongban()
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                var list = new List<Phongbans>();
                Query employeeQuery = db.Collection("Phongban");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Phongbans st = documentSnapshot.ConvertTo<Phongbans>();
                        st.PhongBan_Id = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Phongbans> sortedEmployeeList = list.OrderBy(x => x.PhongBan_Id).ToList();
                return sortedEmployeeList;
            }
            catch
            {
                throw;
            }
        }
        public async Task AddPhongban(Phongbans PhongbanForecast)
        {
            try
            {
                DateTime id = DateTime.Now;
                string nn = id.ToString();
                string result = nn.Replace(" ", String.Empty).Replace("/", String.Empty).Replace(":", String.Empty);
                PhongbanForecast.PhongBan_Id=result;
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference colRef = db.Collection("Phongban").Document(PhongbanForecast.PhongBan_Id);
                PhongbanForecast.PhongBan_Id=colRef.Id.ToString();
                await colRef.SetAsync(PhongbanForecast);
            }
            catch
            {
                throw;
            }
        }
        public async Task UpdatePhongban(Phongbans Phongban)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Phongban").Document(Phongban.PhongBan_Id);
                await empRef.SetAsync(Phongban, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Phongbans> GetPhongbanData(string id)  
        {  
            try  
            { 
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                DocumentReference docRef = db.Collection("Phongban").Document(id);  
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();  
  
                if (snapshot.Exists)  
                {  
                    Phongbans emp = snapshot.ConvertTo<Phongbans>();  
                    emp.PhongBan_Id = snapshot.Id;  
                    return emp;  
                }  
                else  
                {  
                    return new Phongbans();  
                }  
            }  
            catch  
            {  
                throw;  
            }  
        }  
        public async Task DeletePhongban(string id)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Phongban").Document(id);
                await empRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
