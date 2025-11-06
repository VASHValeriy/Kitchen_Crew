/*
    ===========================================================
    🧭 CHEATSHEET: Assets

    Цель: Центр управления спрайтами
    ===========================================================
*/


using UnityEngine;

namespace VashValeriy.Utilities {

    public class Assets : MonoBehaviour {

        public static Assets i; // singleton

        [Header("Optional Sprites")]
        public Sprite s_White;
        public Sprite s_Black;
        public Sprite s_Red;
        public Sprite s_Green;
        public Sprite s_Blue;
        public Sprite s_Yellow;
        public Sprite s_Cyan;
        public Sprite s_Magenta;
        public Sprite s_Orange;
        public Sprite s_Gray;

        private void Awake() {
            if (i == null) {
                i = this;
            } else if (i != this) {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            // Автоматическое создание спрайтов, если не назначены
            if (s_White == null) s_White = CreateColoredSprite(Color.white);
            if (s_Black == null) s_Black = CreateColoredSprite(Color.black);
            if (s_Red == null) s_Red = CreateColoredSprite(Color.red);
            if (s_Green == null) s_Green = CreateColoredSprite(Color.green);
            if (s_Blue == null) s_Blue = CreateColoredSprite(Color.blue);
            if (s_Yellow == null) s_Yellow = CreateColoredSprite(Color.yellow);
            if (s_Cyan == null) s_Cyan = CreateColoredSprite(Color.cyan);
            if (s_Magenta == null) s_Magenta = CreateColoredSprite(Color.magenta);
            if (s_Orange == null) s_Orange = CreateColoredSprite(new Color(1f, 0.5f, 0f)); // Orange
            if (s_Gray == null) s_Gray = CreateColoredSprite(Color.gray);
        }

        private Sprite CreateColoredSprite(Color color) {
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, color);
            tex.Apply();
            return Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
        }
    }
}
