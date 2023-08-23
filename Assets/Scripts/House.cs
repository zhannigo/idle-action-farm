using System;
using UnityEngine;

public class House : MonoBehaviour
{
  [SerializeField] private float lootCollectionRadius;
  [SerializeField] private LayerMask lootMask;
  public event Action<int> IsLootSelled;

  private void Update()
  {
    var lootColliders = Physics.OverlapSphere(transform.position, lootCollectionRadius, lootMask);
    if (lootColliders != null)
    {
      foreach (var lootCollider in lootColliders)
      {
        var loot = lootCollider.GetComponent<Loot>();
        if (loot.State == LootState.OnSell)
        {
          SellLoot(loot);
        }
      }
    }
  }

  private void SellLoot(Loot loot)
  {
    IsLootSelled?.Invoke(loot.Coins);
    Destroy(loot.gameObject);
  }
}