using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;
using Azure.Storage.Blobs.Models;
using System.IO;

public class BlobService
{
    private readonly string connectionString;
    private readonly string containerName;

    public BlobService(string connectionString, string containerName)
    {
        this.connectionString = connectionString;
        this.containerName = containerName;
    }

    public async Task<Uri> UploadFileToBlob(string blobName, Stream content, string contentType)
    {
        var blobServiceClient = new BlobServiceClient(connectionString);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = blobContainerClient.GetBlobClient(blobName);

        await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });

        return blobClient.Uri;
    }

    public string GenerateSasTokenForBlob(string blobName, int validHours = 24)
    {
        if (string.IsNullOrWhiteSpace(blobName))
        {
            // Retorna un valor predeterminado o null para indicar que no hay imagen disponible
            return "Sin imagen disponible";
        }
        var blobServiceClient = new BlobServiceClient(connectionString);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = blobContainerClient.GetBlobClient(blobName);

        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = containerName,
            BlobName = blobName,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(validHours)
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        // Genera el token SAS
        var sasToken = blobClient.GenerateSasUri(sasBuilder).Query;

        // Añade un parámetro de consulta único para evitar el cache del navegador
        var uniqueParam = $"cacheBuster={Guid.NewGuid()}";

        // Asegúrate de devolver la URI del blob con el token SAS y el parámetro único aplicados
        return $"{blobClient.Uri}{sasToken}&{uniqueParam}";
    }
}
