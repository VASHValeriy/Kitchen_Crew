using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResutlUI : MonoBehaviour {

    private const string RECIPE_RESULT = "RecipeResult";

    [SerializeField] private Image _bgResultUI;
    [SerializeField] private Image _iconResultImage;
    [SerializeField] private TextMeshProUGUI _resultTextPro;
    [SerializeField] private Color _resultRecipeSuccessColor;
    [SerializeField] private Color _resultRecipeFailedColor;
    [SerializeField] private Sprite _resultRecipeSuccessSprite;
    [SerializeField] private Sprite _resultRecipeFailedSprite;

    private Animator _animatorRecipeDeliveryUI;

    private void Awake() {
        _animatorRecipeDeliveryUI = GetComponent<Animator>();
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        gameObject.SetActive(false);
    }
    private void OnDestroy() {
        DeliveryManager.Instance.OnRecipeSuccess -= DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed -= DeliveryManager_OnRecipeFailed;
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e) {
        gameObject.SetActive(true);

        _animatorRecipeDeliveryUI.SetTrigger(RECIPE_RESULT);

        _bgResultUI.color = _resultRecipeFailedColor;
        _iconResultImage.sprite = _resultRecipeFailedSprite;
        _resultTextPro.text = "DELIVERY\nFAILED";
    }

    

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
        gameObject.SetActive(true);

        _animatorRecipeDeliveryUI.SetTrigger(RECIPE_RESULT);

        _bgResultUI.color = _resultRecipeSuccessColor;
        _iconResultImage.sprite = _resultRecipeSuccessSprite;
        _resultTextPro.text = "DELIVERY\nSUCCESS";
    }
}