using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DatingApp.API.Helpers
{
	public static class Extensions
	{
		//for passing errors to our header to show the clients
		public static void AddApplicationError(this HttpResponse response, string message)
		{
			response.Headers.Add("Application-Error", message);
			response.Headers.Add("Application-Control-Expose-Headers", "Applicaton-Error");
			response.Headers.Add("Application-Control-Allow-Origin", "*");
		}

		public static void AddPagination(this HttpResponse response,
		int currentPage, int itemsPerPage, int totalItems, int totalPages)
		{
			var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
			var camelCaseFormatter = new JsonSerializerSettings();
			camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
			response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
			response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
		}

		public static int CalculateAge(this DateTime thisDateTime)
		{
			var age = DateTime.Today.Year - thisDateTime.Year;
			if (thisDateTime.AddYears(age) > DateTime.Today)
				age--;

			return age;
		}
	}
}