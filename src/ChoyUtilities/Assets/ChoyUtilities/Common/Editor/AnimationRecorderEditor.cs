// Copyright 2026 DaTea83
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#if UNITY_EDITOR
using UnityEditor.Animations;
using UnityEngine;

// ReSharper disable CheckNamespace
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace ChoyUtilities.Editor {
    [AddComponentMenu("Choy Utilities/Animation Recorder")]
    [RequireComponent(typeof(Animator))]
    public class AnimationRecorderEditor : MonoBehaviour {
        [SerializeField] private AnimationClip animationClip;
        [SerializeField] private float duration = 1.0f;

        [Header("Fire Event")] [SerializeField]
        private string className;

        [SerializeField] private string methodName;
        private bool _canRecord;
        private GameObjectRecorder _recorder;

        private float _timer;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start() {
            _recorder = new GameObjectRecorder(gameObject);
            _recorder.BindComponentsOfType<Transform>(gameObject, true);

            _timer = duration;
        }

        private void LateUpdate() {
            if (_canRecord)
                RecordAnimation();
        }

        private void OnGUI() {
            if (!GUI.Button(new Rect(0, 0, 200, 40), "Start Record")) return;
            if (_canRecord) return;
            HelperCollection.Broadcaster.Call(methodName);
            _canRecord = true;
        }

        private void RecordAnimation() {
            _timer -= Time.unscaledDeltaTime;

            if (_timer < 0) {
                if (!_recorder.isRecording) return;
                _recorder.SaveToClip(animationClip);
                print("End Recording");

                _canRecord = false;
                _timer = duration;
            }
            else {
                _recorder.TakeSnapshot(Time.unscaledDeltaTime);
            }
        }
    }
}
#endif