using UnityEngine;

public class ToggleShaders : MonoBehaviour {


    [SerializeField] private Material _material;
    private const string PLAY_MODE_TIME = "_PlayModeTime";

    private void Update() {
        if (_material == null) return;

        if (Application.isPlaying) {
            _material.SetFloat(PLAY_MODE_TIME, Time.time);
        } else {
            _material.SetFloat(PLAY_MODE_TIME, 0f);
        }
    }
}