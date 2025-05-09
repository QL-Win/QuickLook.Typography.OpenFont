﻿using System.IO;
using Typography.OpenFont;
using Typography.OpenFont.WebFont;

namespace QuickLook.Typography.OpenFont;

public static class Woff2
{
    public static PreviewFontInfo GetFontInfo(string path)
    {
        OurOpenFontSystemSetup.SetupWoff2DecompressFunctions();
        using FileStream fs = new(path, FileMode.Open, FileAccess.Read);
        using BinaryReader input = new(fs);
        Woff2Reader woff2Reader = new()
        {
            DecompressHandler = Woff2DefaultBrotliDecompressFunc.DecompressHandler,
        };
        input.BaseStream.Position = 0;
        return woff2Reader.ReadPreview(input);
    }
}
