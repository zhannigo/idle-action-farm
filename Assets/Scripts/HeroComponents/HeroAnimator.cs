using System;
using HeroComponents.AnimatorComponents;
using UnityEngine;

namespace HeroComponents
{
  [RequireComponent(typeof(CharacterController), typeof(Animator))]
  public class HeroAnimator : MonoBehaviour, IAnimationStateReader
  {
    public AnimatorState State { get; set; }
    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;
    
    private static readonly int MoveHash = Animator.StringToHash("Walk");
    private static readonly int Cut = Animator.StringToHash("Cut");
    private static readonly int Throw = Animator.StringToHash("Throw");

    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _cutStateHash = Animator.StringToHash("Cut");
    private readonly int _walkingStateHash = Animator.StringToHash("Move");

    private CharacterController _characterController;
    private Animator _animator;

    private void Awake()
    {
      _animator = GetComponent<Animator>();
      _characterController = GetComponent<CharacterController>();
    }

    private void Update() => 
      _animator.SetFloat(MoveHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);

    public void PlayCut() => _animator.SetTrigger(Cut);

    public bool IsCutting => _animator.GetCurrentAnimatorStateInfo(0).IsName("slash");

    public void PlayThrow() => _animator.SetTrigger(Throw);
    public bool IsThrow => _animator.GetCurrentAnimatorStateInfo(0).IsName("throw");
    
    public void ResetToIdle() => _animator.Play(_idleStateHash, -1);


    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
      StateExited?.Invoke(StateFor(stateHash));

    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;
      if (stateHash == _idleStateHash)
        state = AnimatorState.Idle;
      else if (stateHash == _walkingStateHash)
        state = AnimatorState.Walking;
      else if (stateHash == _cutStateHash)
        state = AnimatorState.Cut;
      else
      {
        state = AnimatorState.Unknown;
      }
      return state;
    }
  }
}