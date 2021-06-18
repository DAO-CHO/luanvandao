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
    public class CoquanbanhanhData
    {
        private readonly IWebHostEnvironment _env;
        public CoquanbanhanhData(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<List<Coquanbanhanhs>> GetAllCoquanbanhanh()
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                var list = new List<Coquanbanhanhs>();
                Query employeeQuery = db.Collection("Coquanbanhanh");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Coquanbanhanhs st = documentSnapshot.ConvertTo<Coquanbanhanhs>();
                        st.CQBH_id = documentSnapshot.Id;
                        list.Add(st);
                    }
                }
                List<Coquanbanhanhs> sortedEmployeeList = list.OrderBy(x => x.CQBH_id).ToList();
                return sortedEmployeeList;
            }
            catch
            {
                throw;
            }
        }
        public async Task AddCoquanbanhanh(Coquanbanhanhs CoquanbanhanhForecast)
        {
            try
            {
                DateTime id = DateTime.Now;
                string nn = id.ToString();
                string result = nn.Replace(" ", String.Empty).Replace("/", String.Empty).Replace(":", String.Empty);
                CoquanbanhanhForecast.CQBH_id=result;
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference colRef = db.Collection("Coquanbanhanh").Document(CoquanbanhanhForecast.CQBH_id);
                CoquanbanhanhForecast.CQBH_id=colRef.Id.ToString();
                await colRef.SetAsync(CoquanbanhanhForecast);
            }
            catch
            {
                throw;
            }
        }
        public async Task UpdateCoquanbanhanh(Coquanbanhanhs Coquanbanhanh)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Coquanbanhanh").Document(Coquanbanhanh.CQBH_id);
                await empRef.SetAsync(Coquanbanhanh, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Coquanbanhanhs> GetCoquanbanhanhData(string id)  
        {  
            try  
            { 
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path); 
                DocumentReference docRef = db.Collection("Coquanbanhanh").Document(id);  
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();  
  
                if (snapshot.Exists)  
                {  
                    Coquanbanhanhs emp = snapshot.ConvertTo<Coquanbanhanhs>();  
                    emp.CQBH_id = snapshot.Id;  
                    return emp;  
                }  
                else  
                {  
                    return new Coquanbanhanhs();  
                }  
            }  
            catch  
            {  
                throw;  
            }  
        }  
        public async Task DeleteCoquanbanhanh(string id)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\connFire.json");
                FirestoreDb db = FireBaseConnect.connectFB(path);
                DocumentReference empRef = db.Collection("Coquanbanhanh").Document(id);
                await empRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
