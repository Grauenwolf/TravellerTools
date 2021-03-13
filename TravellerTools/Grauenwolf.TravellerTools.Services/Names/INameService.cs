using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Names
{
    public interface INameService
    {
        Task<RandomPerson> CreateRandomPersonAsync(Dice random, bool? isMale = null);
    }
}
