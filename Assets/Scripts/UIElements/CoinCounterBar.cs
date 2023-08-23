using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UIElements
{
  public class CoinCounterBar : MonoBehaviour
  {
    [SerializeField] private TMP_Text _textBar;
    [SerializeField] private House _house;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform _hud;
    private int _coints;
    private int _animatedSpeed;

    private void Start()
    {
      UpdateBar(_coints);
      if(_house!=null) 
        _house.IsLootSelled += CollectCoins;
    }

    private void CollectCoins(int lootCoints)
    {
      AnimateCoint(lootCoints);
    }

    private void AnimateCoint(int lootCoints)
    {
      var spawnPos =  Camera.main.WorldToScreenPoint(_house.transform.position);
      var coin = Instantiate(coinPrefab, spawnPos, Quaternion.identity, _hud);
      coin.transform.DOMove(transform.position, 0.3f)
        .SetDelay(0.1f)
        .OnComplete(() =>
        {
          Destroy(coin.gameObject);
          transform.DOShakePosition(0.5f, 0.6f);
          transform.DOShakeScale(0.2f, 0.2f);
          StartCoroutine(AnimateText(lootCoints));
        });
    }

    private void UpdateBar(int count) => 
      _textBar.text = $"{count}";

    private IEnumerator AnimateText(int count)
    {
      int countInText = _coints;
      float animatedTime = _animatedSpeed / count;
    
      while (countInText != _coints + count)
      {
        countInText += 1;
        UpdateBar(countInText);
        yield return new WaitForSeconds(animatedTime);
      }
      _coints += count;
    }
  }
}