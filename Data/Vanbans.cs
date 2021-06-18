using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;  
namespace QUANLIVANBAN.Data
{
    [FirestoreData]
    public class Vanbans
    {
        [FirestoreProperty]
        public string VanBan_id { get; set; }
        [FirestoreProperty]
        [Required(ErrorMessage = "Tên Cơ Quan là bắt buộc")]
        [Range(minimum: 10, 99999999, ErrorMessage = "Tiêu Đề ít nhất là 10 ký tự")]
        public string VanBan_TieuDe { get; set; }
        [FirestoreProperty]
        [Required(ErrorMessage = "Không để trống")]
        [Range(minimum: 10, 99999999, ErrorMessage = "Ít nhất là 10 ký tự")]
        public string VanBan_So_KyHieu { get; set; }  
        [FirestoreProperty]
        [Required(ErrorMessage = "Không để trống")]
        [Range(minimum: 10, 99999999, ErrorMessage = "Ít nhất là 10 ký tự")]
        public string VanBan_TrichYeu { get; set; }
        [FirestoreProperty]
        public string VanBan_DinhKem { get; set; }
        [FirestoreProperty]
        [Required(ErrorMessage = "Chỉ chấp nhận kiểu số")]
        public string VanBan_SoTrang { get; set; }
        [FirestoreProperty]
        [Required(ErrorMessage = "Không để trống")]
        [Range(minimum: 10, 99999999, ErrorMessage = "Ít nhất là 10 ký tự")]
        public string VanBan_NoiDung { get; set; }
        [FirestoreProperty]
        [Required(ErrorMessage = "Không để trống")]
        [Range(minimum: 10, 99999999, ErrorMessage = "Ít nhất là 10 ký tự")]
        public string VanBan_GhiChu { get; set; }
    }
}
