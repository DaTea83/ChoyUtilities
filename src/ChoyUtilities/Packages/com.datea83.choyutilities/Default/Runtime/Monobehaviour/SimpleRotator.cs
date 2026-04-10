using System;
using EugeneC.Utilities;
using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {

    [AddComponentMenu("Eugene/Simple Rotater")]
    [DisallowMultipleComponent]
    public class SimpleRotator : MonoBehaviour {

        [Flags]
        private enum EAxis : byte {

            None = 0,
            X = 1,
            Y = 1 << 1,
            Z = 1 << 2

        }

        [SerializeField] private EAxis rotateAxis;
        [SerializeField] private float rotateSpeed;

        private float3 _rotateDirection;
    
        private void Start() {
            if ((rotateAxis & EAxis.X) != 0)
                _rotateDirection.x = 1f;

            if ((rotateAxis & EAxis.Y) != 0)
                _rotateDirection.y = 1f;

            if ((rotateAxis & EAxis.Z) != 0)
                _rotateDirection.z = 1f;
        
            if (rotateAxis == EAxis.None)
                this.enabled = false;
        }

        private void Update() {
            transform.Rotate(_rotateDirection * (rotateSpeed * Time.deltaTime));
        }

    }

}