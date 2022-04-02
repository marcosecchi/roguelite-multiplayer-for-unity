using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace TheBitCave.BattleRoyale.Utils
{
    public class CameraManager : Singleton<CameraManager>
    {
        [SerializeField] private CinemachineVirtualCamera playerCamera;

        [SerializeField] private FollowTarget followPlayer;

        public override void Awake()
        {
            base.Awake();
            followPlayer.TargetLost += OnTargetLost;
        }

        private void OnTargetLost()
        {
            // TODO: Handle target lost
        }

        public void SetLocalPlayer(Transform value)
        {
            followPlayer.SetTarget(value);
        }
    }
}
