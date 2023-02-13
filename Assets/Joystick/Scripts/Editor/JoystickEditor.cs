#if UNITY_EDITOR
using ElectrumGames.Joystick.Base;
using UnityEngine;
using UnityEditor;

namespace ElectrumGames.Joystick.Editors
{
    [CustomEditor(typeof(JoystickBase), true)]
    public class JoystickEditor : Editor
    {
        private SerializedProperty handleRange;
        private SerializedProperty deadZone;
        private SerializedProperty axisOptions;
        private SerializedProperty snapX;
        private SerializedProperty snapY;
#if ENABLE_INPUT_SYSTEM
        private SerializedProperty controlPath;
#endif
        protected SerializedProperty background;
        private SerializedProperty handle;
        private SerializedProperty canvas;

        protected Vector2 center = new Vector2(0.5f, 0.5f);

        protected virtual void OnEnable()
        {
            handleRange = serializedObject.FindProperty("handleRange");
            deadZone = serializedObject.FindProperty("deadZone");
            axisOptions = serializedObject.FindProperty("axisOptions");
            snapX = serializedObject.FindProperty("snapX");
            snapY = serializedObject.FindProperty("snapY");
#if ENABLE_INPUT_SYSTEM
            controlPath = serializedObject.FindProperty("controlPath");
#endif
            background = serializedObject.FindProperty("background");
            handle = serializedObject.FindProperty("handle");
            canvas = serializedObject.FindProperty("canvas");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawValues();
            EditorGUILayout.Space();
            DrawComponents();

            serializedObject.ApplyModifiedProperties();

            if (handle == null) 
                return;
            
            var handleRect = (RectTransform) handle.objectReferenceValue;
            handleRect.anchorMax = center;
            handleRect.anchorMin = center;
            handleRect.pivot = center;
            handleRect.anchoredPosition = Vector2.zero;
        }

        protected virtual void DrawValues()
        {
            EditorGUILayout.PropertyField(handleRange,
                new GUIContent("Handle Range",
                    "The distance the visual handle can move from the center of the joystick."));
            EditorGUILayout.PropertyField(deadZone,
                new GUIContent("Dead Zone", "The distance away from the center input has to be before registering."));
            EditorGUILayout.PropertyField(axisOptions, new GUIContent("Axis Options", "Which axes the joystick uses."));
            EditorGUILayout.PropertyField(snapX,
                new GUIContent("Snap X", "Snap the horizontal input to a whole value."));
            EditorGUILayout.PropertyField(snapY, new GUIContent("Snap Y", "Snap the vertical input to a whole value."));
#if ENABLE_INPUT_SYSTEM
            EditorGUILayout.PropertyField(controlPath, new GUIContent("Control Path","Path at controls."));
#endif
        }

        protected virtual void DrawComponents()
        {
            EditorGUILayout.ObjectField(background,
                new GUIContent("Background", "The background's RectTransform component."));
            EditorGUILayout.ObjectField(handle, new GUIContent("Handle", "The handle's RectTransform component."));
            EditorGUILayout.ObjectField(canvas, new GUIContent("Canvas", "Canvas, which contains joystick"));
        }
    }
}
#endif