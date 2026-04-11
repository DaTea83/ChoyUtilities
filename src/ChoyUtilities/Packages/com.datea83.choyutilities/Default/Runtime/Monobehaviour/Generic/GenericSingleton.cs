using System;
using System.Threading;
using Unity.Entities;
using UnityEngine;

namespace ChoyUtilities {

    public abstract class GenericSingleton<T> : MonoBehaviour
        where T : MonoBehaviour {
        
        public static T Instance { get; private set; }

        protected virtual void Awake() {
            InitSingleton();
        }

        protected virtual void OnDisable() {
            CancelTask();
        }

        protected virtual void OnDestroy() {
            UnInitSingleton();
        }

        protected virtual void InitSingleton() {
            if (Instance is not null && Instance != this) {
                Destroy(gameObject);

                return;
            }

            Instance = (T)(MonoBehaviour)this;
        }

        protected virtual void UnInitSingleton() {
            if (Instance == this)
                Instance = null;
        }

        #region AsyncCancellation

        private CancellationTokenSource _cts = new();
        protected CancellationToken Token => _cts.Token;
        protected event Action OnCancelTask;

        protected void CancelTask() {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            OnCancelTask?.Invoke();
        }

        #endregion

    }

}