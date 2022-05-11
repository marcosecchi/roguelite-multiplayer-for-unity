using UnityEngine;

namespace TheBitCave.BattleRoyale
{
    public partial class C
    {
        public static readonly string[] characterTypes =
        {
            CHARACTER_RANGER,
            CHARACTER_MAGE,
            CHARACTER_THIEF,
            CHARACTER_WARRIOR
        };

        public static readonly string[] alignmentTypes =
        {
            ALIGNMENT_GOOD,
            ALIGNMENT_EVIL
        };

        public static string GetCharacterAlignmentLabel(CharacterAlignment alignment)
        {
            return alignment switch
            {
                CharacterAlignment.Evil => ALIGNMENT_EVIL,
                CharacterAlignment.Good => ALIGNMENT_GOOD,
                _ => ALIGNMENT_NEUTRAL
            };
        }

        public static string GetCharacterTypeLabel(CharacterType type)
        {
            return type switch
            {
                CharacterType.Ranger => CHARACTER_RANGER,
                CharacterType.Mage => CHARACTER_MAGE,
                CharacterType.Thief => CHARACTER_THIEF,
                CharacterType.Warrior => CHARACTER_WARRIOR,
                _ => CHARACTER_NONE
            };
        }

        public static string GetRandomCharacterLabel()
        {
            return characterTypes[Random.Range(0, characterTypes.Length)];
        }

        public static string GetRandomAlignment()
        {
            return alignmentTypes[Random.Range(0, alignmentTypes.Length)];
        }

    }
}