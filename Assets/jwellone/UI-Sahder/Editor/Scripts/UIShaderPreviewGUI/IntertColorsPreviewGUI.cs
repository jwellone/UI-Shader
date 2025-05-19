using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class IntertColorsPreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/IntertColors");
        }
    }
}