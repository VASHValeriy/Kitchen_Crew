using UnityEngine;

public class PlayersFootstepSound : MonoBehaviour {

    private Player _player;
    private float _stepsTimer;
    private float _stepsTimerMax = 0.25f;

    private void Awake() {
        _player = GetComponent<Player>();
    }

    private void Update() {
        _stepsTimer -= Time.deltaTime;
        if (_stepsTimer < 0f) {
            _stepsTimer = _stepsTimerMax;

            if (_player.IsWalking()) {
                SoundManager.Instance.PlayStepsOfPlayers(_player.transform.position);
            }
        }
    }
}