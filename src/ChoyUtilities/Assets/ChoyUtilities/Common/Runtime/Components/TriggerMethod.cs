using UnityEngine;

namespace ChoyUtilities {
    
    [RequireComponent(typeof(Collider))]
    public class TriggerMethod : MonoBehaviour {
        [SerializeField] private LayerMask layer;
        [SerializeField] private string objectTag;

        [SerializeField] private string instanceClassName;
        [SerializeField] private string methodName;
        [SerializeField] private bool turnOffAfter;

        private Collider _collider;

        // Start is called before the first frame update
        private void Start() {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.layer != layer && !other.gameObject.CompareTag(objectTag)) return;

            HelperCollection.Broadcaster.Call(methodName);

            if (turnOffAfter)
                gameObject.SetActive(false);
        }
    }
}