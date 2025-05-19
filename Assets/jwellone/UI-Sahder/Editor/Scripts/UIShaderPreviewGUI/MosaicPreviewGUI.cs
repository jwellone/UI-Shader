using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class MosaicPreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Mosaic");
        }
    }
}