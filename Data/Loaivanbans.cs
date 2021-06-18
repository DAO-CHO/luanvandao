using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;  
namespace QUANLIVANBAN.Data
{
    [FirestoreData]
    public class Loaivanbans
    {
        [FirestoreProperty]
        public string LoaiVB_Id { get; set; }
        [FirestoreProperty]
        [Required(ErrorMessage = "Tên Loại Văn Bản là bắt buộc")]
        [Range(minimum: 10, 99999999, ErrorMessage = "Tên Loại Văn Bản ít nhất là 10 ký tự")]
        public string LoaiVanBan { get; set; }
        [FirestoreProperty]
        [Required(ErrorMessage = "Ghi Chu là bắt buộc")]
        [Range(minimum: 10, 99999999, ErrorMessage = "Ghi chú ít nhất là 10 ký tự")]
        public string LoaiVB_Ghichu { get; set; }  
    }
}
