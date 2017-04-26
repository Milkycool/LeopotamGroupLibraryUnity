// ----------------------------------------------------------------------------
// The MIT License
// LeopotamGroupLibrary https://github.com/Leopotam/LeopotamGroupLibraryUnity
// Copyright (c) 2012-2017 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using LeopotamGroup.Common;
using LeopotamGroup.Math;
using LeopotamGroup.Serialization;
using LeopotamGroup.SystemUi.Localization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LeopotamGroup.SystemUi.Markup.Generators {
    static class TextNode {
        static readonly int HashedFontName = "fontName".GetStableHashCode ();

        static readonly int HashedFontSize = "fontSize".GetStableHashCode ();

        static readonly int HashedFontStyle = "fontStyle".GetStableHashCode ();

        static readonly int HashedLocalize = "localize".GetStableHashCode ();

        /// <summary>
        /// Create "text" node. If children supported - GameObject container for them should be returned.
        /// </summary>
        /// <param name="widget">Ui widget.</param>
        /// <param name="node">Xml node.</param>
        /// <param name="container">Markup container.</param>
        public static RectTransform Create (RectTransform widget, XmlNode node, MarkupContainer container) {
#if UNITY_EDITOR
            widget.name = "text";
#endif
            var txt = widget.gameObject.AddComponent<Text> ();
            string attrValue;
            string font = null;
            var align = TextAnchor.MiddleCenter;
            var color = Color.black;
            var style = FontStyle.Normal;

            attrValue = node.GetAttribute (HashedFontName);
            if (!string.IsNullOrEmpty (attrValue)) {
                font = attrValue;
            }

            attrValue = node.GetAttribute (HashedFontSize);
            if (!string.IsNullOrEmpty (attrValue)) {
                int fontSize;
                if (int.TryParse (attrValue, out fontSize)) {
                    txt.fontSize = fontSize;
                }
            }

            attrValue = node.GetAttribute (HashedFontStyle);
            if (!string.IsNullOrEmpty (attrValue)) {
                var parts = MarkupUtils.SplitAttrValue (attrValue);
                for (var i = 0; i < parts.Length; i++) {
                    switch (parts[i]) {
                        case "bold":
                            style |= FontStyle.Bold;
                            break;
                        case "italic":
                            style |= FontStyle.Italic;
                            break;
                    }
                }
            }

            attrValue = node.GetAttribute (HashedLocalize);
            if (!string.IsNullOrEmpty (attrValue)) {
                widget.gameObject.AddComponent<TextLocalization> ().SetToken (attrValue);
            } else {
                txt.text = node.Value;
            }

            txt.alignment = align;
            txt.font = container.GetFont (font);
            txt.color = color;
            txt.fontStyle = style;

            MarkupUtils.SetColor (txt, node);
            MarkupUtils.SetSize (widget, node);
            MarkupUtils.SetRotation (widget, node);
            MarkupUtils.SetOffset (widget, node);
            MarkupUtils.SetMask (widget, node);
            MarkupUtils.SetMask2D (widget, node);
            MarkupUtils.SetHidden (widget, node);
            txt.raycastTarget = MarkupUtils.ValidateInteractive (widget, node);

            return widget;
        }
    }
}