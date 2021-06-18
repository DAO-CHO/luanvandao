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
    public class CoquanData
    {
        private readonly IWebHostEnvironment _env;
        public CoquanData(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<List<Coquans>> GetAllCoquan()
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                var list = new List<Coquans>();
                Query employeeQuery = db.Collection("Coquan");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Coquans st = documentSnapshot.ConvertTo<Coquans>();
                        st.CoQuan_id = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Coquans> sortedEmployeeList = list.OrderBy(x => x.CoQuan_id).ToList();
                return sortedEmployeeList;
            }
            catch
            {
                throw;
            }
        }
        public async Task AddCoquan(Coquans CoquanForecast)
        {
            try
            {
                DateTime id = DateTime.Now;
                string nn = id.ToString();
                string result = nn.Replace(" ", String.Empty).Replace("/", String.Empty).Replace(":", String.Empty);
                CoquanForecast.CoQuan_id=result;
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference colRef = db.Collection("Coquan").Document(CoquanForecast.CoQuan_id);
                CoquanForecast.CoQuan_id=colRef.Id.ToString();
                await colRef.SetAsync(CoquanForecast);
            }
            catch
            {
                throw;
            }
        }
        public async Task UpdateCoquan(Coquans Coquan)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Coquan").Document(Coquan.CoQuan_id);
                await empRef.SetAsync(Coquan, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Coquans> GetCoquanData(string id)  
        {  
            try  
            { 
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                DocumentReference docRef = db.Collection("Coquan").Document(id);  
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();  
  
                if (snapshot.Exists)  
                {  
                    Coquans emp = snapshot.ConvertTo<Coquans>();  
                    emp.CoQuan_id = snapshot.Id;  
                    return emp;  
                }  
                else  
                {  
                    return new Coquans();  
                }  
            }  
            catch  
            {  
                throw;  
            }  
        }  
        public async Task DeleteCoquan(string id)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Coquan").Document(id);
                await empRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
