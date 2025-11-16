
namespace CaseTecnico.MRA.Application.Settings;

public class AppSettings
{
    public FileEncryptionSettings FileEncryption { get; set; } = null!;
}


public class FileEncryptionSettings
{
    public string? DestinationPath { get; set; }
    public string? FileKey { get; set; }
}