using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TheBitCave.MultiplayerRoguelite.Data
{
    [CreateAssetMenu(fileName = "CurveData", menuName = "Roguelite/Data/Curve")]
    public class CurveSO : ScriptableObject
    {
        [FormerlySerializedAs("Curve")] [SerializeField]
        private AnimationCurve curve;

        public AnimationCurve Curve => curve;
    }
}
