using OpenCvSharp;
using System.Numerics;

namespace SumatoId;
public class MatFrame : IFrame
{
    public Mat RawMat { get; private set; }

    public Vector2 Size { get; private set; }

    public MatFrame(Mat mat)
    {
        SetFrame(mat);
    }

    public object Frame => RawMat;

    public IFrame Resize(int width, int height)
    {
        if (IsTheSameSize(width, height)) return this; 

        var resized = new Mat();
        Cv2.Resize(RawMat, resized, new Size(width, height));
        RawMat.Dispose();

        SetFrame(resized);

        return this;
    }

    private bool IsTheSameSize(int width, int height)
    {
        return Size.X == width && Size.Y == height;
    }

    private void SetFrame(Mat mat)
    {
        RawMat = mat;
        Size = new Vector2(mat.Width, mat.Height);
    }
}
