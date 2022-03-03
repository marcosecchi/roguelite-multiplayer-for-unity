using System.Collections;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    /// <summary>
    /// Behaviour utility used by Cinemachine to lock on the target local player
    /// </summary>
    public class FollowPlayer : MonoBehaviour
    {
        private Transform _target;
        
        private void Update()
        {
            if (_target != null)
            {
                transform.position = Vector3.Lerp(transform.position, _target.position, .3f);
            }
            else
            {
                StartCoroutine(FindTarget());
            }
        }

        /// <summary>
        /// Keep on looking for the local player if it is null (i.e.: not yet spawned)
        /// </summary>
        /// <returns></returns>
        private IEnumerator FindTarget()
        {
            while (_target == null)
            {
                yield return new WaitForSeconds(.5f);
                var t = FindObjectOfType<BaseCharacter>();
                if(t != null && t.isLocalPlayer) _target = t.transform;
            }
        }
    }
}