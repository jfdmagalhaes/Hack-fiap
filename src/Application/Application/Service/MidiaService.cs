using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Domain.Aggregates;
using Domain.Helpers;
using Domain.Interfaces;
using Domain.Models;
using Domain.Services;
using FFMpegCore;
using System.Drawing;
using System.IO.Compression;

namespace Application.Services;
public class MidiaService : IMidiaService
{
    private readonly IMidiaRepository _repository;
    private readonly BlobContainerClient _containerClient;
    private readonly AppSettings _appSettings;

    public MidiaService(IMidiaRepository repository, AppSettings appSettings)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        string azureConnectionString = appSettings.AzureBlobStorage.ConnectionString;
        string containerName = appSettings.AzureBlobStorage.ContainerName;

        _containerClient = new BlobContainerClient(azureConnectionString, containerName);
    }

    public async Task<IEnumerable<Midia>> GetAllMidias()
    {
        return await _repository.GetMidiasAsync();
    }

    public async Task SaveMidiaAndSplit(MidiaModel model)
    {
        try
        {
            string fileExtension = Path.GetExtension(model.FormFile.FileName);

            using MemoryStream fileUploadStream = new MemoryStream();


            var filePath = Path.Combine(Path.GetTempPath(), model.FormFile.FileName); // Ou o diretório que você deseja salvar
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.FormFile.CopyToAsync(stream);
            }

            var outputFolder = @"C:\Users\mario\Downloads\FIAP_DOTNET_NET1_23\FIAP_DOTNET_NET1_23\FIAPProcessaVideo\Images";

            Directory.CreateDirectory(outputFolder);

            var videoInfo = FFProbe.Analyse(filePath);
            var duration = videoInfo.Duration;

            var interval = TimeSpan.FromSeconds(20);

            for (var currentTime = TimeSpan.Zero; currentTime < duration; currentTime += interval)
            {
                Console.WriteLine($"Processando frame: {currentTime}");

                var outputPath = Path.Combine(outputFolder, $"frame_at_{currentTime.TotalSeconds}.jpg");
                FFMpeg.Snapshot(filePath, outputPath, new Size(1920, 1080), currentTime);

                BlobContainerClient blobContainerClient = _containerClient;
                var uniqueName = Guid.NewGuid().ToString() + fileExtension;
                BlobClient blobClient = blobContainerClient.GetBlobClient(uniqueName);

                using (var stream = new FileStream(outputPath, FileMode.Create))
                {
                    await model.FormFile.CopyToAsync(stream);
                }

                blobClient.Upload(outputPath, new BlobUploadOptions()
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = "image/jpg"
                    }
                }, cancellationToken: default);
            }

            Console.WriteLine("Processo finalizado.");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}