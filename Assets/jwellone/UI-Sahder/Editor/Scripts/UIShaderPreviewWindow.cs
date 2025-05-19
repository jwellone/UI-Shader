using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;

#nullable enable

namespace jwelloneEditor
{
    public class UIShaderPreviewWindow : EditorWindow
    {
        Vector2 _scrollPosition;
        Material? _material;
        Texture? _textureBg;
        Texture2D? _sourceTexture;
        Texture2D? _destTexture;
        MaterialEditor? _materialEditor;
        string[] _popupDisplayNames = null!;
        int _popupSelectIndex;
        readonly List<IUIShaderPreviewData> _previewGUIList = new();

        IUIShaderPreviewData? previewGui => _previewGUIList.Count > 0 ? _previewGUIList[_popupSelectIndex] : null;

        public Texture2D? sourceTexture => _sourceTexture;

        [MenuItem("jwellone/Window/UI Shader Preview")]
        static void Init()
        {
            var window = GetWindow(typeof(UIShaderPreviewWindow));
            window.titleContent = new("UI Shader Preview");
            window.minSize = new Vector2(512, 512);
            window.Show();
        }

        void OnEnable()
        {
            var targetType = typeof(IUIShaderPreviewData);
            var types = AppDomain.CurrentDomain.GetAssemblies().
                    SelectMany(a => a.GetTypes()).Where(t => !t.IsAbstract && !t.IsInterface && t.IsClass && targetType.IsAssignableFrom(t)).ToList();

            foreach (var t in types)
            {
                var instance = Activator.CreateInstance(t) as IUIShaderPreviewData;
                _previewGUIList.Add(instance!);
            }

            _previewGUIList.Sort((a, b) => a.displayName.CompareTo(b.displayName));

            _popupDisplayNames = new string[_previewGUIList.Count];
            for (var i = 0; i < _previewGUIList.Count; ++i)
            {
                _popupDisplayNames[i] = _previewGUIList[i].displayName;
            }

            _popupSelectIndex = 0;

            _textureBg = AssetDatabase.LoadAssetAtPath<Texture>("Assets/jwellone/UI-Sahder/Editor/Textures/preview-bg.jpg");
        }

        void OnDisable()
        {
            _previewGUIList.Clear();
            _sourceTexture = null;
            _textureBg = null;
            DestroyDestTexture();
            DestroyMaterial();
            DestroyMaterialEditor();
        }

        void OnGUI()
        {
            EditorGUI.BeginChangeCheck();

            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            var index = EditorGUILayout.Popup(_popupSelectIndex, _popupDisplayNames);

            _sourceTexture = (Texture2D)EditorGUILayout.ObjectField(_sourceTexture, typeof(Texture2D), false);

            if (GUILayout.Button("リセット", GUILayout.Width(54)))
            {
                DestroyDestTexture();
                DestroyMaterial();
                DestroyMaterialEditor();
            }

            if (GUILayout.Button("保存", GUILayout.Width(54)))
            {
                var filePath = EditorUtility.SaveFilePanel("Preview保存", Application.dataPath + "/../", "", ".png");
                Save(filePath);
            }

            GUILayout.EndHorizontal();

            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

            CreateDestTextureIfNeeded();
            var width = position.size.x / 2f;
            if (_destTexture != null)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                var textureRect = GUILayoutUtility.GetRect(width, _destTexture.height * width / _destTexture.width, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));

                GUI.BeginGroup(textureRect);
                {
                    var xLoop = Mathf.RoundToInt(textureRect.width);
                    var yLoop = Mathf.RoundToInt(textureRect.height);
                    var bgWidth = _textureBg!.width;
                    var bgHeight = _textureBg!.height;
                    var bgRect = new Rect(0, 0, bgWidth, bgHeight);
                    for (var y = 0; y < yLoop; y += bgHeight)
                    {
                        for (var x = 0; x < xLoop; x += bgWidth)
                        {
                            bgRect.x = x;
                            bgRect.y = y;
                            GUI.DrawTexture(bgRect, _textureBg);
                        }
                    }
                }
                GUI.EndGroup();

                GUI.DrawTexture(textureRect, _destTexture);

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

            if (index != _popupSelectIndex || _material == null)
            {
                _popupSelectIndex = index;

                DestroyMaterial();
                DestroyMaterialEditor();
            }

            _material ??= new Material(previewGui?.shader);
            if (_materialEditor == null && _material != null)
            {
                _materialEditor = (MaterialEditor)Editor.CreateEditor(_material, typeof(MaterialEditor));
                if (_materialEditor != null)
                {
                    UnityEditorInternal.InternalEditorUtility.SetIsInspectorExpanded(_materialEditor.target, true);
                }
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            GUILayout.BeginVertical();
            _materialEditor?.OnInspectorGUI();
            GUILayout.EndVertical();

            EditorGUILayout.EndScrollView();

            if (EditorGUI.EndChangeCheck())
            {
                Blit(_sourceTexture, _destTexture, _material);
            }
        }

        void CreateDestTextureIfNeeded()
        {
            if (_sourceTexture == null)
            {
                return;
            }

            var width = _sourceTexture.width;
            var height = _sourceTexture.height;
            if (_destTexture == null || width != _destTexture.width || height != _destTexture.height)
            {
                DestroyDestTexture();
                _destTexture = new Texture2D(width, height, TextureFormat.BGRA32, false);
            }
        }

        void Save(string path)
        {
            if (string.IsNullOrEmpty(path) || _destTexture == null)
            {
                return;
            }

            byte[] bytes = null!;
            switch (Path.GetExtension(path))
            {
                case ".jpg":
                case ".jpeg":
                    bytes = _destTexture.EncodeToJPG();
                    break;

                case ".tga":
                    bytes = _destTexture.EncodeToTGA();
                    break;

                default:
                    bytes = _destTexture.EncodeToPNG();
                    break;
            }

            File.WriteAllBytes(path, bytes);
        }

        void Blit(Texture2D? source, Texture2D? dest, Material? material)
        {
            if (source == null || dest == null)
            {
                return;
            }

            var tmpRT = RenderTexture.active;
            var rt = RenderTexture.GetTemporary(source.width, source.height, 24, dest.graphicsFormat);
            rt.Release();

            RenderTexture.active = rt;

            Graphics.Blit(source, rt, material);

            dest.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            dest.Apply();

            RenderTexture.ReleaseTemporary(rt);
            RenderTexture.active = tmpRT;
        }

        void DestroyDestTexture()
        {
            if (_destTexture != null)
            {
                DestroyImmediate(_destTexture);
                _destTexture = null;
            }
        }

        void DestroyMaterial()
        {
            if (_material != null)
            {
                DestroyImmediate(_material);
                _material = null;
            }
        }

        void DestroyMaterialEditor()
        {
            if (_materialEditor != null)
            {
                DestroyImmediate(_materialEditor);
                _materialEditor = null;
            }
        }
    }
}