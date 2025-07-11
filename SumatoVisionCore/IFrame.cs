using System.Numerics;

namespace SumatoVisionCore;

public interface IFrame
{
    object Frame { get; }
    IFrame Resize(int width, int height);
    bool IsEmpty => Frame == null;
    Vector2 Size { get; }
}
