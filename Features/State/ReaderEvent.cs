public class ReaderEvent
{
	public DateTime EventTimeUTC { get; set; }
	public string Location { get; set; }
	public string ReaderDesc { get; set; }
	public string IdHash { get; set; }
	public int DevId { get; set; }
	public int Machine { get; set; }

	public ReaderEvent(DateTime eventTimeUTC, string location, string readerDesc, string idHash, int devId, int machine)
	{
		EventTimeUTC = eventTimeUTC;
		Location = location;
		ReaderDesc = readerDesc;
		IdHash = idHash;
		DevId = devId;
		Machine = machine;
	}
	public override string ToString()
	{
		return $"EventTimeUTC: {EventTimeUTC.ToString("D").Split(",")[0]}, Location: {Location}, ReaderDesc: {ReaderDesc}, IdHash: {IdHash}, DevId: {DevId}, Machine: {Machine}";
	}
}
