
public interface IPadInput {

    bool InputPressed { get; }
    float Position { get; }
    float Rotation { get; }
    float RawVerticalDelta { get; }

    void Refresh();
}
