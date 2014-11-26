using System.Threading.Tasks;
using TinderApp.TinderApi;

namespace TinderApp.Models
{
    /// <summary>
    /// Updates the profile biography text
    /// </summary>
    public class BioUpdate
    {
        public string Bio { get; set; }

        public async Task SaveProfile()
        {
            await Client.Post("profile", this);
        }
    }
}