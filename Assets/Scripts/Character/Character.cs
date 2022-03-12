using TheBitCave.MultiplayerRoguelite.Interfaces;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    /// <summary>
    /// An extension of the base character that can have a type and can be skinned
    /// </summary>
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
        
        public virtual CharacterType Type => type;
        public virtual string TypeStringified => C.GetStringifiedCharacter(type);
        
        #endregion
    }
}
