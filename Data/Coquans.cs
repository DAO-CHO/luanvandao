using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;  
namespace QUANLIVANBAN.Data
{
    [FirestoreData]
    public class Coquans
    {
        [FirestoreProperty]
        public string CoQuan_id { get; set; }
        [FirestoreProperty]
        [Required(ErrorMessage = "Tên Cơ Quan là bắt buộc")]
        [Range(minimum: 10, 99999999, ErrorMessage = "Tên Cơ Quan ít nhất là 10 ký tự")]
        public string CoQuan { get; set; }
        [FirestoreProperty]
        [Required(ErrorMessage = "Ghi Chu là bắt buộc")]
        [Range(minimum: 10, 99999999, ErrorMessage = "Ghi chú ít nhất là 10 ký tự")]
        public string CoQuan_GhiChu { get; set; }  
    }
}
