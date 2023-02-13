#if UNITY_EDITOR
using ElectrumGames.Joysticks;
using UnityEngine;
using UnityEditor;

namespace ElectrumGames.Joystick.Editors
{
    [CustomEditor(typeof(FloatingJoystick))]
    public class FloatingJoystickEditor : JoystickEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (background == null) 
                return;
            
            var backgroundRect = (RectTransform) background.objectReferenceValue;
            backgroundRect.anchorMax = Vector2.zero;
            backgroundRect.anchorMin = Vector2.zero;
            backgroundRect.pivot = center;
        }
    }
}
#endif