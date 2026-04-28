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

using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChoyUtilities {
    public static partial class HelperCollection {
        public static (float, float) GetBoundingBoxSize(this RectTransform rectTransform) {
            var rect = rectTransform.rect;
            var center = rect.center;

            var topLeftRel = new float2(rect.xMin - center.x, rect.yMin - center.y);
            var topRightRel = new float2(rect.xMax - center.x, rect.yMin - center.y);

            // Rotate in 2D using Z (RectTransform is effectively 2D around Z)
            var zRad = math.radians(rectTransform.localEulerAngles.z);
            var sin = math.sin(zRad);
            var cos = math.cos(zRad);

            var rotatedTopLeftRel = Rotate(topLeftRel);
            var rotatedTopRightRel = Rotate(topRightRel);

            var wMax = math.max(math.abs(rotatedTopLeftRel.x), math.abs(rotatedTopRightRel.x));
            var hMax = math.max(math.abs(rotatedTopLeftRel.y), math.abs(rotatedTopRightRel.y));

            return (2f * wMax, 2f * hMax);

            float2 Rotate(float2 rel) { return new float2(cos * rel.x - sin * rel.y, sin * rel.x + cos * rel.y); }
        }

        public static string GetLocaleCode(this ELanguage language) {
            return language switch {
                ELanguage.NotDefined => throw new ArgumentOutOfRangeException(nameof(language), language,
                    "Not defined language"),
                ELanguage.English => "en",
                ELanguage.SimplifiedChinese => "zh-Hans",
                ELanguage.TraditionalChinese => "zh-TW",
                ELanguage.Malay => "ms",
                _ => throw new ArgumentOutOfRangeException(nameof(language), language, "" +
                    "Not defined language")
            };
        }

        public static VisualElement GetVisualElement(this EVisualElements type) {
            return type switch {
                EVisualElements.Button => new Button(),
                EVisualElements.DoubleField => new DoubleField(),
                EVisualElements.DropdownField => new DropdownField(),
                EVisualElements.EnumField => new EnumField(),
                EVisualElements.FloatField => new FloatField(),
                EVisualElements.Foldout => new Foldout(),
                EVisualElements.IntegerField => new IntegerField(),
                EVisualElements.Label => new Label(),
                EVisualElements.ListView => new ListView(),
                EVisualElements.LongField => new LongField(),
                EVisualElements.MinMaxSlider => new MinMaxSlider(),
                EVisualElements.MultiColumnListView => new MultiColumnListView(),
                EVisualElements.MultiColumnTreeView => new MultiColumnTreeView(),
                EVisualElements.ProgressBar => new ProgressBar(),
                EVisualElements.RadioButton => new RadioButton(),
                EVisualElements.RadioButtonGroup => new RadioButtonGroup(),
                EVisualElements.RepeatButton => new RepeatButton(),
                EVisualElements.Slider => new Slider(),
                EVisualElements.SliderInt => new SliderInt(),
                EVisualElements.TemplateContainer => new TemplateContainer(),
                EVisualElements.TextElement => new TextElement(),
                EVisualElements.Toggle => new Toggle(),
                EVisualElements.TreeView => new TreeView(),
                EVisualElements.UnsignedLongField => new UnsignedLongField(),
                EVisualElements.Vector2Field => new Vector2Field(),
                EVisualElements.Vector2IntField => new Vector2IntField(),
                EVisualElements.Vector3Field => new Vector3Field(),
                EVisualElements.Vector3IntField => new Vector3IntField(),
                EVisualElements.Vector4Field => new Vector4Field(),
                _ => new VisualElement()
            };
        }
        
        public static VisualElement GetVisualElement(this EVisualElements type, VisualElement root, string name) {
            switch (type) {
                case EVisualElements.Button:
                    return root.Q<Button>(name);
                case EVisualElements.DoubleField:
                    return root.Q<DoubleField>(name);
                case EVisualElements.DropdownField:
                    return root.Q<DropdownField>(name);
                case EVisualElements.EnumField:
                    return root.Q<EnumField>(name);
                case EVisualElements.FloatField:
                    return root.Q<FloatField>(name);
                case EVisualElements.Foldout:
                    return root.Q<Foldout>(name);
                case EVisualElements.IntegerField:
                    return root.Q<IntegerField>(name);
                case EVisualElements.Label:
                    return root.Q<Label>(name);
                case EVisualElements.ListView:
                    return root.Q<ListView>(name);
                case EVisualElements.LongField:
                    return root.Q<LongField>(name);
                case EVisualElements.MinMaxSlider:
                    return root.Q<MinMaxSlider>(name);
                case EVisualElements.MultiColumnListView:
                    return root.Q<MultiColumnListView>(name);
                case EVisualElements.MultiColumnTreeView:
                    return root.Q<MultiColumnTreeView>(name);
                case EVisualElements.ProgressBar:
                    return root.Q<ProgressBar>(name);
                case EVisualElements.RadioButton:
                    return root.Q<RadioButton>(name);
                case EVisualElements.RadioButtonGroup:
                    return root.Q<RadioButtonGroup>(name);
                case EVisualElements.RepeatButton:
                    return root.Q<RepeatButton>(name);
                case EVisualElements.Slider:
                    return root.Q<Slider>(name);
                case EVisualElements.SliderInt:
                    return root.Q<TextField>(name);
                case EVisualElements.TemplateContainer:
                    return root.Q<TemplateContainer>(name);
                case EVisualElements.TextElement:
                    return root.Q<TextElement>(name);
                case EVisualElements.Toggle:
                    return root.Q<Toggle>(name);
                case EVisualElements.TreeView:
                    return root.Q<TreeView>(name);
                case EVisualElements.UnsignedLongField:
                    return root.Q<UnsignedLongField>(name);
                case EVisualElements.Vector2Field:
                    return root.Q<Vector2Field>(name);
                case EVisualElements.Vector2IntField:
                    return root.Q<Vector2IntField>(name);
                case EVisualElements.Vector3Field:
                    return root.Q<Vector3Field>(name);
                case EVisualElements.Vector3IntField:
                    return root.Q<Vector3IntField>(name);
                case EVisualElements.Vector4Field:
                    return root.Q<Vector4Field>(name);
                default:
                    return root.Q<VisualElement>(name);
            }
        }
    }
}