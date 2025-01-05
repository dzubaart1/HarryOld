using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core.LocalManagers.Interfaces
{
    public abstract class BaseLocalManager : MonoBehaviour
    {
        [CanBeNull] 
        public abstract Player GetPlayer();
        public abstract void TeleportGrabInteractableToPlayer(GrabInteractable grabInteractable);
    }
}