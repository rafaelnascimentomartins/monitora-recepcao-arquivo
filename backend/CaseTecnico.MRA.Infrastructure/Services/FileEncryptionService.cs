
using CaseTecnico.MRA.CrossCutting.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace CaseTecnico.MRA.Infrastructure.Services;

public class FileEncryptionService : IFileEncryptionService
{
    public async Task SaveEncryptedAsync(Stream fileStream, string destinationFileName, string password)
    {
        // Obtém a pasta atual do projeto (bin/Debug/netX)
        var projectRoot = Directory.GetCurrentDirectory();

        // Define a pasta de uploads
        var uploadsDir = Path.Combine(projectRoot, "Uploads");

        // Cria a pasta se não existir
        if (!Directory.Exists(uploadsDir))
            Directory.CreateDirectory(uploadsDir);

        // Gera um arquivo temporário dentro da pasta Uploads
        var tempFilePath = Path.Combine(uploadsDir, Path.GetRandomFileName());

        // Salva o Stream temporariamente
        using (var tempFile = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
        {
            await fileStream.CopyToAsync(tempFile);
        }

        // Define o caminho final do arquivo criptografado
        var destinationPath = Path.Combine(uploadsDir, destinationFileName);

        // Criptografa e salva no destino final
        EncryptFile(tempFilePath, destinationPath, password);

        // Remove o arquivo temporário
        File.Delete(tempFilePath);
    }

    private void EncryptFile(string inputFile, string outputFile, string password)
    {
        using var aes = Aes.Create();
        var key = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes("Salt1234!@#$"), 10000);
        aes.Key = key.GetBytes(32);
        aes.IV = key.GetBytes(16);

        using var fsOutput = new FileStream(outputFile, FileMode.Create);
        using var cryptoStream = new CryptoStream(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write);
        using var fsInput = new FileStream(inputFile, FileMode.Open);

        fsInput.CopyTo(cryptoStream);
    }

}
