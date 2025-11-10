using UnityEngine;
using System.Collections.Generic;
using System;

public class DeliveryManager : MonoBehaviour {

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO _recipeListSO;

    private List<RecipeSO> _waitingRecipeSOList;

    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipeMax = 4;
    private int _successRecipeEndedAmount;

    private void Awake() {
        Instance = this;
        _waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        _spawnRecipeTimer -= Time.deltaTime;
        if (_spawnRecipeTimer <= 0f) {
            _spawnRecipeTimer = _spawnRecipeTimerMax;

            if (_waitingRecipeSOList.Count < _waitingRecipeMax) {
                RecipeSO waitingRecipeSo = _recipeListSO.recipeSOList[UnityEngine.Random.Range(0, _recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSo.recipeName);
                _waitingRecipeSOList.Add(waitingRecipeSo);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject) {

        for (int i = 0; i < _waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = _waitingRecipeSOList[i];
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // Одинаковое кол-во ингридиентов
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKtichenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    // Проходим циклом по всем ингридиентам в Рецепте
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        // Проходим циклом по всем ингридиентам на Тарелке
                        if (plateKitchenObjectSO == recipeKtichenObjectSO) {
                            // Все ингридиенты совпали
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        // Не найден какой-то ингридиент на Тарелке
                        plateContentsMatchesRecipe = false;
                    }
                }
                if (plateContentsMatchesRecipe) {

                    _successRecipeEndedAmount++;

                    // Игрок доставил правильный рецепт
                    _waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        // Совпадений не найдено
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return _waitingRecipeSOList;
    }

    public int GetSuccessRecipeEndedAmount() {
        return _successRecipeEndedAmount;
    }

}