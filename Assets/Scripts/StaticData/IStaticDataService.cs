using System.Collections.Generic;

namespace StaticData
{
  public interface IStaticDataService
  {
    void LoadStaticData();
    HeroStaticData ForHero();
  }
}