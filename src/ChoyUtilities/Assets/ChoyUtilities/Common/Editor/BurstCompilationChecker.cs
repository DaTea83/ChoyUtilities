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

using Unity.Burst;
using UnityEngine;

// ReSharper disable CheckNamespace

namespace ChoyUtilities.Editor {
#if UNITY_EDITOR
    /// <summary>
    ///     Static class to check to see if Burst compilation is enabled in the editor.
    /// </summary>
    /// <remarks>
    ///     If Burst is disabled, a warning will be printed in the editor, giving the user more information on how to enable.
    /// </remarks>
    internal static class BurstCompilationChecker {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize() {
            if (!BurstCompiler.Options.EnableBurstCompilation)
                Debug.LogWarning(
                    "Burst compilation is not enabled and performance is expected to be degraded. Enable this at Preference > Jobs > Burst > Enable Compilation.");
        }
    }
#endif
}