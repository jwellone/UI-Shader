using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class FillPreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Fill");
        }
    }
}
