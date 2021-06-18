using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;  
namespace QUANLIVANBAN.Data
{
    [FirestoreData]
    public class Vanbandens
    {
        [FirestoreProperty]
        public string VBDenId { get; set; }
        [FirestoreProperty]
        public string VBDen_TieuDe  { get; set; }
        [FirestoreProperty]
        public string VBDen_NgayNhan   { get; set; } 

         [FirestoreProperty]
        
        public string VBDen_NguoiNhan    { get; set; }

         [FirestoreProperty]
        
        public string VBDen_NguoiGui    { get; set; }
        [FirestoreProperty]
        public string VBDen_NoiNhan    { get; set; }

        [FirestoreProperty]
        public string VBDen_SoTrang    { get; set; }

        [FirestoreProperty]
        public string VBDen_NoiDung     { get; set; }

        [FirestoreProperty]
        
        public string  VBDen_SoDen    { get; set; }

        [FirestoreProperty]
        public string VBDen_TrichYeu     { get; set; }
        [FirestoreProperty]
        public string VBDen_HanTraLoi      { get; set; }

        [FirestoreProperty]
        public string VBDen_DinhKem      { get; set; }
        [FirestoreProperty]
        public string VBDen_public      { get; set; }
        [FirestoreProperty]
        public string VBDen_phongban     { get; set; }
    }
}
