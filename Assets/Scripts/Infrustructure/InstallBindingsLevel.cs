using StaticData;
using UnityEngine;
using Zenject;

namespace Infrustructure
{
  public class InstallBindingsLevel : MonoInstaller, IInitializable
  {
    [SerializeField] public GameObject JoystickPrefab;

    public override void InstallBindings()
    {
      Container.BindInterfacesTo<InstallBindingsLevel>().FromInstance(this).AsSingle();
      Container.Bind<VariableJoystick>().FromComponentOn(JoystickPrefab).AsSingle();
      Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle().NonLazy();
    }

    public void Initialize()
    {
      var staticData = Container.Resolve<IStaticDataService>();
      staticData.LoadStaticData();
    }
  }
}