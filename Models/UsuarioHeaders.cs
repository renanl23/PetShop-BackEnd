
using Microsoft.AspNetCore.Mvc;
public class UsuarioHeaders
{
    [FromHeader]
    public string? username { get; set; }
    public int? tipo { get; set; }
}