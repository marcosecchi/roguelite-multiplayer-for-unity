using Mirror;
using TheBitCave.BattleRoyale.Data;
using UnityEngine;

namespace TheBitCave.BattleRoyale.Prototype
{
    [AddComponentMenu("")]
    public class ProtoFloater : MonoBehaviour
    {
        [SerializeField] private CurveSO animationCurve;

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
