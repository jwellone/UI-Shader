using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class EdgeFilterPreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/EdgeFilter");
        }
    }
}