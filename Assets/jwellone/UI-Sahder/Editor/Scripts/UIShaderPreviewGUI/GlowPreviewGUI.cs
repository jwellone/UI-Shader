using UnityEngine;
using UnityEditor;
using jwellone;

#nullable enable

namespace jwelloneEditor
{
    public sealed class GlowPreviewGUI : UIShaderPreviewGUI
    {
        Color _emissionColor;

        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Glow");
        }

        public override void OnGUI(EditorWindow parent)
        {
            base.OnGUI(parent);
            _emissionColor = EditorGUILayout.ColorField("Emission Color", _emissionColor);
        }

        protected override void UpdateMaterialProperty()
        {
            _material!.SetColor(UIShaderProperty.emissionColor, _emissionColor);
        }
    }
}