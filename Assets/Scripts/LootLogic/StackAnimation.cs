using System.Collections.Generic;
using UnityEngine;

public class StackAnimation
{
  private readonly Loot _Loot;

  public StackAnimation(Loot loot)
  {
    _Loot = loot;
  }

  public void Connect(Transform parentTransform, bool connect, float maxLoot)
  {
    _Loot.transform.localRotation = Quaternion.identity;
    _Loot.lootRb.mass = _Loot.MassFactor * maxLoot;

    if (!connect)
    {
      ConnectJoint(parentTransform, maxLoot);
      _Loot.lootRb.isKinematic = false;
    }
  }

  private void ConnectJoint(Transform parentTransform, float maxLoot)
  {
    var joint = _Loot.TryGetComponent(out FixedJoint x)? x : _Loot.gameObject.AddComponent<FixedJoint>();
    _Loot.lootRb.drag = 100 * maxLoot;
    _Loot.lootRb.angularDrag = 10;
    joint.anchor = new Vector3(0,0.05f,0);
    joint.connectedBody = parentTransform.GetComponent<Rigidbody>();
    joint.enableCollision = true;
  }
}