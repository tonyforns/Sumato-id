namespace SumatoId
{
    public interface IFrameSource
    {
        bool Read(out IFrame frame); 
    }
}
