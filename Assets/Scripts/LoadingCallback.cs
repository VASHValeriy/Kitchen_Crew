using UnityEngine;

public class LoadingCallback : MonoBehaviour {

    private bool _isFirstUpadte = true;

    private void Update() {
        if(_isFirstUpadte) {
            _isFirstUpadte = false;

            ScenesLoader.LoaderCallback();
        }

    }

}