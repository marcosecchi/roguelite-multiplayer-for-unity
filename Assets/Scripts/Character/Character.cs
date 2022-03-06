using System.Collections;
using System.Collections.Generic;
using TheBitCave.MultiplayerRoguelite.Interfaces;
using TheBitCave.MultiplayerRoguelite.Utils;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    public class Character : BaseCharacter, ICharacterTypeable
    {
        [Header("Character Type")]
        [SerializeField] protected CharacterType type;
        
        protected CharacterSkin skin;

        protected override void Awake()
        {
            base.Awake();
            skin = GetComponent<CharacterSkin>();
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            type = CharacterUtils.GetRandomCharacterType();
            if (skin != null)
            {
                skin.Init(type);
            }
        }

        #region ITypeable implementation 
        
        public virtual CharacterType Type => type;
        
        #endregion
    }
}
