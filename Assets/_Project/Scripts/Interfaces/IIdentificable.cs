
public enum ObjectID
{
    NONE = 0,

    MEMORYTILE_HOMO = 1,
    MEMORYTILE_CROOS = 2,
    MEMORYTILE_BLUETOOTH = 3,
    MEMORYTILE_HOLE = 4,
    MEMORYTILE_SQUIRCLE = 5,
    MEMORYTILE_PLATFORM = 6,
    MEMORYTILE_TURRET = 7,
    MEMORYTILE_COW = 8,
}

public interface IIdentificable
{
    public ObjectID ID { get;}
}