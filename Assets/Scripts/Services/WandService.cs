using System.Threading.Tasks;
using UnityEngine;

namespace HarryPoter.Core
{
    public class WandService : IService
    {
        public WandConfiguration Configuration { get; private set; }
        
        public WandService(WandConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        public void Destroy()
        {
        }

        public void SpawnWand(Vector3 pos)
        {
            Object.Instantiate(Configuration.Wand, pos, Quaternion.identity);
        }
    }
}