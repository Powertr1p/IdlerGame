using VContainer;
using VContainer.Unity;

namespace Utilities.SaveSystem
{
    public class SaveSystemInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerInventorySaveBox>(Lifetime.Singleton).AsSelf();
        }
    }
}
