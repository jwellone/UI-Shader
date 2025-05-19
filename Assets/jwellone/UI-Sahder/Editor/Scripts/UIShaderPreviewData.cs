using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
    public sealed class BinarizationPreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "Binarization";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/Binarization");
    }

    public sealed class CirclePreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "Circle";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/Circle");
    }

    public sealed class DitheringPreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "Dithering";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/Dithering");
    }

    public sealed class EdgeFilterPreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "EdgeFilter";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/EdgeFilter");
    }

    public sealed class FillPreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "Fill";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/Fill");
    }

    public sealed class GlowPreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "Glow";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/Glow");
    }

    public sealed class GrayscalePreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "Grayscale";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/Grayscale");
    }

    public sealed class HeightMapToNormalMapPreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "HeightMapToNormalMap";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/HeightMapToNormalMap");
    }

    public sealed class HSVPreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "HSV";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/HSV");
    }

    public sealed class IntertColorsPreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "IntertColors";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/IntertColors");
    }

    public sealed class MosaicPreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "Mosaic";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/Mosaic");
    }

    public sealed class OutlinePreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "Outline";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/Outline");
    }

    public sealed class PosterizePreviewGUI : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "Posterize";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/Posterize");
    }

    public sealed class RadialBlurPreviewGUI : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "RadialBlur";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/RadialBlur");
    }

    public sealed class RGBShiftPreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "RGBShift";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/RGBShift");
    }

    public sealed class SepiaPreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "Sepia";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/Sepia");
    }

    public sealed class VignettePreviewData : IUIShaderPreviewData
    {
        string IUIShaderPreviewData.displayName => "Vignette";
        Shader? IUIShaderPreviewData.shader => Shader.Find("UI/Vignette");
    }
}
