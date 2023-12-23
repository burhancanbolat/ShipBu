
using System.ComponentModel.DataAnnotations;

namespace ShipBu.Models;

public class RegisterViewModel
{
    [Display(Name = "Ad")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Text)]
    public  string Name { get; set; }

    

    [Display(Name = "Parola")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "Parola Tekrar")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "{0} ile {1} alanı aynı olmalıdır!")]
    public string? PasswordCheck { get; set; }




    [Display(Name = "Email")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.EmailAddress)]
    public  string Email { get; set; }

    [Display(Name = "Telefon Numarası")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Text)]
    public  string PhoneNumber { get; set; }

    [Display(Name = "Adres")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Text)]
    public  string Adress { get; set; }

    [Display(Name = "Fatura Adresi")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Text)]
    public  string InvoiceAdress { get; set; }



    public string? ReturnUrl { get; set; }
}
