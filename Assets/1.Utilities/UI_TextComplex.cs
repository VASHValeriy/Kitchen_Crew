/*
    ===========================================================
    🧭 CHEATSHEET: UI_TextComplex

    Цель: Класс для создания сложного текста в UI, который может содержать иконки, вставленные в текст. Позволяет комбинировать обычный текст и спрайты, автоматически расставлять позиции и получать размеры текста для центрирования или позиционирования.
    ===========================================================
*/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VashValeriy.Utilities {

    public class UI_TextComplex {
        private static Transform GetCanvasTransform() {
            return UtilsClass.GetCanvasTransform();
        }

        public struct Icon {
            public Sprite sprite;
            public Vector2 size;
            public Color color;
            public Icon(Sprite sprite, Vector2 size, Color? color = null) {
                this.sprite = sprite;
                this.size = size;
                if (color == null) {
                    this.color = Color.white;
                } else {
                    this.color = (Color)color;
                }
            }
        }

        public GameObject gameObject;
        private Transform transform;
        private RectTransform rectTransform;

        public UI_TextComplex(Transform parent, Vector2 anchoredPosition, int fontSize, char iconChar, string text, Icon[] iconArr, TMP_FontAsset font) {
            SetupParent(parent, anchoredPosition);
            string tmp = text;
            float textPosition = 0f;
            while (tmp.IndexOf(iconChar) != -1) {
                string untilTmp = tmp.Substring(0, tmp.IndexOf(iconChar));
                string iconNumber = tmp.Substring(tmp.IndexOf(iconChar) + 1);
                int indexOfSpaceAfterIconNumber = iconNumber.IndexOf(" ");
                if (indexOfSpaceAfterIconNumber != -1) {
                    // После iconNumber все еще есть место
                    iconNumber = iconNumber.Substring(0, indexOfSpaceAfterIconNumber);
                } else {
                    // После iconNumber больше нет пробела
                }
                tmp = tmp.Substring(tmp.IndexOf(iconChar + iconNumber) + (iconChar + iconNumber).Length);
                if (untilTmp.Trim() != "") {
                    TextMeshProUGUI uiText = UtilsClass.DrawTextUI(untilTmp, transform, new Vector2(textPosition, 0), fontSize, font);
                    textPosition += uiText.preferredWidth;
                }
                // Значок рисования
                int iconIndex = UtilsClass.Parse_Int(iconNumber, 0);
                Icon icon = iconArr[iconIndex];
                UtilsClass.DrawSprite(icon.sprite, transform, new Vector2(textPosition + icon.size.x / 2f, 0), icon.size);
                textPosition += icon.size.x;
            }
            if (tmp.Trim() != "") {
                UtilsClass.DrawTextUI(tmp, transform, new Vector2(textPosition, 0), fontSize, font);
            }
        }
        private void SetupParent(Transform parent, Vector2 anchoredPosition) {
            gameObject = new GameObject("UI_TextComplex", typeof(RectTransform));
            transform = gameObject.transform;
            rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.SetParent(parent, false);
            rectTransform.sizeDelta = new Vector2(0, 0);
            rectTransform.anchorMin = new Vector2(0, .5f);
            rectTransform.anchorMax = new Vector2(0, .5f);
            rectTransform.pivot = new Vector2(0, .5f);
            rectTransform.anchoredPosition = anchoredPosition;
        }
        public void SetTextColor(Color color) {
            foreach (Transform trans in transform) {
                TextMeshProUGUI text = trans.GetComponent<TextMeshProUGUI>();
                if (text != null) {
                    text.color = color;
                }
            }
        }
        public float GetTotalWidth() {
            float textPosition = 0f;
            foreach (Transform trans in transform) {
                TextMeshProUGUI text = trans.GetComponent<TextMeshProUGUI>();
                if (text != null) {
                    textPosition += text.preferredWidth;
                }
                Image image = trans.GetComponent<Image>();
                if (image != null) {
                    textPosition += image.GetComponent<RectTransform>().sizeDelta.x;
                }
            }
            return textPosition;
        }
        public float GetTotalHeight() {
            foreach (Transform trans in transform) {
                TextMeshProUGUI text = trans.GetComponent<TextMeshProUGUI>();
                if (text != null) {
                    return text.preferredHeight;
                }
            }
            return 0f;
        }
        public void AddTextOutline(Color color, float size) {
            foreach (Transform textComplexTrans in transform) {
                if (textComplexTrans.GetComponent<TextMeshProUGUI>() != null) {
                    Outline outline = textComplexTrans.gameObject.AddComponent<Outline>();
                    outline.effectColor = color;
                    outline.effectDistance = new Vector2(size, size);
                }
            }
        }
        public void SetAnchorMiddle() {
            rectTransform.anchorMin = new Vector2(.5f, .5f);
            rectTransform.anchorMax = new Vector2(.5f, .5f);
        }
        public void CenterOnPosition(Vector2 position) {
            rectTransform.anchoredPosition = position + new Vector2(-GetTotalWidth() / 2f, 0);
        }
        public void DestroySelf() {
            Object.Destroy(gameObject);
        }
    }
}
