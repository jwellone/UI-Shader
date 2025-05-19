using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class CirclePreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Circle");
        }
    }
}