using System;
using System.Threading.Tasks;
using WillowTree.NameGame.Core.Models;

namespace WillowTree.NameGame.Core.Services
{
    public class NameGameService
    {
		private static readonly string DataUrl = "https://www.willowtreeapps.com/api/v1.0/profiles";

        public async Task<Profile[]> GetProfiles()
        {
            return null;
        }
    }
}
