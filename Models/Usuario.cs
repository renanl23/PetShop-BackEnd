namespace PetShop_BackEnd.Models
{
    public class Usuario
{
    public long id { get; set; }

    public string? name {get; set;}
    public string? username { get; set; }
    public string? password { get; set; }

    public int tipo { get; set; }
}
}