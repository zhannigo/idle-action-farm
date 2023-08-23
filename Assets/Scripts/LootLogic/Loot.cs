using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Loot: MonoBehaviour
{
  [SerializeField] private float droppingForce = 3;
  [SerializeField] private int _cost;

  public Rigidbody lootRb;
  public LootState State { get => _state; set => _state = value; }
  public int Coins { get => _cost; }

  private StackAnimation _StackAnimation;

  private LootState _state;
  private Transform _directionTransform;
  private float _distance;
  private Vector3 _startPosition;
  private float _startTime;
  public int MassFactor = 10;
  private Sequence sequence;

  public Loot()
  {
    _StackAnimation = new StackAnimation(this);
  }

  private void Start()
  {
    lootRb = GetComponent<Rigidbody>();
    PopUpLoot();
    _state = LootState.Dropped;
  }
  private void PopUpLoot()
  {
    Vector3 lootForce = Random.insideUnitSphere * droppingForce;
    lootForce.y = Mathf.Max(lootForce.y, 1f);
    lootRb.AddForce(lootForce, ForceMode.Impulse);
  }
  
  public Loot ThrowTo(LootState state, Transform parentTransform, Vector3 direction, bool connect, int maxLoot)
  {
    _state = state;
    transform.DOLocalJump(direction, 0.5f,1, 0.3f)
      .OnComplete(() =>
      {
        _StackAnimation.Connect(parentTransform, connect, maxLoot);
      });
    return this;
  }

  public void ThrowTo(LootState state, Vector3 direction)
  {
    _state = state;
    Destroy(GetComponent<FixedJoint>());
    sequence = DOTween.Sequence();
    sequence.Append(
    transform.DOLocalJump(direction, 0.5f,1, 0.7f)
      .OnComplete(() =>
      {
        transform.localRotation = Quaternion.identity;
      })
    );
  }

  private void OnDestroy()
  {
    sequence.Kill();
  }
}