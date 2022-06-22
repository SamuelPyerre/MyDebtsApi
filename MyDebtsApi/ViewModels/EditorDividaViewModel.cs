using System.ComponentModel.DataAnnotations;

namespace MyDebtsApi.ViewModels
{
    public class EditorDividaViewModel
    {
        [Required(ErrorMessage = "O Título da Dívida não pode ser vazio.")]
        [StringLength(40, ErrorMessage = "O campo Título não pode ultrapassar 40 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A Descrição da Dívida não pode ser vazia")]
        [StringLength(300, ErrorMessage = "O campo Descrição não pode ultrapassar 300 caracteres.")]
        public string Descricao { get; set; }
    }
}