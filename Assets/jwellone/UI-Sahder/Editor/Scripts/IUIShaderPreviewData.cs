using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public interface IUIShaderPreviewData
    {
        string displayName { get; }
        Shader? shader { get; }
    }
}