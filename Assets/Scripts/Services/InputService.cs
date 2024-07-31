using UnityEngine;

namespace HarryPoter.Core
{
    public class InputService : IService
    {
        public InputConfiguration Configuration { get; private set; }
        public Player Player { get; private set; }
        
        public InputService(InputConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void Initialize()
        {
            SpawnPlayer();
        }

        public void Destroy()
        {
        }

        private void SpawnPlayer()
        {
            Player = Object.Instantiate(Configuration.Player, Configuration.StartPos, Quaternion.identity);
        }
    }
}