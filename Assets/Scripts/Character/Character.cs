using TheBitCave.MultiplayerRoguelite.Interfaces;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    public class Character : BaseCharacter, ICharacterTypeable
    {
        [SerializeField]
        protected CharacterType type;

        protected CharacterSkin skin;

        protected override void Awake()
        {
            base.Awake();
            skin = GetComponent<CharacterSkin>();
        }
        
        #region ITypeable implementation 
        
        public virtual string Type => C.GetStringifiedCharacter(type);
        
        #endregion
    }
}
