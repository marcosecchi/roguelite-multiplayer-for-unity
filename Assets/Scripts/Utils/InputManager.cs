using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheBitCave.MultiplayerRoguelite.Utils
{
    public class InputManager : PersistentSingleton<InputManager>
    {
        private InputActions _actions;
        
        public override void Awake()
        {
            base.Awake();
            _actions = new InputActions();
        }

        public InputActions Actions => _actions;
    }
}
