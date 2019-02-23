public abstract class State
{
    public abstract void Enter(PadLateralMove pad);
    public abstract State HandleInput();
    public abstract void Update(float deltaTime);
    public abstract void Exit();
}
