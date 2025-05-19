using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class BinarizationPreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Binarization");
        }
    }
}