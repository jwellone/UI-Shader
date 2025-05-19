using UnityEngine;
using UnityEditor;
using jwellone;

#nullable enable

namespace jwelloneEditor
{
    public sealed class HeightMapToNormalMapPreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/HeightMapToNormalMap");
        }
    }
}