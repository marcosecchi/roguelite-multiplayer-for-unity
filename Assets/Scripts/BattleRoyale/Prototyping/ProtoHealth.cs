using System.Collections;
using Mirror;
using TheBitCave.BattleRoyale.Abilities;
using TheBitCave.BattleRoyale.Data;
using TMPro;
using UnityEngine;

namespace TheBitCave.BattleRoyale.Prototype
{
    public class ProtoHealth : Health
    {
        public CurveSO animCurve;
        public TextMeshProUGUI label;
        
        [Server]
        public override void Damage(float amount, uint provoker)
        {
            Debug.Log("DamagedBy " + provoker );
            base.Damage(amount, provoker);
            if (animCurve != null)
            {
                StartCoroutine(nameof(ChangeScale));
            }
        }

        protected virtual IEnumerator ChangeScale()
        {
            var count = 0f;
            var startScale = transform.localScale;
            while (count <= 1)
            {
                count += .05f;
                transform.localScale = startScale * animCurve.Curve.Evaluate(count);
                yield return new WaitForSeconds(0.005f);
            }
        }
        
        protected override void HealthChanged(float _, float newValue)
        {
            label.text = newValue.ToString();
        }

    }
}
