using ExcangeRate.API.Models;
using ExcangeRate.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExcangeRate.API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ExchangeRateHistoryController : Controller
	{
		private readonly IExcangeRateHistory _excangeRateHistory;

		public ExchangeRateHistoryController(IExcangeRateHistory excangeRateHistory)
		{
			_excangeRateHistory = excangeRateHistory;
		}

		[HttpPost(Name = "getExcangeHistory")]
		public async Task<IActionResult> GetExcangeRateHistory([FromBody] ExcangeRateModel excangeRatesModel)
		{
			return Ok(await _excangeRateHistory.ExcangeHistory(excangeRatesModel.Dates, excangeRatesModel.Currency));
		}
	}
}