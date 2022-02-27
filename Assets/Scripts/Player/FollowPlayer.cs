using System.Collections;
using Mirror;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
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

        private IEnumerator FindTarget()
        {
            while (_target == null)
            {
                yield return new WaitForSeconds(.5f);
                var t = FindObjectOfType<AbstractPlayerController>();
                if(t != null && t.isLocalPlayer) _target = t.transform;
            }
        }
    }
}