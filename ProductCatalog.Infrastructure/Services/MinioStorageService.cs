using Amazon.S3;
using Amazon.S3.Model;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Infrastructure.Services;

/// <summary>
/// Implementacao do servico de armazenamento usando Minio via S3
/// </summary>
public class MinioStorageService : IStorageService
{
    private readonly IAmazonS3 _s3Client;
    private const string BucketName = "product-images";

    /// <summary>
    /// Construtor que configura o cliente S3 para o Minio
    /// </summary>
    public MinioStorageService()
    {
        var config = new AmazonS3Config
        {
            ServiceURL = "http://minio:9000",
            ForcePathStyle = true
        };

        _s3Client = new AmazonS3Client(
            "admin",
            "admin123",
            config
        );
    }

    /// <summary>
    /// Envia o arquivo para o bucket do Minio de forma assincrona
    /// </summary>
    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
    {
        var request = new PutObjectRequest
        {
            BucketName = BucketName,
            Key = fileName,
            InputStream = fileStream,
            ContentType = contentType
        };

        await _s3Client.PutObjectAsync(request);

        return fileName;
    }

    /// <summary>
    /// Remove um objeto do bucket de forma assincrona
    /// </summary>
    public async Task DeleteAsync(string fileName)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = BucketName,
            Key = fileName
        };

        await _s3Client.DeleteObjectAsync(request);
    }
}