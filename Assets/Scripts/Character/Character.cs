using Mirror;
using TheBitCave.MultiplayerRoguelite.Abilities;
using TheBitCave.MultiplayerRoguelite.Interfaces;
using TheBitCave.MultiplayerRoguelite.Prototype;
using TheBitCave.MultiplayerRoguelite.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

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
        protected AbilityRangedAttack abilityRangedAttack;

        [SyncVar]
        protected bool isInCloseCombatRange;
        
        #region Mirror Callbacks

        public override void OnStartServer()
        {
            var health = GetComponent<Health>();
            if (health != null) health.StartingHitPoints = stats.StartingHitPoints;
        }
        
        public override void OnStartClient()
        {
            if (!isLocalPlayer) return;
            inputActions.Player.Attack.started += OnAttackStarted;
        }
        
        public override void OnStopClient()
        {
            if (!isLocalPlayer) return;
            inputActions.Player.Attack.started -= OnAttackStarted;
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            inputActions.Player.Enable();
        }

        #endregion

        #region Unity Callbacks

        protected virtual void Awake()
        {
            inputActions = InputManager.Instance.Actions;
            characterController = GetComponent<CharacterController>();
            abilityRangedAttack = GetComponent<AbilityRangedAttack>();
        }

        protected virtual void OnEnable()
        {
            if (!isLocalPlayer) return;
            inputActions.Player.Enable();
        }

        protected virtual void OnDisable()
        {
            if (!isLocalPlayer) return;
            inputActions.Player.Disable();
        }
        
        /// <summary>
        /// Handles local player input
        /// </summary>
        protected virtual void FixedUpdate()
        {
            // Handle local player input
            if (isLocalPlayer)
            {
                // Player Input
                var input = inputActions.Player.Move.ReadValue<Vector2>();
                var isRunning = inputActions.Player.Run.inProgress;
                var speed = isRunning ? Stats.RunSpeed : Stats.WalkSpeed;
            
                // Character Movement
                var t = transform;
                var move = Mathf.Clamp01(input.y) * speed * t.forward;
                characterController.SimpleMove(move);

                var rotation = input.x * Stats.RotationSpeed * t.up;
                transform.Rotate(rotation);
            
                // Animator update
                if (animator == null) return;
                animator.SetFloat(C.ANIMATOR_PARAMETER_SPEED, characterController.velocity.magnitude);
            }
            // Handle server operations 
            else if (isServer)
            {
                // TODO: Implement close combat range checking
                isInCloseCombatRange = false;
            }
        }

        #endregion

        #region Attack

        protected virtual void OnAttackStarted(InputAction.CallbackContext obj)
        {
            abilityRangedAttack = GetComponent<AbilityRangedAttack>();

            Debug.Log(abilityRangedAttack);
            if (isInCloseCombatRange)
            {
                // TODO: Implement close combat attack
            }
            else if (abilityRangedAttack != null)
            {
                Debug.Log("Attack Ranged");
                abilityRangedAttack.Attack();
            }
        }
        
        #endregion
        
        #region Getters and Setters

        public CharacterStatsSO Stats => stats;

        #endregion

        #region ITypeable implementation 
        
        public CharacterType Type => stats.Type;
        public virtual string TypeStringified => C.GetStringifiedCharacter(stats.Type);
        
        #endregion
    }
}
