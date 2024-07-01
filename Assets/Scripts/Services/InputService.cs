using System.Threading.Tasks;
using UnityEngine;

namespace HarryPoter.Core
{
    public class InputService : IService
    {
        public InputConfiguration Configuration { get; private set; }
        
        public InputService(InputConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public Task Initialize()
        {
            Object.Instantiate(Configuration.Player, Configuration.StartPos, Quaternion.identity);
            return Task.CompletedTask;
        }

        public void Destroy()
        {
        }
    }
}