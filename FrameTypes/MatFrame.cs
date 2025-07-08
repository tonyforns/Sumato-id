using OpenCvSharp;

namespace SumatoId;
public class MatFrame : IFrame
{
    public Mat RawMat { get; private set; }

    public MatFrame(Mat mat)
    {
        RawMat = mat;
    }

    public object Frame => RawMat;

    public IFrame Resize(int width, int height)
    {
        var resized = new Mat();
        Cv2.Resize(RawMat, resized, new Size(width, height));
        return new MatFrame(resized);
    }

}
