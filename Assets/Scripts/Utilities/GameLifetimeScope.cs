using Utilities.SaveSystem;
using VContainer;
using VContainer.Unity;

namespace Utilities
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerInventorySaveBox>(Lifetime.Singleton);
        }
    }
}