using Microsoft.AspNetCore.Components.Forms;

public class FileProcessingService {
	public List<ReaderEvent>? ReaderEvents;

	public event Action? OnDataProcessed;

	public async Task ProcessFileAsync(IBrowserFile file) {
		ReaderEvents = new List<ReaderEvent>();
		await using var stream = file.OpenReadStream(long.MaxValue);
		using var sr = new StreamReader(stream);
		await sr.ReadLineAsync();

		string? line;
		while ((line = await sr.ReadLineAsync()) != null)
		{
			string[] values = line.Split(',');
			if (values.Length == 6)
			{
				ReaderEvents.Add(new ReaderEvent(
					DateTime.Parse(values[0].Trim('"')),
					values[1].Trim('"'),
					values[2].Trim('"'),
					values[3].Trim('"'),
					Convert.ToInt32(values[4].Trim('"')),
					Convert.ToInt32(values[5].Trim('"'))
				));
			}
		}
		OnDataProcessed?.Invoke();
	}
}
