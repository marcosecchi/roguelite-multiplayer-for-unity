using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    public partial class C
    {
        // These constants have the same name as the corresponding Addressables labels
        public const string CHARACTER_NONE = "none";
        public const string CHARACTER_MINION = "minion";
        public const string CHARACTER_ARCHER = "archer";
        public const string CHARACTER_MAGE = "mage";
        public const string CHARACTER_WARRIOR = "warrior";

        public static readonly string[] characterTypes =
        {
            CHARACTER_MINION,
            CHARACTER_ARCHER,
            CHARACTER_MAGE,
            CHARACTER_WARRIOR
        };

        public static string GetStringifiedCharacter(CharacterType type)
        {
            return type switch
            {
                CharacterType.Minion => CHARACTER_MINION,
                CharacterType.Archer => CHARACTER_ARCHER,
                CharacterType.Mage => CHARACTER_MAGE,
                CharacterType.Warrior => CHARACTER_WARRIOR,
                _ => CHARACTER_NONE
            };
        }

        public static string GetRandomCharacter()
        {
            return characterTypes[Random.Range(0, characterTypes.Length)];
        }

    }
}