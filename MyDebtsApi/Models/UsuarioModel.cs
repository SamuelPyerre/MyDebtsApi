namespace MyDebtsApi.Models;

public class UsuarioModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<DividaModel> Dividas { get; set; }
}