using Mirror;
using UnityEngine;
using TheBitCave.MultiplayerRoguelite.Data;

namespace TheBitCave.MultiplayerRoguelite.Prototype
{
    [AddComponentMenu("")]
    public class ProtoFloater : MonoBehaviour
    {
        [SerializeField] private CurveScriptableObject animationCurve;

        private float _startY;
        
        public void Start()
        {
            _startY = transform.localPosition.y;
        }

        private void FixedUpdate()
        {
            var localPos = transform.localPosition;
            var pos = new Vector3(localPos.x, _startY + animationCurve.Curve.Evaluate(Time.time) * .3f, localPos.z);
            transform.localPosition = pos;
        }
    }
}
