using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System.IO;

namespace QUANLIVANBAN.MyClass
{
    public class FireBaseConnect
    {
        public FireBaseConnect()
        {
        }
        public static FirestoreDb connectFB(string path)
        {
            var jsonString = File.ReadAllText(path);
            var builder = new FirestoreClientBuilder {JsonCredentials = jsonString};
            FirestoreDb db = FirestoreDb.Create("quanlivanban-26d21", builder.Build());
            return db;
        }
    }
}