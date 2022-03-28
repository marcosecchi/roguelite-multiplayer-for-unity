namespace TheBitCave.BattleRoyale.Interfaces
{
    /// <summary>
    /// Interface used to assign a type (i.e.: a character class) and an alignment
    /// </summary>
    public interface ICharacterTypeable
    {
        CharacterAlignment Alignment { get; }
        CharacterType Type { get; }
        string TypeStringified { get; }
        string AlignmentStringified { get; }
    }
}
