using UnityEngine;

namespace TheBitCave.BattleRoyale
{
    public partial class C
    {
        // These constants have the same name as the corresponding Addressables labels
        public const string CHARACTER_NONE = "none";
        public const string CHARACTER_RANGER = "ranger";
        public const string CHARACTER_MAGE = "mage";
        public const string CHARACTER_THIEF = "thief";
        public const string CHARACTER_WARRIOR = "warrior";

        public const string ALIGNMENT_GOOD = "good";
        public const string ALIGNMENT_EVIL = "evil";
        public const string ALIGNMENT_NEUTRAL = "neutral";

        public static readonly string[] characterTypes =
        {
            CHARACTER_RANGER,
            CHARACTER_MAGE,
            CHARACTER_THIEF,
            CHARACTER_WARRIOR
        };

        public static readonly string[] alignmentTypes =
        {
//            ALIGNMENT_GOOD,
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

    }
}