using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Names
{
	public static class NameService
	{
		readonly static ConcurrentQueue<RandomPerson> s_Users = new ConcurrentQueue<RandomPerson>();

		public static async Task<RandomPerson> CreateRandomPersonAsync()
		{
			RandomPerson result = null;

			s_Users.TryDequeue(out result);

			if (result == null)
			{
				try
				{
					using (var client = new WebClient())
					{
						var rawString = await client.DownloadStringTaskAsync(new Uri("http://api.randomuser.me/?results=100")).ConfigureAwait(false);
						var collection = JsonConvert.DeserializeObject<UserRoot>(rawString);

						result = new RandomPerson(collection.results[0]);
						for (var i = 1; i < collection.results.Length; i++)
							s_Users.Enqueue(new RandomPerson(collection.results[i]));
					}
				}
				catch
				{
					result = new RandomPerson("Error", "Error", "?");
				}
			}
			return result;
		}
	}
}