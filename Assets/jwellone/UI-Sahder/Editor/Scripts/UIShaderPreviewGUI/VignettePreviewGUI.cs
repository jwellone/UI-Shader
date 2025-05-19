using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class VignettePreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Vignette");
        }
    }
}
