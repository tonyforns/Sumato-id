
namespace SumatoId;

public interface IFrame
{
    object Frame { get; }
    IFrame Resize(int width, int height);
    bool IsEmpty => Frame == null;
}
