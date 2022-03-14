using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    public partial class C
    {
        // These constants have the same name as the corresponding Addressables labels
        public const string CHARACTER_NONE = "none";
        public const string CHARACTER_MINION = "minion";
        public const string CHARACTER_RANGER = "ranger";
        public const string CHARACTER_MAGE = "mage";
        public const string CHARACTER_THIEF = "thief";
        public const string CHARACTER_WARRIOR = "warrior";

        public static readonly string[] characterTypes =
        {
            CHARACTER_MINION,
            CHARACTER_RANGER,
            CHARACTER_MAGE,
            CHARACTER_THIEF,
            CHARACTER_WARRIOR
        };

        public static string GetCharacterLabel(CharacterType type)
        {
            return type switch
            {
                CharacterType.Minion => CHARACTER_MINION,
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