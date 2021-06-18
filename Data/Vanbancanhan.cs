using Google.Cloud.Firestore;  
namespace QUANLIVANBAN.Data
{
    [FirestoreData]
    public class Vanbancanhans
    {
        [FirestoreProperty]
        public string VbcnId { get; set; }
        [FirestoreProperty]
        public string Tenvbcn{ get; set; }
        [FirestoreProperty]
        public string Loaivanban { get; set; }
        [FirestoreProperty]
        public string Nguoitao { get; set; }
        [FirestoreProperty] 
        public string Ngaytao { get; set; }
        [FirestoreProperty]
        public string Sotrang { get; set; }
        [FirestoreProperty]
        public string Tomtat { get; set; }
        [FirestoreProperty]
        public string Dinhkem { get; set; }

       
    }
}
