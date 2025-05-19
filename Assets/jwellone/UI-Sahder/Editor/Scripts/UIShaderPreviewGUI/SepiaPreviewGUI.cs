using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class SepiaPreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Sepia");
        }
    }
}