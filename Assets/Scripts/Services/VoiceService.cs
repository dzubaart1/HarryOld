using System.Threading.Tasks;

namespace HarryPoter.Core
{
    public class VoiceService : IService
    {
        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        public void Destroy()
        {
        }
    }
}