using System.Collections;
using System.Collections.Generic;
using Mirror;
using TheBitCave.MultiplayerRoguelite.Interfaces;
using TheBitCave.MultiplayerRoguelite.Utils;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    public class Character : BaseCharacter, ICharacterTypeable
    {
        [SerializeField]
        protected string type = C.CHARACTER_MAGE;

        protected CharacterSkin skin;

        protected override void Awake()
        {
            base.Awake();
            skin = GetComponent<CharacterSkin>();
        }
        
        #region ITypeable implementation 
        
        public virtual string Type => type.ToLower();
        
        #endregion
    }
}
