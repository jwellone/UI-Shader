using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class DitheringPreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Dithering");
        }
    }
}