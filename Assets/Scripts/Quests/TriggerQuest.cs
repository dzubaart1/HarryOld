using HarryPoter.Core;
using Oculus.Interaction.HandGrab;
using UnityEngine;

namespace HarryPoter.Core.Quests
{
    [RequireComponent(typeof(Collider))]
    public class TriggerQuest : Quest
    {
        public string ObjectTag;
        public bool IsEnter;
    }
}