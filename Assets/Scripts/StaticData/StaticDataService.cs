using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string HeroStaticDataPath = "StaticData/HeroData";
    private HeroStaticData _hero;

    public void LoadStaticData()
    {
      _hero = Resources.Load<HeroStaticData>(HeroStaticDataPath);
    }

    public HeroStaticData ForHero() => _hero;
  }
}