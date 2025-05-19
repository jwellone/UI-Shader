using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class GrayscalePreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Grayscale");
        }
    }
}