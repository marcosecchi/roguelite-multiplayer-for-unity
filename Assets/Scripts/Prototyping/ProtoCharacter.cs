using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Prototype
{
    [AddComponentMenu("")]
    public class ProtoCharacter : BaseCharacter
    {

        [Header("HUD")]
        [SerializeField]
        private TextMeshProUGUI pointsLabel;

        [SyncVar(hook = nameof(OnSkinColorChange))]
        private Color _skinColor;
        
        [SyncVar(hook = nameof(OnPointsChange))]
        private int _points = -1;
        
        public override void OnStartServer()
        {
            base.OnStartServer();
            _skinColor = Random.ColorHSV();
            _points = 0;
        }

        public void AddPoints(int value)
        {
            if (!isServer) return;
            _points += value;
        }
        
        private void OnSkinColorChange(Color _, Color newValue)
        {
            var smr = GetComponentInChildren<SkinnedMeshRenderer>();
            if (smr == null) return;
            smr.material.color = newValue;
        }

        private void OnPointsChange(int _, int newValue)
        {
            pointsLabel.text = newValue.ToString();
        }
    }
}
