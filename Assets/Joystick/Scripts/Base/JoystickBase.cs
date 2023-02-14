using UnityEngine;
using UnityEngine.EventSystems;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
#endif

namespace ElectrumGames.Joystick.Base
{
    public abstract class JoystickBase : 
#if ENABLE_INPUT_SYSTEM
        OnScreenControl,
#else  
        MonoBehaviour,
#endif
        IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private float handleRange = 1;
        [SerializeField] private float deadZone;
        public AxisOptions axisOptions = AxisOptions.Both;
        public bool snapX;
        public bool snapY;
        #if ENABLE_INPUT_SYSTEM
        [InputControl(layout = "Vector2")] 
        [SerializeField] private string controlPath;
        #endif

        [SerializeField] protected RectTransform background;
        [SerializeField] private RectTransform handle;
        [SerializeField] private Canvas canvas;
        
        private Camera _camera;
        private RectTransform _baseRect;

        private Vector2 _input = Vector2.zero;
        
        public float Horizontal => (snapX) ? SnapFloat(_input.x, AxisOptions.Horizontal) : _input.x;

        public float Vertical => (snapY) ? SnapFloat(_input.y, AxisOptions.Vertical) : _input.y;

        public Vector2 Direction => new Vector2(Horizontal, Vertical);

        public float HandleRange
        {
            get => handleRange;
            set => handleRange = Mathf.Abs(value);
        }

        public float DeadZone
        {
            get => deadZone;
            set => deadZone = Mathf.Abs(value);
        }
        
#if ENABLE_INPUT_SYSTEM
        protected override string controlPathInternal
        {
            get => controlPath;
            set => controlPath = value;
        }
#endif

        protected virtual void Start()
        {
            HandleRange = handleRange;
            DeadZone = deadZone;
            
            _baseRect = transform as RectTransform;
            if (canvas == null)
                canvas = GetComponentInParent<Canvas>();

            var center = new Vector2(0.5f, 0.5f);
            background.pivot = center;
            handle.anchorMin = center;
            handle.anchorMax = center;
            handle.pivot = center;
            handle.anchoredPosition = Vector2.zero;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _camera = null;
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
                _camera = canvas.worldCamera;

            var position = RectTransformUtility.WorldToScreenPoint(_camera, background.position);
            var radius = background.sizeDelta / 2;
            _input = (eventData.position - position) / (radius * canvas.scaleFactor);
            FormatInput();
            HandleInput(_input.magnitude, _input.normalized, radius, _camera);
            handle.anchoredPosition = _input * radius * handleRange;
            
#if ENABLE_INPUT_SYSTEM
            SendValueToControl(handle.anchoredPosition);
#endif
        }

        protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        {
            if (magnitude > deadZone)
            {
                if (magnitude > 1)
                    _input = normalised;
            }
            else
                _input = Vector2.zero;
        }

        private void FormatInput()
        {
            switch (axisOptions)
            {
                case AxisOptions.Horizontal:
                    _input = new Vector2(_input.x, 0f);
                    break;
                case AxisOptions.Vertical:
                    _input = new Vector2(0f, _input.y);
                    break;
            }
        }

        private float SnapFloat(float value, AxisOptions snapAxis)
        {
            if (value == 0)
                return value;

            if (axisOptions == AxisOptions.Both)
            {
                var angle = Vector2.Angle(_input, Vector2.up);
                if (snapAxis == AxisOptions.Horizontal)
                {
                    if (angle < 22.5f || angle > 157.5f)
                        return 0;
                    
                    return (value > 0) ? 1 : -1;
                }
                if (snapAxis == AxisOptions.Vertical)
                {
                    if (angle > 67.5f && angle < 112.5f)
                        return 0;

                    return (value > 0) ? 1 : -1;
                }

                return value;
            }

            if (value > 0)
                return 1;
            if (value < 0)
                return -1;
            
            return 0;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            _input = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
            
#if ENABLE_INPUT_SYSTEM
            SendValueToControl(handle.anchoredPosition);
#endif
        }

        protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
        {
            var localPoint = Vector2.zero;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, screenPosition, _camera, out localPoint))
            {
                var pivotOffset = _baseRect.pivot * _baseRect.sizeDelta;
                return localPoint - (background.anchorMax * _baseRect.sizeDelta) + pivotOffset;
            }

            return Vector2.zero;
        }
    }
}