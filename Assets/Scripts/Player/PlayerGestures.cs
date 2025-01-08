using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using UnityEngine;

namespace HarryPoter.Core
{
    public class PlayerGestures : MonoBehaviour
    {
        [SerializeField] private List<ActiveStateGroup> _spawnWandGestures = new List<ActiveStateGroup>();
     
        [Space]
        [SerializeField] private List<ActiveStateGroup> _moveForwardGestures = new List<ActiveStateGroup>();

        [Space]
        [SerializeField] private List<ActiveStateGroup> _recordingGestures = new List<ActiveStateGroup>();
        private void Update()
        {
            UserController userController = UserController.Instance;
            if (userController == null)
            {
                return;
            }

            if (_spawnWandGestures.Any(gesture => gesture.Active))
            {
                userController.SpawnWand();
            }

            if (_moveForwardGestures.Any(gesture => gesture.Active))
            {
                userController.MoveForward();
            }
            else
            {
                userController.StopMoveForward();
            }
            
            if (_recordingGestures.Any(gesture => gesture.Active))
            {
                userController.StartVoiceRecording();
            }
        }
    }
}