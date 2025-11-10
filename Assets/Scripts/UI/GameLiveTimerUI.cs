using UnityEngine;
using UnityEngine.UI;

public class GameLiveTimerUI : MonoBehaviour {

    [SerializeField] private Image _clockImage;

    private void Update() {
        _clockImage.fillAmount = GameManager.Instance.GetitsTimeToPlayNormalized();
    }
}