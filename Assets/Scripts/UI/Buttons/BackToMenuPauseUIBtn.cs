using UnityEngine;

public class BackToMenuPauseUIBtn : BaseCloseUIBtn {

    protected override void SwitchObjects() {
        Time.timeScale = 1f;
        GameManager.Instance.ReturnToMenu();
        ScenesLoader.LoadScene(ScenesLoader.Scenes.MainMenuScene);
    }

}