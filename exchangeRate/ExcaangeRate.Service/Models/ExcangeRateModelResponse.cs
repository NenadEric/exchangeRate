using System.Collections.Generic;

namespace ExcangeRate.Service.Models
{
	public class ExcangeRateModelResponse
	{
		public Dictionary<string, double> Rates { get; set; }
		public string Date { get; set; }
		public string Base { get; set; }
	}
}
