namespace Domain.Helpers;
public class AppSettings
{
    public MassTransit MassTransit { get; set; } = new();
}

public class MassTransit 
{ 
    public string NomeFila { get; set; } = string.Empty;
    public string Servidor { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}