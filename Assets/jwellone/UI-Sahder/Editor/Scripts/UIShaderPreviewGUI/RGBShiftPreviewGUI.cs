using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class RGBShiftPreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/RGBShift");
        }
    }
}