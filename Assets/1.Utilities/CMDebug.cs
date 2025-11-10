/*
    =========================================
    🧭 CHEATSHEET: CMDebug
    Назначение: быстрый дебаг и отладка в Unity.
    Особенности:
    - Создание кнопок в 2D/3D мире
    - Текстовые всплывающие подсказки
    - Функциональные обновления текста через FunctionUpdater
    =========================================

*/

using System;
using UnityEngine;
using VashValeriy.Utilities;

namespace VashValeriy {
    public static class CMDebug {
        public static UI_Sprite ButtonUI(Vector2 anchoredPosition, string text, Action ClickFunc) {
            return UI_Sprite.CreateDebugButton(anchoredPosition, text, ClickFunc);
        }

        public static void TextPopupMouse(string text) {
            UtilsClass.CreateWorldTextPopup(text, UtilsClass.GetMouseWorldPositionZeroZ());
        }

        public static void TextPopup(string text, Vector3 position) {
            UtilsClass.CreateWorldTextPopup(text, position);
        }

        public static FunctionUpdater TextUpdater(Func<string> GetTextFunc, Vector3 localPosition, Transform parent = null) {
            return UtilsClass.CreateWorldTextUpdater(GetTextFunc, localPosition, parent);
        }

        // Обновление текста в UI
        public static FunctionUpdater TextUpdaterUI(Func<string> GetTextFunc, Vector2 anchoredPosition) {
            return UtilsClass.CreateUITextUpdater(GetTextFunc, anchoredPosition);
        }

        // Текст следующий за мышкой
        public static void MouseTextUpdater(Func<string> GetTextFunc, Vector3 positionOffset) {
            GameObject gameObject = new GameObject();
            FunctionUpdater.Create(() => {
                gameObject.transform.position = UtilsClass.GetMouseWorldPositionZeroZ() + positionOffset;
                return false;
            });
            TextUpdater(GetTextFunc, Vector3.zero, gameObject.transform);
        }

    }

}