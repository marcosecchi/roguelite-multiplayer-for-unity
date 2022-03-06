using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Utils
{
    public static class CharacterUtils
    {
        public static string GetCharacterLabel(CharacterType type)
        {
            return type switch
            {
                CharacterType.Archer => C.ADDRESSABLE_LABEL_ARCHER,
                CharacterType.Mage => C.ADDRESSABLE_LABEL_MAGE,
                CharacterType.Warrior => C.ADDRESSABLE_LABEL_WARRIOR,
                _ => C.ADDRESSABLE_LABEL_MINION
            };
        }

        public static CharacterType GetRandomCharacterType()
        {
            // TODO: complete randomization
            return CharacterType.Archer;
        }
    }
}
