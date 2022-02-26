using Mirror;
using UnityEngine;
using TheBitCave.MultiplayerRoguelite.Data;

namespace TheBitCave.MultiplayerRoguelite.Prototype
{
    [AddComponentMenu("")]
    public class PlayerCounterNetworkTransformChild : NetworkTransformChild
    {
        [SerializeField] private CurveScriptableObject animationCurve;

        private float startY;
        
        public override void OnStartClient()
        {
            if (isLocalPlayer)
            {
                startY = target.transform.localPosition.y;
            }
            else
            {
                target.gameObject.SetActive(false);
                enabled = false;
            }
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer) return;
            var localPosition = target.transform.localPosition;
            var pos = new Vector3(localPosition.x, startY + animationCurve.Curve.Evaluate(Time.time) * .3f, localPosition.z);
            target.localPosition = pos;
        }
    }
}
