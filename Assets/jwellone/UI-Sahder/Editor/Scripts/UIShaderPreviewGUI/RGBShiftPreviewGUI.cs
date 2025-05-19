using UnityEngine;
using UnityEditor;
using jwellone;

#nullable enable

namespace jwelloneEditor
{
    public sealed class RGBShiftPreviewGUI : UIShaderPreviewGUI
    {
        float _amount;

        protected override Shader? GetShader()
        {
            return Shader.Find("UI/RGBShift");
        }

        public override void OnGUI(EditorWindow parent)
        {
            base.OnGUI(parent);
            _amount = EditorGUILayout.Slider("Amount", _amount, 0f, 0.1f);
        }

        protected override void UpdateMaterialProperty()
        {
            _material.SetFloat(UIShaderProperty.amount, _amount);
        }
    }
}