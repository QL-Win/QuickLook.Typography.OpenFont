//MIT, 2016-present, WinterDev
using BrotliSharpLib;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using Typography.OpenFont.WebFont;

namespace QuickLook.Typography.OpenFont;

public static class OurOpenFontSystemSetup
{
    public static void SetupWoff2DecompressFunctions()
    {
        // Woff2
        Woff2DefaultBrotliDecompressFunc.DecompressHandler = static (byte[] compressedBytes, Stream output) =>
        {
            bool result = false;
            try
            {
                using (var ms = new MemoryStream(compressedBytes))
                {
                    ms.Position = 0; // Set to start pos
                    Decompress(ms, output);
                }
                result = true;
            }
            catch (Exception ex)
            {
                _ = ex;
            }
            return result;
        };
    }

    [SuppressMessage("Style", "IDE0063:Use simple 'using' statement")]
    private static void Decompress(Stream input, Stream output)
    {
        try
        {
            using (var bs = new BrotliStream(input, CompressionMode.Decompress))
            using (var ms = new MemoryStream())
            {
                bs.CopyTo(output);
            }
        }
        catch (IOException ex)
        {
            throw ex;
        }
    }
}
