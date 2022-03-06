namespace TheBitCave.MultiplayerRoguelite.Interfaces
{
    /// <summary>
    /// Interface used to assign a type (i.e.: a character class)
    /// </summary>
    public interface ICharacterTypeable
    {
        CharacterType Type { get; }
    }
}
