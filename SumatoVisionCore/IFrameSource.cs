namespace SumatoVisionCore;

public interface IFrameSource
{
    bool Read(out IFrame frame); 
}
