using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace TheBitCave.BattleRoyale
{
    /// <summary>
    /// Behaviour utility used by Cinemachine to lock on the target local player
    /// </summary>
    public class FollowTarget : MonoBehaviour
    {
        private Transform _target;
        private Transform _previous;

        [Range(0.01f, 1f)]
        [SerializeField]
        private float followSpeed = .3f;
        
        public delegate void TargetEvent();

        public event TargetEvent TargetLost;
        
        /// <summary>
        /// Keep following the local player, if any
        /// </summary>
        private void Update()
        {
            if (_target != null)
            {
                transform.position = Vector3.Lerp(transform.position, _target.position, followSpeed);
                _previous = _target;
            }
            else if (_previous != _target)
            {
                TargetLost?.Invoke();
            }
        }
        
        /// <summary>
        /// Assigns the target to follow
        /// </summary>
        /// <param name="target"></param>
        public void SetTarget(Transform target)
        {
            _target = target;
        }

    }
}