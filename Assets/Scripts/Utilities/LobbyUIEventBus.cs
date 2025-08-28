using System;

namespace Utilities
{
    public static class LobbyUIEventBus
    {
        public static event Action OnInventoryOpenRequested;
        public static event Action OnLobbyShowRequested;
        public static event Action OnChangeToolRequested;
        public static event Action OnRaidStarted;
        
        public static void RequestInventoryOpen() => OnInventoryOpenRequested?.Invoke();
        public static void RequestLobbyShow() => OnLobbyShowRequested?.Invoke();
        public static void RaidStarted() => OnRaidStarted?.Invoke();
        public static void ChangeTool() => OnChangeToolRequested?.Invoke();
    }
}