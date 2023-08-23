using UnityEngine;

namespace WeatLogic
{
  public class WeatGrow : MonoBehaviour
  {
    [SerializeField] private float growTime;
    [SerializeField] private ParticleSystem growFX;
    [SerializeField] private GameObject _weatPrefab;

    private void Start()
    {
      _weatPrefab.SetActive(false);
      growFX.gameObject.SetActive(true);
      ParticleSystem.MainModule growFXMain = growFX.main;
      growFXMain.startLifetime = growTime;
      growFX.Play();
    }

    private void OnParticleSystemStopped()
    {
      _weatPrefab.SetActive(true);
      growFX.gameObject.SetActive(false);
    }
  }
}