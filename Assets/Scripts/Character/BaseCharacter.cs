using Mirror;
using TheBitCave.MultiplayerRoguelite.Abilities;
using TheBitCave.MultiplayerRoguelite.Prototype;
using UnityEngine;
using TheBitCave.MultiplayerRoguelite.Utils;

namespace TheBitCave.MultiplayerRoguelite
{
    [RequireComponent(typeof(CharacterController))]
    public partial class BaseCharacter : NetworkBehaviour
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

        protected virtual void Awake()
        {
            inputActions = InputManager.Instance.Actions;
            characterController = GetComponent<CharacterController>();
            abilityAttack = GetComponent<AbilityAttack>();
            networkAnimator = GetComponent<NetworkAnimator>();
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
    }
}
