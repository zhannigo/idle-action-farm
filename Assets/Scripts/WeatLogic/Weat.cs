using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WeatLogic
{
  public class Weat : MonoBehaviour
  {
    [SerializeField] private ParticleSystem _curveFx;
    [SerializeField] private LayerMask sickleMask;
    [SerializeField] private GameObject _lootPrefab;
    private float spawnRadius = 2;

    private void Update()
    {
      if (Physics.CheckSphere(transform.position, 1f, sickleMask))
      {
        StartCoroutine(SpawnLoot());
      }
    }

    private IEnumerator SpawnLoot()
    {
      _curveFx.Play();
      yield return new WaitForSeconds(0.5f);
      Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
      spawnPosition.y = Mathf.Max(spawnPosition.y, 1f);

      Instantiate(_lootPrefab, spawnPosition, Quaternion.identity);
      Destroy(gameObject);
      GetComponentInParent<WeatField>().RemoveWeat(this);
    }
  }
}