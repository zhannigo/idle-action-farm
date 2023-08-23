using UnityEngine;

namespace StaticData
{
  [CreateAssetMenu(fileName = "HeroData", menuName = "StaticData/Hero")]
  public class HeroStaticData : ScriptableObject
  {
    [Header("Movement Data")]
    public float MovementSpeed = 10f;

    [Header("Backpack Data")] public int MaxLoot;
    public float LootPosOffset;
  }
}