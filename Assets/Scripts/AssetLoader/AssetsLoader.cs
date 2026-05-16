using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AssetLoader
{
    public class AssetsLoader : IDisposable
    {
        public async UniTask<GameObject> InstantiateGameObject(AssetReferenceGameObject reference, CancellationToken cancellationToken = default)
        {
            if (ReferenceEquals(reference, null) || !reference.RuntimeKeyIsValid())
            {
                Debug.LogError($"AssetsLoader: invalid reference (guid={(!ReferenceEquals(reference, null) ? reference.AssetGUID : "(null)")}, valid={!ReferenceEquals(reference, null) && reference.RuntimeKeyIsValid()})");
                return null;
            }

            var handle = Addressables.InstantiateAsync(reference);
            var result = await handle.ToUniTask(cancellationToken: cancellationToken);

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return result;
            }

            Debug.LogError($"AssetsLoader: failed to load (guid={reference.AssetGUID}, status={handle.Status}): {handle.OperationException}");
            return null;
        }

        public void Dispose()
        {
        }
    }
}