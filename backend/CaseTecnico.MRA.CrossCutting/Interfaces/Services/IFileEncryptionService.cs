
namespace CaseTecnico.MRA.CrossCutting.Interfaces.Services;

public interface IFileEncryptionService
{
    Task SaveEncryptedAsync(Stream fileStream, string destinationPath, string password);
}
