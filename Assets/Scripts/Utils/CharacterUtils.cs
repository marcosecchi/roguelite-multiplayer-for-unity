using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Utils
{
    public static class CharacterUtils
    {
        public static string GetRandomCharacterType()
        {
            return C.characterTypes[Random.Range(0, C.characterTypes.Length)];
        }
    }
}
