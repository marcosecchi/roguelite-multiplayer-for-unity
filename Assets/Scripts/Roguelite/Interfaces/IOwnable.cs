namespace TheBitCave.MultiplayerRoguelite.Interfaces
{
    /// <summary>
    /// An interface used to set the owner of an object (i.e.: the owner of a bullet, etc.)
    /// </summary>
    public interface IOwnable
    {
        uint OwnerId { get; set; }
    }
}
