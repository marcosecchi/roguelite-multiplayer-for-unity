using Mirror;
using TheBitCave.MultiplayerRoguelite.Abilities;
using TheBitCave.MultiplayerRoguelite.Interfaces;
using TheBitCave.MultiplayerRoguelite.Prototype;
using TheBitCave.MultiplayerRoguelite.Utils;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite
{
    /// <summary>
    /// The main controller for every character: handles movement, attacks and other features.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    [AddComponentMenu(menuName: "Roguelite/Character")]
    public partial class Character : NetworkBehaviour, ICharacterTypeable
    {
        [Header("Stats")]
        [SerializeField]
        protected CharacterStatsSO stats;

        [Header("Components")]
        [SerializeField]
        protected Animator animator;
        
        protected InputActions inputActions;
        protected CharacterController characterController;
        protected AbilityAttack abilityAttack;
        protected NetworkAnimator networkAnimator;
        
        protected CharacterSkin skin;

        protected virtual void Awake()
        {
            inputActions = InputManager.Instance.Actions;
            characterController = GetComponent<CharacterController>();
            abilityAttack = GetComponent<AbilityAttack>();
            networkAnimator = GetComponent<NetworkAnimator>();
            skin = GetComponent<CharacterSkin>();
        }

        public override void OnStartServer()
        {
            var health = GetComponent<Health>();
            if (health != null) health.StartingHitPoints = stats.StartingHitPoints;
        }
        
        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            inputActions.Player.Enable();
        }
        
        private void OnEnable()
        {
            if (!isLocalPlayer) return;
            inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            if (!isLocalPlayer) return;
            inputActions.Player.Disable();
        }

        #region Getters and Setters

        public Animator Animator => animator;
        public NetworkAnimator NetworkAnimator => networkAnimator;
        public CharacterStatsSO Stats => stats;

        #endregion

        #region ITypeable implementation 
        
        public virtual CharacterType Type => stats.Type;
        public virtual string TypeStringified => C.GetStringifiedCharacter(stats.Type);
        
        #endregion
    }
}
