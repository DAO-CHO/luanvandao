using Google.Cloud.Firestore;
namespace QUANLIVANBAN.Data
{
    [FirestoreData]
    public class InVanBan
    {
        [FirestoreProperty]
        public string TLVB_Id { get;set;}
        [FirestoreProperty]
        public string TLVB_TieuDe { get;set;}
        [FirestoreProperty]
        public string TLVB_DonVi1 { get;set;}
        [FirestoreProperty]
        public string TLVB_DonVi2 { get;set;}
        [FirestoreProperty]
        public string TLVB_QuyetDinhSo { get;set;}
        [FirestoreProperty]
        public string TLVB_ThanhPho { get;set;}
        [FirestoreProperty]
        public string TLVB_TieuDePhu { get;set;}
        [FirestoreProperty]
        public string TLVB_XuatTienTo { get;set;}
        [FirestoreProperty]
        public string TLVB_NoiDung { get;set;}
        [FirestoreProperty]
        public string TLVB_NoiNhan { get;set;}
        [FirestoreProperty]
        public string TLVB_ChucVu { get;set;}
        [FirestoreProperty]
        public string TLVB_NguoiKy { get;set;}
        [FirestoreProperty]
        public string TLVB_NoiDungVanBan { get;set;}
    }
}
