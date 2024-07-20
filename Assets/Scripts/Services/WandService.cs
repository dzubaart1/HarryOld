using System.Threading.Tasks;
using UnityEngine;

namespace HarryPoter.Core
{
    public class WandService : IService
    {
        public Wand CurrentWand { get; private set; }
        
        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        public void Destroy()
        {
        }

        public void SetWand(Wand wand)
        {
            if (CurrentWand != null)
            {
                Object.Destroy(CurrentWand.gameObject);
                CurrentWand = null;
            }
            
            CurrentWand = wand;
        }
    }
}