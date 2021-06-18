using Google.Cloud.Firestore;  
namespace QUANLIVANBAN.Data
{
    [FirestoreData]
    public class Taikhoans
    {
        [FirestoreProperty]
        public string TaikhoanId { get; set; }
        [FirestoreProperty]
        public string Taikhoangoogleid { get; set; }
        [FirestoreProperty]
        public string Taikhoanhoten { get; set; } 
        [FirestoreProperty]
        public string TaikhoanpassWorld { get; set; }
        [FirestoreProperty]
        public string Taikhoansdt { get; set; }
        [FirestoreProperty]
        public string Taikhoanemail { get; set; }  
        [FirestoreProperty]
        public string Taikhoandiachi { get; set; }
        [FirestoreProperty]
        public string Taikhoanhinhanh { get; set; }
        [FirestoreProperty]
        public string Taikhoan_Quyen { get; set; }
        [FirestoreProperty]
        public string Taikhoan_PhongBan { get; set; }
        [FirestoreProperty]
        public string Taikhoan_ChucVu { get; set; }
        [FirestoreProperty]
        public string Taikhoan_NgayBatDau { get; set; }
        [FirestoreProperty]
        public string Taikhoan_NgayKetThuc { get; set; }

 
    }
}
