using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class AddtivePreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Addtive");
        }
    }
}