#if UNITY_2023_1_OR_NEWER
using System;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ChoyUtilities {
    
    public static partial class HelperCollection {
        
        private const string TASK_CANCELLATION_MESSAGE = "Task was cancelled";

        public static async Awaitable AwaitableUntil(this CancellationToken cancellationToken, Func<bool> condition) {
            while (!condition()) await Awaitable.NextFrameAsync(cancellationToken);
        }

        public static async Awaitable AwaitableUntil(this CancellationToken cancellationToken,
            Func<bool> condition,
            Action onWaitDone) {
            while (!condition()) {
                onWaitDone?.Invoke();
                await Awaitable.NextFrameAsync(cancellationToken);
            }
        }

        public static async Awaitable RotateObjectAsync(this CancellationToken token,
            GameObject obj, float3 rotateTo, float duration, EMotion motion = EMotion.Linear) {
            await token.RotateObjectAsync(obj.transform, rotateTo, duration, motion);
        }

        public static async Awaitable<bool> RotateObjectAsync(this CancellationToken token,
            Transform obj, float3 rotateTo, float duration, EMotion motion = EMotion.Linear) {
            try {
                if (obj is null) return false;

                var time = 0f;
                quaternion startRot = obj.transform.rotation;
                var endRot = quaternion.Euler(rotateTo);

                while (time < duration) {
                    time += Time.unscaledDeltaTime;
                    obj.transform.rotation = math.slerp(startRot, endRot, 
                        motion.Evaluate(time / duration));
                    await Awaitable.EndOfFrameAsync(token);
                }

                if (token.IsCancellationRequested) return false;
                obj.transform.rotation = endRot;
                return true;
            }
            catch {
                throw new Exception(TASK_CANCELLATION_MESSAGE);
            }
        }

        public static async Awaitable ScaleObjectAsync(this CancellationToken token,
            GameObject obj,
            Vector3 scaleTo,
            float scalingDuration,
            Action onDone = null) {
            try {
                if (obj is null) return;

                var time = 0f;
                var startScale = obj.transform.localScale;

                while (time <= scalingDuration) {
                    time += Time.unscaledDeltaTime;
                    obj.transform.localScale = Vector3.Lerp(startScale, scaleTo, time / scalingDuration);
                    await Awaitable.EndOfFrameAsync(token);
                }

                obj.transform.localScale = scaleTo;
                onDone?.Invoke();
            }
            catch {
                throw new Exception(TASK_CANCELLATION_MESSAGE);
            }
        }

        public static async Awaitable DialogueAsync(this CancellationToken token,
            List<string> dialogueList, float dialogueDuration,
            Action<string> displayTo, float timePerChar, Action onDone = null) {
            await token.DialogueAsync(dialogueList.ToArray(), 
                dialogueDuration, displayTo, timePerChar, onDone);
        }

        public static async Awaitable DialogueAsync(this CancellationToken token,
            string[] dialogueList, float dialogueDuration, Action<string> displayTo,
            float timePerChar = 0.05f, Action onDone = null) {
            try {
                if (dialogueList is null || dialogueList.Length == 0) return;

                foreach (var line in dialogueList) {
                    if (line == string.Empty) continue;

                    var timer = 0f;
                    var currentDisplaying = "";

                    while (currentDisplaying != line) {
                        timer += Time.unscaledDeltaTime;
                        var length = (int)math.ceil(timer / timePerChar);
                        length = math.clamp(length, 0, line.Length);
                        currentDisplaying = line[..length];
                        displayTo(currentDisplaying);

                        await Awaitable.NextFrameAsync(token);
                    }

                    await Awaitable.WaitForSecondsAsync(dialogueDuration, token);
                }

                displayTo("");
                onDone?.Invoke();
            }
            catch {
                throw new Exception(TASK_CANCELLATION_MESSAGE);
            }
        }

        public static async Awaitable RollRightAngleAsync(this CancellationToken token,
            Transform ob, float rollSpeed, float3 dir, float rollCooldown = 0.1f) {
            try {
                var anchor = (float3)ob.position + (new float3(0, -1f, 0) + dir) * 0.5f;
                var axis = math.cross(new float3(0, 1f, 0), dir);

                for (var i = 0; i <= 90 / rollSpeed; i++) {
                    ob.RotateAround(anchor, axis, i);
                    await Awaitable.WaitForSecondsAsync(rollCooldown, token);
                }
            }
            catch {
                throw new Exception(TASK_CANCELLATION_MESSAGE);
            }
        }

        public static async Awaitable TimeScaleAsync(this CancellationToken token,
            float targetScale, float loadDuration = 2f, EMotion motion = EMotion.Linear,
            Action onDone = null) {
            try {
                var currentScale = Time.timeScale;
                float unscaledTimer = 0;

                while (unscaledTimer <= loadDuration) {
                    unscaledTimer += Time.unscaledDeltaTime;
                    var t = math.lerp(currentScale, targetScale, motion.Evaluate(unscaledTimer / loadDuration));
                    Time.timeScale = t;
                    await Awaitable.EndOfFrameAsync(token);
                }
                onDone?.Invoke();
            }
            catch {
                throw new Exception(TASK_CANCELLATION_MESSAGE);
            }
        }

        #region Fade Screen Async

        public enum EFadeType : byte {
            FadeIn = 0,
            FadeOut = 1
        }

        public static async Awaitable FadeScreenAsync(this CancellationToken token,
            Image image, bool isFadein, float duration, EMotion motion = EMotion.Linear,
            Action onDone = null) {
            await token.FadeScreenAsync(image, isFadein ? EFadeType.FadeIn : EFadeType.FadeOut, 
                duration, Time.unscaledDeltaTime, motion, onDone);
        }

        public static async Awaitable<bool> FadeScreenAsync(this CancellationToken token,
            Image image, EFadeType fadeType, float duration, float dt, EMotion motion = EMotion.Linear,
            Action onDone = null) {
            try {
                if (image is null) return false;

                var targetAlpha = fadeType switch {
                    EFadeType.FadeOut => 0,
                    EFadeType.FadeIn => 1,
                    _ => image.color.a
                };

                var currentAlpha = image.color.a;
                float time = 0;

                while (time <= duration) {
                    time += dt;
                    var alpha = math.lerp(currentAlpha, targetAlpha, motion.Evaluate(time / duration));
                    image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                    await Awaitable.EndOfFrameAsync(token);
                }

                image.color = new Color(image.color.r, image.color.g, image.color.b, targetAlpha);
                onDone?.Invoke();

                return true;
            }
            catch {
                throw new Exception(TASK_CANCELLATION_MESSAGE);
            }
        }

        public static async Awaitable<bool> FadeScreenAsync(this CancellationToken token,
            CanvasGroup cg, EFadeType fadeType, float duration, float dt, EMotion motion = EMotion.Linear,
            Action onDone = null) {
            try {
                if (cg is null) return false;

                var targetAlpha = fadeType switch {
                    EFadeType.FadeOut => 0,
                    EFadeType.FadeIn => 1,
                    _ => cg.alpha
                };
                var currentAlpha = cg.alpha;
                var time = 0f;

                while (time <= duration) {
                    time += dt;
                    var alpha = math.lerp(currentAlpha, targetAlpha, motion.Evaluate(time / duration));
                    cg.alpha = alpha;
                    await Awaitable.EndOfFrameAsync(token);
                }

                cg.alpha = targetAlpha;
                onDone?.Invoke();

                return true;
            }
            catch {
                throw new Exception(TASK_CANCELLATION_MESSAGE);
            }
        }

        #endregion

        #region Change Color Async

        public static async Awaitable<Color> ChangeColorAsync(this CancellationToken token,
            float4 start, float4 target, float duration, EMotion motion = EMotion.Linear,
            Action onDone = null) {
            try {
                float time = 0;

                while (time <= duration) {
                    time += Time.unscaledDeltaTime;
                    start = math.lerp(start, target, motion.Evaluate(time / duration));
                    await Awaitable.EndOfFrameAsync(token);
                }
                onDone?.Invoke();
                return new Color(start.x, start.y, start.z, start.w);
            }
            catch {
                throw new Exception(TASK_CANCELLATION_MESSAGE);
            }
        }

        public static async Awaitable<Color> ChangeColorAsync(this CancellationToken token,
            Color start, Color target, float duration, EMotion motion = EMotion.Linear,
            Action onDone = null) {
            return await token.ChangeColorAsync(start, target, duration, Time.unscaledDeltaTime, motion, onDone);
        }

        public static async Awaitable<Color> ChangeColorAsync(this CancellationToken token,
            Color start, Color target, float duration, float dt, EMotion motion = EMotion.Linear,
            Action onDone = null) {
            try {
                float time = 0;
                var startColor = new float4(start.r, start.g, start.b, start.a);
                var targetColor = new float4(target.r, target.g, target.b, target.a);
                var newColor = new float4();

                while (time <= duration) {
                    time += dt;
                    newColor = math.lerp(startColor, targetColor, motion.Evaluate(time / duration));
                    await Awaitable.EndOfFrameAsync(token);
                }
                onDone?.Invoke();

                return new Color(newColor.x, newColor.y, newColor.z, newColor.w);
            }
            catch {
                throw new Exception(TASK_CANCELLATION_MESSAGE);
            }
        }

        #endregion

        #region Move Transform Async

        public static async Awaitable<bool> MoveAsync(this CancellationToken token,
            GameObject obj, Transform targetPos, float duration, EMotion motion = EMotion.Linear,
            Action onDone = null) {
            return await token.MoveAsync(obj, targetPos.position, duration, motion, onDone);
        }

        public static async Awaitable<bool> MoveAsync(this CancellationToken token,
            RectTransform obj, float3 target, float duration, EMotion motion = EMotion.Linear,
            Action onDone = null) {
            try {
                if (obj is null) return false;
                var time = 0f;
                var start = (float3)obj.anchoredPosition3D;

                while (time <= duration) {
                    time += Time.unscaledDeltaTime;
                    obj.anchoredPosition3D = math.lerp(start, target, motion.Evaluate(time / duration));
                    await Awaitable.NextFrameAsync(token);
                }

                if (token.IsCancellationRequested) return false;
                onDone?.Invoke();

                return true;
            }
            catch {
                throw new Exception(TASK_CANCELLATION_MESSAGE);
            }
        }

        public static async Awaitable<bool> MoveAsync(this CancellationToken token,
            GameObject obj, float3 targetPos, float duration, EMotion motion = EMotion.Linear,
            Action onDone = null) {
            try {
                if (obj is null) return false;

                float time = 0;
                var startPos = (float3)obj.transform.position;

                while (time <= duration) {
                    time += Time.unscaledDeltaTime;
                    var pos = math.lerp(startPos, targetPos, motion.Evaluate(time / duration));
                    obj.transform.position = pos;
                    await Awaitable.EndOfFrameAsync(token);
                }

                if (token.IsCancellationRequested) return false;
                obj.transform.position = targetPos;
                onDone?.Invoke();

                return true;
            }
            catch {
                throw new Exception(TASK_CANCELLATION_MESSAGE);
            }
        }

        #endregion
    }
}
#endif