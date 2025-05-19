using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class RadialBlurPreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/RadialBlur");
        }
    }
}
