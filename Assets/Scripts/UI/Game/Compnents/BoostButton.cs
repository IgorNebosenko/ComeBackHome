using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace CBH.UI.Game
{
    public class BoostButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image buttonImage;
        [InputControl(layout = "Button")] 
        [SerializeField] private string controlPath;

        private static readonly Color32 ButtonPressedColor = new Color32(255, 255, 255, 127);

        private bool _isPressed;

        protected override string controlPathInternal
        {
            get => controlPath;
            set => controlPath = value;
        }

        private void Update()
        {
            SendValueToControl(_isPressed ? 1.0f : 0f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
            buttonImage.color = ButtonPressedColor;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
            buttonImage.color = Color.white;
        }
    }
}