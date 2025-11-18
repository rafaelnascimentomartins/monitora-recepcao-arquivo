
namespace CaseTecnico.MRA.CrossCutting.Interfaces.Services;

public interface IFileEncryptionService
{
    Task SaveEncryptedAsync(Stream fileStream,
        string originalFileName,
        string password,
        string backendRootPath);
}
