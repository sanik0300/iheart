
namespace меньше_3
{
    enum Sentences : int { On, Off, Shh, Vibr, Title, ThereIsCall }
    public interface ILocal
    {
        string[] words { get; }
        bool NeedToDarkenLeTheme();
    }
}
