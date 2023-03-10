using CBH.Analytics;
using CBH.Core.Configs;
using UnityEngine.InputSystem;

namespace CBH.Core.Entity.Input
{
    public class RocketInput : IInput, InputSchema.IPlayerActions
    {
        private InputSchema _inputSchema;
        private InputData _inputData;

        private bool _lastBoostState;
        private bool _lastRotationLeftState;
        private bool _lastRotationRightState;
        
        private bool _isBoostUpdated;
        private bool _isRotationUpdated = true;

        public bool EnabledBoost { get; private set; }
        public float RotationDirection { get; private set; }

        public RocketInput(InputSchema inputSchema, InputData inputData)
        {
            _inputSchema = inputSchema;
            _inputData = inputData;
        }
        
        public void Init()
        {
            _inputSchema.Player.SetCallbacks(this);
            _inputSchema.Enable();
        }

        public void Update(float deltaTime)
        {
            EnabledBoost = _isBoostUpdated;

            if (EnabledBoost)
            {
                _inputData.timeBoostPressed += deltaTime;

                if (!_lastBoostState)
                    _inputData.countBoostPressed++;
            }

            _lastBoostState = EnabledBoost;

            if (_isRotationUpdated)
            {
                RotationDirection = _inputSchema.Player.Rotation.ReadValue<float>() * deltaTime;
                if (RotationDirection > 0)
                    _inputData.timeRotationRightPressed += deltaTime;
                else
                    _inputData.timeRotationLeftPressed += deltaTime;

                if (RotationDirection > 0)
                {
                    _inputData.timeRotationRightPressed += deltaTime;
                    if (!_lastRotationRightState)
                        _inputData.countRotationLeftPressed++;
                    
                    _lastRotationRightState = true;
                    _lastRotationLeftState = false;
                }
                else
                {
                    _inputData.timeRotationLeftPressed += deltaTime;
                    if (!_lastRotationLeftState)
                        _inputData.countRotationLeftPressed++;
                    
                    _lastRotationLeftState = true;
                    _lastRotationRightState = false;
                }
            }
            else
            {
                RotationDirection = 0;
                _lastRotationLeftState = false;
                _lastRotationRightState = false;
            }
        }

        public void ResetInput()
        {
            EnabledBoost = false;
            RotationDirection = 0f;

            _isRotationUpdated = false;
            _isBoostUpdated = false;

            _lastBoostState = false;
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