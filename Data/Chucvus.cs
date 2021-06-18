using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;  
namespace QUANLIVANBAN.Data
{
    [FirestoreData]
    public class Chucvus
    {
        [FirestoreProperty]
        public string ChucVu_Id { get; set; }
        [FirestoreProperty]
        [Required(ErrorMessage = "Tên Chức Vụ là bắt buộc")]
        [Range(minimum: 10, 99999999, ErrorMessage = "Tên ít nhất là 10 ký tự")]
        public string ChucVu { get; set; }
        [FirestoreProperty]
        [Required(ErrorMessage = "Ghi Chu là bắt buộc")]
        [Range(minimum: 10, 99999999, ErrorMessage = "Ghi chú ít nhất là 10 ký tự")]
        public string ChucVu_Ghichu { get; set; }  
    }
}
