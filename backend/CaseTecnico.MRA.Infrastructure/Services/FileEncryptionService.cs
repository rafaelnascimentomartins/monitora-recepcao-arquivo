
using CaseTecnico.MRA.CrossCutting.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace CaseTecnico.MRA.Infrastructure.Services;

public class FileEncryptionService : IFileEncryptionService
{
    public async Task SaveEncryptedAsync(
        Stream fileStream, 
        string originalFileName, 
        string password,
        string backendRootPath)
    {
        // Caminho da pasta UploadsArquivos dentro do backend
        var uploadsDir = Path.Combine(backendRootPath, "UploadsArquivos");

        if (!Directory.Exists(uploadsDir))
            Directory.CreateDirectory(uploadsDir);

        // Extrai nome e extensão do arquivo enviado
        var fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileName);
        var ext = Path.GetExtension(originalFileName);

        // Nome final com ticks
        var finalFileName = $"{fileNameWithoutExt}-{DateTime.UtcNow.Ticks}{ext}";

        var finalPath = Path.Combine(uploadsDir, finalFileName);

        // Criptografar diretamente o Stream → sem arquivo temporário
        await EncryptStreamToFileAsync(fileStream, finalPath, password);
    }

    private async Task EncryptStreamToFileAsync(Stream input, string outputFile, string password)
    {
        using var aes = Aes.Create();

        var key = new Rfc2898DeriveBytes(
            Encoding.UTF8.GetBytes(password),
            Encoding.UTF8.GetBytes("Salt1234!@#$"),
            10000,
            HashAlgorithmName.SHA256
        );

        aes.Key = key.GetBytes(32);
        aes.IV = key.GetBytes(16);

        using var fsOutput = new FileStream(outputFile, FileMode.Create);
        using var cryptoStream = new CryptoStream(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write);

        input.Position = 0; // Garantir início
        await input.CopyToAsync(cryptoStream);
    }
}
