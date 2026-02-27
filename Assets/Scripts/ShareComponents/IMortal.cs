using System;

namespace ShareComponents
{
    public interface IMortal
    {
        bool IsAlive { get; }
        event Action OnDeath;
    }
}