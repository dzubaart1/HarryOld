using System;
using System.Threading.Tasks;

namespace HarryPoter.Core
{
    public class FindingItemsService : IService
    {
        public enum EListItem
        {
            YoYo = 0,
            Amore = 1,
            Ear = 2,
            ChawingDisc = 3,
            Klaxon = 4,
            DarknessBox = 5,
            WandFake = 6,
        }

        public event Action EndListEvent;
        public event Action<EListItem> CheckInListItemEvent; 
        
        private int _items;
        
        public Task Initialize()
        {
            _items = (1 << 7) - 1;
            return Task.CompletedTask;
        }

        public void Destroy()
        {
        }

        public void CheckIn(EListItem listItem)
        {
            _items ^= 1 << (int)listItem;
            CheckInListItemEvent?.Invoke(listItem);
            
            if (_items == 0)
            {
                EndListEvent?.Invoke();
            }
        }
    }
}