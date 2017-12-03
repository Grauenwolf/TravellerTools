//using Newtonsoft.Json;
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Immutable;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace Grauenwolf.TravellerTools.Names
//{
//    public class RandomuserNameService : INameService
//    {
//        readonly ConcurrentQueue<RandomPerson> m_Users = new ConcurrentQueue<RandomPerson>();
//        readonly HttpClient m_HttpClient = new HttpClient();
//        static DateTime? m_LastError;

//        public async Task<RandomPerson> CreateRandomPersonAsync(Dice random)
//        {
//            RandomPerson result = null;

//            m_Users.TryDequeue(out result);

//            if (result == null)
//            {
//                if (m_LastError?.AddMinutes(5) > DateTime.Now)
//                {
//                    result = new RandomPerson("Error", "Error", "?");
//                }
//                else
//                {
//                    try
//                    {

//                        var rawString = await m_HttpClient.GetStringAsync(new Uri("http://api.randomuser.me/?results=100")).ConfigureAwait(false);
//                        var collection = JsonConvert.DeserializeObject<UserRoot>(rawString);

//                        result = new RandomPerson(collection.results[0]);
//                        for (var i = 1; i < collection.results.Length; i++)
//                            m_Users.Enqueue(new RandomPerson(collection.results[i]));

//                        m_LastError = null;
//                    }
//                    catch
//                    {
//                        result = new RandomPerson("Error", "Error", "?");
//                        m_LastError = DateTime.Now;

//                    }
//                }
//            }
//            return result;
//        }
//    }
//}