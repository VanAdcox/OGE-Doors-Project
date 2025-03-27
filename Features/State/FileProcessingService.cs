using Microsoft.AspNetCore.Components.Forms;

public class FileProccessingService {
	private IBrowserFile? _rawFile;
	public List<ReaderEvent>? ReaderEvents;

	public event Action? OnDataProcessed;

	public async Task ProcessFileAsync(IBrowserFile file) {
		// TODO actually process file
		_rawFile = file;
		OnDataProcessed?.Invoke();
	}
}
