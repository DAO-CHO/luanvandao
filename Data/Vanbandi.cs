using Google.Cloud.Firestore;  
namespace QUANLIVANBAN.Data
{
    [FirestoreData]
    public class Vanbandis
    {
        [FirestoreProperty]  
        public string VBDi_id { get; set; }
        [FirestoreProperty]
        public string VBDi_TieuDe { get; set; }

        [FirestoreProperty] 
        public string VBDi_NgayGui { get; set; }
        [FirestoreProperty]
        public string VBDi_NguoiGui { get; set; }
        [FirestoreProperty]
        public string VBDi_NoiNhan { get; set; }
        [FirestoreProperty]
        public string VBDi_SoTrang { get; set; }
        [FirestoreProperty]
        public string VBDi_NoiDung { get; set; }
        [FirestoreProperty] 
        public string VBDi_SoDi { get; set; }
        [FirestoreProperty]
        public string VBDi_TrichYeu { get; set; }
        [FirestoreProperty]
        public string Dinhkem { get; set; }
        [FirestoreProperty]
        public string VBDi_public      { get; set; }
        [FirestoreProperty]
        public string VBDi_phongban     { get; set; }
    }
}
