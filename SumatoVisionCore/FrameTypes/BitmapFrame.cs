using System.Drawing;
using System.Numerics;

namespace SumatoVisionCore;
public class BitmapFrame : IFrame
{
    public Bitmap RawBitmap { get; private set; }

    public Vector2 Size { get; private set; }

    public BitmapFrame(Bitmap bitmap)
    {
        SetFrame(bitmap);
    }

    public object Frame => RawBitmap;

    public IFrame Resize(int width, int height)
    {
        if(IsTheSameSize(width, height)) return this;

        var resized = new Bitmap(width, height);
        using (var g = Graphics.FromImage(resized))
        {
            g.DrawImage(RawBitmap, 0, 0, width, height);
        }
         
        SetFrame(resized);    
        RawBitmap.Dispose();

        return this;
    }

    private bool IsTheSameSize(int width, int height)
    {
        return Size.X == width && Size.Y == height;
    }

    private void SetFrame(Bitmap bitmap)
    {
        RawBitmap = bitmap;
        Size = new Vector2(bitmap.Width, bitmap.Height);
    }
}