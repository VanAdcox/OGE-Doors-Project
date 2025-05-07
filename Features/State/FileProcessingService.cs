using Microsoft.AspNetCore.Components.Forms;

public class FileProcessingService {
	public List<ReaderEvent>? ReaderEvents;
	public DateTime? StartDate { get; private set; }
	public DateTime? EndDate { get; private set; }

	public event Action? OnDataProcessed;
	public event Action? OnFilterChanged;

	public async Task ProcessFileAsync(IBrowserFile file) {
		ReaderEvents = new List<ReaderEvent>();
		await using var stream = file.OpenReadStream(long.MaxValue);
		using var sr = new StreamReader(stream);
		await sr.ReadLineAsync();

		string? line;
		string lastLine = "";
		int duplicateLines = 0;
		while ((line = await sr.ReadLineAsync()) != null)
		{
			if(line != lastLine) {
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
			} else {
				duplicateLines++;
			}
			lastLine = line;
		}
		Console.WriteLine($"{duplicateLines} Duplicate Entries Found");
		OnDataProcessed?.Invoke();
	}

	public IEnumerable<ReaderEvent> GetFilteredEvents()
	{
		if (ReaderEvents == null) return Enumerable.Empty<ReaderEvent>();

		Console.WriteLine($"start: {StartDate}");
		Console.WriteLine($"end: {EndDate}");
		Console.WriteLine(ReaderEvents.Where(ev => (!StartDate.HasValue || ev.EventTimeUTC >= StartDate) && (!EndDate.HasValue || ev.EventTimeUTC <= EndDate)).Count());
		return ReaderEvents.Where(ev =>
				(!StartDate.HasValue || ev.EventTimeUTC >= StartDate) &&
				(!EndDate.HasValue || ev.EventTimeUTC <= EndDate));
	}
	public void SetDateRange(DateTime? start, DateTime? end)
	{
		StartDate = start;
		EndDate = end;
		OnFilterChanged?.Invoke();
	}
}
