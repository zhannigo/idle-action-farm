using UnityEngine;
using UnityEngine.Animations.Rigging;

public class StackingAnimator
{
  private readonly Transform _headTransform;
  private readonly Transform _parentRig;
  private readonly GameObject _dampedRig;

  public StackingAnimator(Transform parentRig, Transform head)
  {
    _parentRig = parentRig;
    _headTransform = head;
    _dampedRig = new GameObject("rig");
  }
  
  public void Connect(Transform parentTransform, Transform emptyLink)
  {
    var damped = Object.Instantiate(_dampedRig, _parentRig).AddComponent<DampedTransform>();
    damped.data.sourceObject = parentTransform;
    damped.data.constrainedObject = emptyLink;
    damped.data.dampPosition = 1f;
    damped.data.dampRotation = 1f;
    damped.data.maintainAim = true;
  }
}