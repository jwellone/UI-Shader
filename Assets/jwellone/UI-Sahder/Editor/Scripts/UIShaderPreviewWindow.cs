using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

#nullable enable

namespace jwelloneEditor
{
    public class UIShaderPreviewWindow : EditorWindow
    {
        private string[] _popupDisplayNames = null!;
        private int _popupSelectIndex;
        private readonly List<IPreviewGUI> _previewGUIList = new();

        IPreviewGUI? previewGui => _previewGUIList.Count > 0 ? _previewGUIList[_popupSelectIndex] : null;

        [MenuItem("jwellone/Window/UI Shader Preview")]
        static void Init()
        {
            var window = EditorWindow.GetWindow(typeof(UIShaderPreviewWindow));
            window.titleContent = new("UI Shader Preview");
            window.minSize = new Vector2(512, 512);
            window.Show();
        }

        void OnEnable()
        {
            var targetType = typeof(IPreviewGUI);
            var types = AppDomain.CurrentDomain.GetAssemblies().
                    SelectMany(a => a.GetTypes()).Where(t => !t.IsAbstract && !t.IsInterface && t.IsClass && targetType.IsAssignableFrom(t)).ToList();

            foreach (var t in types)
            {
                var instance = Activator.CreateInstance(t) as IPreviewGUI;
                _previewGUIList.Add(instance!);
            }

            _previewGUIList.Sort((a, b) => a.displayName.CompareTo(b.displayName));

            _popupDisplayNames = new string[_previewGUIList.Count];
            for (var i = 0; i < _previewGUIList.Count; ++i)
            {
                _popupDisplayNames[i] = _previewGUIList[i].displayName;
            }

            _popupSelectIndex = 0;
        }

        void OnDisable()
        {
            foreach (var preview in _previewGUIList)
            {
                preview.Dispose();
            }
            _previewGUIList.Clear();
        }

        void OnGUI()
        {
            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            var index = EditorGUILayout.Popup(_popupSelectIndex, _popupDisplayNames, GUILayout.Width(256));
            if (index != _popupSelectIndex)
            {
                _popupSelectIndex = index;
            }

            if (GUILayout.Button("保存", GUILayout.Width(48)))
            {
                var filePath = EditorUtility.SaveFilePanel("Preview保存", Application.dataPath + "/../", "", ".png");
                if (!string.IsNullOrEmpty(filePath))
                {
                    previewGui?.Save(filePath);
                }
            }

            GUILayout.EndHorizontal();

            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

            previewGui?.OnGUI(this);
        }
    }
}