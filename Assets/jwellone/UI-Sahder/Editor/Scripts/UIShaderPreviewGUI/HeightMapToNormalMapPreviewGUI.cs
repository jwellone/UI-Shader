using UnityEngine;
using UnityEditor;
using jwellone;

#nullable enable

namespace jwelloneEditor
{
    public sealed class HeightMapToNormalMapPreviewGUI : UIShaderPreviewGUI
    {
        float _scaleFactor = 1f;
        float _parallaxScale = 5f;

        protected override Shader? GetShader()
        {
            return Shader.Find("UI/HeightMapToNormalMap");
        }

        public override void OnGUI(EditorWindow parent)
        {
            base.OnGUI(parent);

            _scaleFactor = EditorGUILayout.Slider("Scale Factor", _scaleFactor, 0f, 1f);
            _parallaxScale = EditorGUILayout.Slider("Parallax Scale", _parallaxScale, 0f, 10f);
        }

        protected override void UpdateMaterialProperty()
        {
            _material.SetFloat(UIShaderProperty.scaleFactor, _scaleFactor);
            _material.SetFloat(UIShaderProperty.parallaxScale, _parallaxScale);
        }
    }
}