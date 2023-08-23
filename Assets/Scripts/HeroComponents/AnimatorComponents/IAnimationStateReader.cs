namespace HeroComponents.AnimatorComponents
{
  public interface IAnimationStateReader
  {
    void ExitedState(int stateHash);
    void EnteredState(int stateHash);
    AnimatorState State { get; }
  }
}