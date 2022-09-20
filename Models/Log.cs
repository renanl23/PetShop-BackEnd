using System.Net;
namespace PetShop_BackEnd.Models
{
    public class Log
{
    public long id { get; set; }
    public string? username { get; set; }

    public string? acao { get; set; }
    public string? endPoint { get; set; }
    public bool permitido { get; set; }
    public DateTime dateTime { get; set; }

}
}