public abstract class Alert {
	public DateTime DateOfAlertUTC;
	public List<ReaderEvent> RelevantScans;
	public String IdHash;
	public abstract int Severity { get; } // 2 = high, 1 = medium, 0 = low

	public abstract String getDescription();
	public Alert(DateTime dateOfAlertUTC, List<ReaderEvent> relevantScans) {
		this.DateOfAlertUTC = dateOfAlertUTC;
		this.RelevantScans = relevantScans;
		this.IdHash = relevantScans[0].IdHash;
	}
}

public class ImpossibleMovementAlert : Alert {
	public ImpossibleMovementAlert(DateTime dateOfAlertUTC, List<ReaderEvent> relevantScans) : base(dateOfAlertUTC, relevantScans) {}
	public override int Severity => 2;
	public override string getDescription() => $"{IdHash} scans at multiple readers within 3 seconds";
}
