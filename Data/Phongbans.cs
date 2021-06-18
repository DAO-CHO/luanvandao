using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;  
namespace QUANLIVANBAN.Data
{
    [FirestoreData]
    public class Phongbans
    {
        [FirestoreProperty]
        public string PhongBan_Id { get; set; }
        [FirestoreProperty]
        [Required(ErrorMessage = "Tên phòng ban là bắt buộc")]
        [Range(minimum: 10, 99999999, ErrorMessage = "Tên phòng ban ít nhất là 10 ký tự")]
        public string Phongban { get; set; } 
        [FirestoreProperty]
        public string Phongban_CoQuan { get; set; } 
        [FirestoreProperty]
        [Range(minimum: 10, 99999999, ErrorMessage = "Ghi chú ít nhất là 10 ký tự")]
        public string Phongban_Ghichu { get; set; } 
    }
}
