using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class AddtivePreviewGUI : UIShaderPreviewGUI
    {
        protected override Shader? GetShader()
        {
            var s = Shader.Find("UI/Addtive");
            Debug.Log($"### {s}");
            return s;
        }
    }
}