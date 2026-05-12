using UnityEditor;
using UnityEngine;

namespace CityRuinBackgroundsPixelArt
{
    [CustomEditor(typeof(ParallaxEffect))]
    public class ParallaxEffectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ParallaxEffect parallaxEffect = (ParallaxEffect)target;

            EditorGUILayout.HelpBox("To ensure proper positioning of the layers, it is not recommanded to change the default values of this script.", MessageType.Info);

            parallaxEffect.independantSpeed =
                EditorGUILayout.Slider(new GUIContent("Independant Speed", "The layer will move independently to the left if the value is less than 0, and to the right if the value is greater than 0."), parallaxEffect.independantSpeed, -1.0f, 1.0f);

            if (GUI.changed)
                EditorUtility.SetDirty(parallaxEffect);
        }
    }
}