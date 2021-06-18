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
    public class PhanQuyenData
    {
        private readonly IWebHostEnvironment _env;
        public PhanQuyenData(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<List<Phanquyens>> GetAllPhanquyen()
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                var list = new List<Phanquyens>();
                Query employeeQuery = db.Collection("Phanquyen");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Phanquyens st = documentSnapshot.ConvertTo<Phanquyens>();
                        st.PhanQuyen_Id = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Phanquyens> sortedEmployeeList = list.OrderBy(x => x.PhanQuyen_Id).ToList();
                return sortedEmployeeList;
            }
            catch
            {
                throw;
            }
        }
        public async Task AddPhanquyen(Phanquyens PhanquyenForecast)
        {
            try
            {
                DateTime id = DateTime.Now;
                string nn = id.ToString();
                string result = nn.Replace(" ", String.Empty).Replace("/", String.Empty).Replace(":", String.Empty);
                PhanquyenForecast.PhanQuyen_Id=result;
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference colRef = db.Collection("Phanquyen").Document(PhanquyenForecast.PhanQuyen_Id);
                PhanquyenForecast.PhanQuyen_Id=colRef.Id.ToString();
                await colRef.SetAsync(PhanquyenForecast);
            }
            catch
            {
                throw;
            }
        }
        public async Task UpdatePhanquyen(Phanquyens Phanquyen)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Phanquyen").Document(Phanquyen.PhanQuyen_Id);
                await empRef.SetAsync(Phanquyen, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Phanquyens> GetPhanquyenData(string id)  
        {  
            try  
            { 
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                DocumentReference docRef = db.Collection("Phanquyen").Document(id);  
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();  
  
                if (snapshot.Exists)  
                {  
                    Phanquyens emp = snapshot.ConvertTo<Phanquyens>();  
                    emp.PhanQuyen_Id = snapshot.Id;  
                    return emp;  
                }  
                else  
                {  
                    return new Phanquyens();  
                }  
            }  
            catch  
            {  
                throw;  
            }  
        }  
        public async Task DeletePhanquyen(string id)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Phanquyen").Document(id);
                await empRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
