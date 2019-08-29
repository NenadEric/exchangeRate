using ExcangeRate.Service.Interfaces;
using ExcangeRate.Service.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExcangeRate.Service.Services
{
	public class ExcangeRateHistoryService : IExcangeRateHistory
	{
		private readonly IConfiguration _configuration;
		private List<string> listOfDates;
		private List<string> listCurrency;

		public ExcangeRateHistoryService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public List<string> DateSpliter(string Dates)
		{
			listOfDates = new List<string>();
			string[] dates = Dates.Split(", ");
			foreach (string date in dates)
			{
				listOfDates.Add(date);
			}
			return listOfDates;
		}

		public List<string> CurrencySpliter(string Currency)
		{
			listCurrency = new List<string>();
			string[] currencies = Currency.Split("->");
			foreach (string currency in currencies)
			{
				listCurrency.Add(currency);
			}

			return listCurrency;
		}


		public async Task<StatisticResponse> ExcangeHistory(string Dates, string Currency)
		{
			var statistic = new StatisticResponse();

			using (var httpClient = new HttpClient())
			{
				try
				{
					httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
					var dates = DateSpliter(Dates);
					var currencies = CurrencySpliter(Currency);
					foreach (string date in dates)
					{
						var rawObject = await GetExchangeRateModel(date, httpClient, currencies);

						foreach (var rate in rawObject.Rates)
						{
							var rateVal = rate.Value;
							if (rateVal < statistic.MinimumRate)
							{
								statistic.MinimumRate = rateVal;
								statistic.MiminumRateDate = rawObject.Date;
							}

							if (rateVal > statistic.MaximuRate)
							{
								statistic.MaximuRate = rateVal;
								statistic.MaximumRateDate = rawObject.Date;
							}
							statistic.RateSum += rateVal;
							statistic.RateCount++;
						}
					}
				}
				catch { 
				
				}
			}
			return statistic;
		}

		public async Task<ExcangeRateModelResponse> GetExchangeRateModel(string date, HttpClient httpClient, List<string> currencies)
		{
			string openApiRequest = $"{httpClient.BaseAddress}{date}{_configuration["BaseCurrency"]}{currencies[0].ToUpper()}{_configuration["TargetCurrency"]}{currencies[1].ToUpper()}";
			var response = await httpClient.GetAsync(openApiRequest);
			response.EnsureSuccessStatusCode();
			var stringResult = await response.Content.ReadAsStringAsync();

			var rawObject = JsonConvert.DeserializeObject<ExcangeRateModelResponse>(stringResult);

			return rawObject;
		}
	}
}
