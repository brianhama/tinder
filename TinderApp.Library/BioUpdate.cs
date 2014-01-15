using System.Threading.Tasks;
using TinderApp.Lib.API;

namespace TinderApp.Library
{
    public class BioUpdate
    {
        public string Bio { get; set; }

        public async Task SaveProfile()
        {
            await Client.Post("profile", this);
        }
    }
}