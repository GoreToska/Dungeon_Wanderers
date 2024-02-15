using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		// register
		Container.Bind<IInputService>().To<StandardInput>().FromNew().AsSingle();
		Container.Bind<InputHandler>().FromNew().AsSingle();
	}
}
