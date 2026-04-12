using UnityEngine;

namespace ChoyUtilities {
    [RequireComponent(typeof(RectTransform))]
    public abstract class UiHelper : MonoBehaviour {
        [SerializeField] [Min(0.01f)] protected float transitionTime;
        
        protected RectTransform RectTransform;

        protected virtual void OnValidate() {
            RectTransform = GetComponent<RectTransform>();
        }
        
        public abstract void OnSpawn();

        public abstract float OnStartOpen();

        public abstract void OnEndOpen();

        public abstract float OnStartClose();

        public abstract void OnEndClose();
    }
}