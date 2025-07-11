namespace SumatoVisionCore;
public class MessageModel
{
    public byte[] Data { get; set; }
    public FrameType DataType { get; set; }

    public MessageModel(byte[] data, FrameType frameType)
    {
        Data = data;
        DataType = frameType;
    }

    public enum FrameType
    {
        Mat,
        BitMap
    }

}