using ElectrumGames.Joystick.Base;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ElectrumGames.Joysticks
{
    public class VariableJoystick : JoystickBase
    {

        [SerializeField] private float moveThreshold = 1;
        [SerializeField] private JoystickType joystickType = JoystickType.Fixed;

        public float MoveThreshold
        {
            get => moveThreshold;
            set => moveThreshold = Mathf.Abs(value);
        }
        
        private Vector2 _fixedPosition = Vector2.zero;

        protected override void Start()
        {
            base.Start();
            _fixedPosition = background.anchoredPosition;
            SetMode(joystickType);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (joystickType != JoystickType.Fixed)
            {
                background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
                background.gameObject.SetActive(true);
            }

            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (joystickType != JoystickType.Fixed)
                background.gameObject.SetActive(false);

            base.OnPointerUp(eventData);
        }
        
        private void SetMode(JoystickType joystickType)
        {
            this.joystickType = joystickType;
            if (joystickType == JoystickType.Fixed)
            {
                background.anchoredPosition = _fixedPosition;
                background.gameObject.SetActive(true);
            }
            else
                background.gameObject.SetActive(false);
        }

        protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        {
            if (joystickType == JoystickType.Dynamic && magnitude > moveThreshold)
            {
                var difference = normalised * (magnitude - moveThreshold) * radius;
                background.anchoredPosition += difference;
            }

            base.HandleInput(magnitude, normalised, radius, cam);
        }
    }

    public enum JoystickType
    {
        Fixed,
        Floating,
        Dynamic
    }
}