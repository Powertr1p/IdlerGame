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
        private AsyncOperationHandle<GameObject> _operationHandle;

        public async UniTask<GameObject> InstantiateGameObject(AssetReferenceGameObject reference, CancellationToken cancellationToken = default)
        {
            _operationHandle = Addressables.InstantiateAsync(reference); 
            var result = await _operationHandle.ToUniTask(cancellationToken: cancellationToken);

            if (_operationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                return result;
            }
            
            Debug.LogError($"failed to load gameObject: {_operationHandle.OperationException}");
            return null;
        }

        public void Dispose()
        {
            if (_operationHandle.IsValid())
            {
                Addressables.ReleaseInstance(_operationHandle);
            }
        }
    }
}