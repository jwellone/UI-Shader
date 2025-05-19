using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class GlowPreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Glow");
        }
    }
}