using Newtonsoft.Json;

namespace ExcangeRate.Service.Models
{
	public class StatisticResponse
	{
		[JsonIgnore]
		public double MinimumRate { get; set; } = double.MaxValue;
		[JsonIgnore]
		public double MaximuRate { get; set; } = 0;
		[JsonIgnore]
		public string MiminumRateDate { get; set; }
		[JsonIgnore]
		public string MaximumRateDate { get; set; }
		[JsonIgnore]
		public double RateSum { get; set; }
		[JsonIgnore]
		public int RateCount { get; set; }

		public string Minimum => $"A min rate of {MinimumRate} on {MiminumRateDate}";
		public string Maximum => $"A max rate of {MaximuRate} on {MaximumRateDate}";
		public string Average => $"An average rate of {RateSum / RateCount}";
	}
}
