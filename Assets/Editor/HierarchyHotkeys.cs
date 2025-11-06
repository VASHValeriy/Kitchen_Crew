using UnityEditor;
using UnityEngine;

public class HierarchyHotkeys{
    // Ctrl + [
    [MenuItem("Tools/Hierarchy/Move Up %o")]
    private static void MoveUp() {
        foreach (GameObject obj in Selection.gameObjects) {
            Transform t = obj.transform;
            int index = t.GetSiblingIndex();
            if (index > 0)
                t.SetSiblingIndex(index - 1);
        }
    }

    // Ctrl + ]
    [MenuItem("Tools/Hierarchy/Move Down %p")]
    private static void MoveDown() {
        foreach (GameObject obj in Selection.gameObjects) {
            Transform t = obj.transform;
            int index = t.GetSiblingIndex();
            int siblingCount;

            if (t.parent != null) {
                siblingCount = t.parent.childCount;
            } else {
                // количество root объектов в сцене
                siblingCount = t.gameObject.scene.GetRootGameObjects().Length;
            }

            if (index < siblingCount - 1)
                t.SetSiblingIndex(index + 1);
        }
    }
    
}
