using UnityEngine;
using UnityEditor;
using jwellone;

#nullable enable

namespace jwelloneEditor
{
    public sealed class PosterizePreviewGUI : UIShaderPreviewGUI
    {
        float _step;

        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Posterize");
        }

        public override void OnGUI(EditorWindow parent)
        {
            base.OnGUI(parent);
            _step = EditorGUILayout.Slider("Step", _step, 1, 32);
        }

        protected override void UpdateMaterialProperty()
        {
            _material.SetFloat(UIShaderProperty.step, _step);
        }
    }
}