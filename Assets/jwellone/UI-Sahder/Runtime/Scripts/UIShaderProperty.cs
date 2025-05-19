using UnityEngine;

#nullable enable

namespace jwellone
{
    public static class UIShaderProperty
    {
        public readonly static int rate = Shader.PropertyToID("_Rate");
        public readonly static int color = Shader.PropertyToID("_Color");
        public readonly static int scaleFactor = Shader.PropertyToID("_ScaleFactor");
        public readonly static int parallaxScale = Shader.PropertyToID("_ParallaxScale");
        public readonly static int outlineSize = Shader.PropertyToID("_OutlineSize");
        public readonly static int outlineColor = Shader.PropertyToID("_OutlineColor");
        public readonly static int sampleCount = Shader.PropertyToID("_SampleCount");
        public readonly static int intensity = Shader.PropertyToID("_Intensity");
        public readonly static int centerX = Shader.PropertyToID("_CenterX");
        public readonly static int centerY = Shader.PropertyToID("_CenterY");
        public readonly static int vignetteColor = Shader.PropertyToID("_VignetteColor");
        public readonly static int radius = Shader.PropertyToID("_Radius");
        public readonly static int softness = Shader.PropertyToID("_Softness");

        public readonly static string grayscaleNtscKeyword = "GRAYSCALE_NTSC";
        public readonly static string radialBlurUseDitherKeyword = "RADIAL_BLUR_USE_DITHER";
    }
}