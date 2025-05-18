using System;
using UnityEditor;

#nullable enable

namespace jwelloneEditor
{
    public interface IPreviewGUI : IDisposable
    {
        string displayName { get; }
        void OnGUI(EditorWindow parent);
        void Save(string path);
    }
}