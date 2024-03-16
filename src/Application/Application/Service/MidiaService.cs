using Domain.Aggregates;
using Domain.Interfaces;
using Domain.Services;
using FFMpegCore;
using System.Drawing;

namespace Application.Services;
public class MidiaService : IMidiaService
{
    private readonly IMidiaRepository _repository;

    public MidiaService(IMidiaRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<Midia>> GetAllMidias()
    {
        return await _repository.GetMidiasAsync();
    }

    public async Task SplitMidia(string midiaPath)
    {
        var videoInfo = FFProbe.Analyse(midiaPath);
        var duration = videoInfo.Duration;

        var interval = TimeSpan.FromSeconds(20);

        //TODO onde sera?
        var outputFolder = @"C:\Projetos\FIAP_HACK\FIAPProcessaVideo\FIAPProcessaVideo\Images\";


        for (var currentTime = TimeSpan.Zero; currentTime < duration; currentTime += interval)
        {
            Console.WriteLine($"Processando frame: {currentTime}");

            var outputPath = Path.Combine(outputFolder, $"frame_at_{currentTime.TotalSeconds}.jpg");
            FFMpeg.Snapshot(midiaPath, outputPath, new Size(1920, 1080), currentTime);
        }
    }
}