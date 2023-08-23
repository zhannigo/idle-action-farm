using System;
using System.Collections;
using HeroComponents.AnimatorComponents;
using UnityEngine;

namespace HeroComponents
{
  [RequireComponent(typeof(HeroAnimator), typeof(Hero))]
  public class Cutter : MonoBehaviour
  {
    private HeroAnimator _heroAnimator;
    
    private GameObject _sickle;
    private Collider _sickleCollider;
    private ParticleSystem _cuttingEffect;
    private float _cuttingDistance;
    private LayerMask _weatMask;
    private Hero _heroData;

    private void Awake()
    {
      _heroAnimator = GetComponent<HeroAnimator>();
      _heroData = GetComponent<Hero>();
    }

    private void Start()
    {
      if (_heroData.Sickle == null)
      {
        throw new Exception("Get sickle");
      }

      _heroData.CollusionTracker.IsWeat += MakeCut;;
      CreateSickle();
    }

    private void CreateSickle()
    {
      _sickle = Instantiate(_heroData.Sickle, _heroData.SicklePosition);
      _sickleCollider = _sickle.GetComponent<Collider>();
      _cuttingEffect = _sickle.GetComponentInChildren<ParticleSystem>();
      _sickle.SetActive(false);
    }

    private void MakeCut()
    {
      if (!_heroAnimator.IsCutting)
      {
        _heroAnimator.PlayCut();
      }
    }
    private void OnAnimationStarted() => 
      ShowSickle();

    private void OnAnimationEnded() => 
      HideSickle();

    private void ShowSickle()
    {
      _sickle.gameObject.SetActive(true);
      _sickleCollider.enabled = true;
      _cuttingEffect.Play();
    }

    private void HideSickle()
    {
      _sickle.SetActive(false);
      _sickleCollider.enabled = false;
      _cuttingEffect.Stop();
    }

  }
}