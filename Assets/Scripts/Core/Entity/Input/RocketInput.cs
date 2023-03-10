using CBH.Analytics;
using CBH.Core.Configs;
using UnityEngine.InputSystem;

namespace CBH.Core.Entity.Input
{
    public class RocketInput : IInput, InputSchema.IPlayerActions
    {
        private InputSchema _inputSchema;
        
        private bool _isBoostUpdated;
        private bool _isRotationUpdated = true;

        public bool EnabledBoost { get; private set; }
        public float RotationDirection { get; private set; }

        public RocketInput(InputSchema inputSchema, InputData inputData)
        {
            _inputSchema = inputSchema;
        }
        
        public void Init()
        {
            _inputSchema.Player.SetCallbacks(this);
            _inputSchema.Enable();
        }

        public void Update(float deltaTime)
        {
            EnabledBoost = _isBoostUpdated;

            if (_isRotationUpdated)
                RotationDirection = _inputSchema.Player.Rotation.ReadValue<float>() * deltaTime;
            else
                RotationDirection = 0;
        }

        public void ResetInput()
        {
            EnabledBoost = false;
            RotationDirection = 0f;

            _isRotationUpdated = false;
            _isBoostUpdated = false;
        }

        public void OnVelocity(InputAction.CallbackContext context)
        {
            _isBoostUpdated = context.phase != InputActionPhase.Canceled;
        }

        public void OnRotation(InputAction.CallbackContext context)
        {
            _isRotationUpdated = context.phase != InputActionPhase.Canceled;
        }
    }
}