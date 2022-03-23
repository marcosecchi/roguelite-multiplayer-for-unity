using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheBitCave.BattleRoyale
{
    [AddComponentMenu("")]
    public class CharacterSkinBodyElements : MonoBehaviour
    {
        [SerializeField] protected GameObject body;
        [SerializeField] protected GameObject armLeft;
        [SerializeField] protected GameObject armRight;
        
        public virtual GameObject Body => body;
        public virtual GameObject ArmLeft => armLeft;
        public virtual GameObject ArmRight => armRight;
    }
}
