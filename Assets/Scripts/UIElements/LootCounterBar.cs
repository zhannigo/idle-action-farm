using HeroComponents;
using TMPro;
using UnityEngine;

namespace UIElements
{
  public class LootCounterBar: MonoBehaviour
  {
    [SerializeField] private LootCollector _Backpack;
    [SerializeField] private TMP_Text _textBar;

    private void Start()
    {
      UpdateBar(0,40);
      if(_Backpack!=null) 
        _Backpack.IsLootCollected += UpdateBar;
    }

    private void UpdateBar(int currentCount, int maxCount) => 
      _textBar.text = $"{currentCount}/{maxCount}";
  }
}