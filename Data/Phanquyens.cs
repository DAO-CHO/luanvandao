using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;  
namespace QUANLIVANBAN.Data
{
    [FirestoreData]
    public class Phanquyens
    {
        [FirestoreProperty]
        public string PhanQuyen_Id { get; set; }
        [FirestoreProperty]
        [Required(ErrorMessage = " Không được để trống")]
        [Range(minimum: 10, 99999999, ErrorMessage = "Ít nhất là 10 ký tự")]
        public string PhanQuyen_Quyen { get; set; } 
        [FirestoreProperty]
        [Required(ErrorMessage = " Không được để trống")]
        [Range(minimum: 10, 99999999, ErrorMessage = "Ít nhất là 10 ký tự")]
        public string PhanQuyen_Ghichu { get; set; }  
    }
}
