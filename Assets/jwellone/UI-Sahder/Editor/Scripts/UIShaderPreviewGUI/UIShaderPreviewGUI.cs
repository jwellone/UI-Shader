using System;
using System.IO;
using UnityEngine;
using UnityEditor;

#nullable enable

namespace jwelloneEditor
{
    public abstract class UIShaderPreviewGUI : IPreviewGUI
    {
        public virtual string displayName
        {
            get => this.GetType().Name.Replace("PreviewGUI", "");
        }

        bool _disposed;
        Material? _cacheMaterial;
        Texture2D? _sourceTexture;
        Texture2D? _destTexture;
        protected Material _material => _cacheMaterial ??= new Material(GetShader());

        ~UIShaderPreviewGUI()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void OnGUI(EditorWindow parent)
        {
            EditorGUILayout.BeginHorizontal();

            var halfWidth = parent.position.size.x / 2.0f - 12;
            GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(halfWidth));

            GUILayout.BeginHorizontal();
            GUILayout.Label("Source");

            _sourceTexture = (Texture2D)EditorGUILayout.ObjectField(_sourceTexture, typeof(Texture2D), false);

            GUILayout.EndHorizontal();

            if (_sourceTexture != null)
            {
                var textureRect = GUILayoutUtility.GetRect(halfWidth, _sourceTexture.height * halfWidth / _sourceTexture.height, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
                GUI.DrawTexture(textureRect, _sourceTexture);
            }
            else
            {
                GUILayout.Space(halfWidth);
            }

            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(halfWidth));

            GUILayout.Label("Dest");
            GUILayout.Space(1);

            CreateOrUpdateDestTextureIfNeeded();
            if (_destTexture != null)
            {
                var textureRect = GUILayoutUtility.GetRect(halfWidth, _destTexture.height * halfWidth / _destTexture.height, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
                GUI.DrawTexture(textureRect, _destTexture);
            }
            else
            {
                GUILayout.Space(halfWidth);
            }

            GUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }

        public void Save(string path)
        {
            if (_destTexture == null)
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

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            if (_cacheMaterial != null)
            {
                GameObject.DestroyImmediate(_cacheMaterial);
                _cacheMaterial = null;
            }

            _sourceTexture = null;

            if (_destTexture != null)
            {
                Texture.DestroyImmediate(_destTexture);
                _destTexture = null;
            }
        }

        void CreateOrUpdateDestTextureIfNeeded()
        {
            if (_sourceTexture == null)
            {
                return;
            }

            var width = _sourceTexture.width;
            var height = _sourceTexture.height;
            if (_destTexture == null || width != _destTexture.width || height != _destTexture.height)
            {
                if (_destTexture != null)
                {
                    GameObject.DestroyImmediate(_destTexture);
                    _destTexture = null;
                }

                _destTexture = new Texture2D(width, height, TextureFormat.BGRA32, false);
            }

            Blit(_sourceTexture, _destTexture, _material);
        }

        protected virtual Shader? GetShader()
        {
            return null;
        }

        protected virtual void UpdateMaterialProperty()
        {
        }

        protected void Blit(Texture2D source, Texture2D dest, Material? material)
        {
            var tmpRT = RenderTexture.active;
            var rt = RenderTexture.GetTemporary(source.width, source.height, 24, dest.graphicsFormat);
            rt.Release();

            RenderTexture.active = rt;

            UpdateMaterialProperty();
            Graphics.Blit(source, rt, material);

            dest.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            dest.Apply();

            RenderTexture.ReleaseTemporary(rt);
            RenderTexture.active = tmpRT;
        }
    }
}