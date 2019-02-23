
public interface IPadInput {

    bool InputPressed { get; }
    float Position { get; }
    float Rotation { get; }

    void Refresh();
}
