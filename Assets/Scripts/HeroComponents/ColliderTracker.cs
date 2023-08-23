using System;
using UnityEngine;

namespace HeroComponents
{
  public class ColliderTracker: MonoBehaviour
  {
    [SerializeField] private LayerMask lootMask;
    [SerializeField] private float lootCollectionRadius;
    [SerializeField] private LayerMask houseMask;
    [SerializeField] private float houseDetectionRadius;
    [SerializeField] private float _cuttingDistance;
    [SerializeField] private LayerMask _weatMask;

    public Action<GameObject, Loot> IsLoot;
    public Action<Transform> IsHouse;
    public Action IsWeat;

    private void Update()
    {
      var lootColliders = Physics.OverlapSphere(transform.position, lootCollectionRadius, lootMask);
      if (lootColliders != null)
      {
        foreach (var lootCollider in lootColliders)
        {
          var loot = lootCollider.GetComponent<Loot>();
          if(loot.State == LootState.Dropped) 
            IsLoot?.Invoke(lootCollider.gameObject, loot);
        }
      }

      var houseColliders = Physics.OverlapSphere(transform.position, houseDetectionRadius, houseMask);
      if (houseColliders.Length > 0)
      {
        IsHouse?.Invoke(houseColliders[0].transform);
      }
      
      if (Physics.OverlapSphere(transform.position, _cuttingDistance, _weatMask).Length > 0)
        IsWeat?.Invoke();
    }
  
  }
}