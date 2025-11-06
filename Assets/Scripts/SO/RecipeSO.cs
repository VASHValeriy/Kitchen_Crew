using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject {

    public List<KitchenObjectSO> kitchenObjectSOList;
    public string recipeName;

}