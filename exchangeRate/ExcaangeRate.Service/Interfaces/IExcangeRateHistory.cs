using ExcangeRate.Service.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExcangeRate.Service.Interfaces
{
	public interface IExcangeRateHistory
	{
		Task<StatisticResponse> ExcangeHistory(string Dates, string Currency);
		List<string> DateSpliter(string Dates);
		List<string> CurrencySpliter(string Currency);
		Task<ExcangeRateModelResponse> GetExchangeRateModel(string date, HttpClient httpClient, List<string> currencies);
	}
}
