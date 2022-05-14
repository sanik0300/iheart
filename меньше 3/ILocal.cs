
namespace меньше_3
{
    public enum Sentences : int { On, Off, Shh, Vibr, Title, Stop, ThereIsCall, WaitThroughCall, AudioHere, IfStillPlay, Yes, No }
    public interface ILocal
    {
        string[] words { get; }
        bool NeedToDarkenLeTheme();
    }
}
