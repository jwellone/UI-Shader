using UnityEngine;
using UnityEditor;
using jwellone;

#nullable enable

namespace jwelloneEditor
{
    public sealed class BinarizationPreviewGUI : UIShaderPreviewGUI
    {
        float _rate = 1f;

        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Binarization");
        }

        public override void OnGUI(EditorWindow parent)
        {
            base.OnGUI(parent);
            _rate = EditorGUILayout.Slider("Rate", _rate, 0f, 1f);
        }

        protected override void UpdateMaterialProperty()
        {
            _material!.SetFloat(UIShaderProperty.rate, _rate);
        }
    }
}