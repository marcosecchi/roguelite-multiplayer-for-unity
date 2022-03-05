using System.Collections;
using System.Collections.Generic;
using TheBitCave.MultiplayerRoguelite.Interfaces;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    public class Character : BaseCharacter, ICharacterTypeable
    {
        [Header("Character Type")]
        [SerializeField] protected CharacterType type;
        
        public virtual CharacterType Type => type;
    }
}
