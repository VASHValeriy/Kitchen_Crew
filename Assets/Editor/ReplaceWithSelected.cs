using UnityEngine;
using UnityEditor;

public class ReplaceWithCutObject : EditorWindow {
    private static GameObject _cutObject;

    // Вырезать объект (запомнить и удалить)
    [MenuItem("Edit/Custom Cut Object %#x")]
    static void CustomCut() {
        if (Selection.activeGameObject == null) {
            Debug.LogWarning("Нет выбранного объекта для вырезания!");
            return;
        }

        _cutObject = Selection.activeGameObject;
        Debug.Log($"Вырезан объект: {_cutObject.name}");
    }

    // Заменить выбранный объект тем, что вырезан (Ctrl+Shift+Alt+P)
    [MenuItem("Tools/Replace With Cut Object %&#p")]
    static void ReplaceWithCut() {
        if (_cutObject == null) {
            Debug.LogWarning("Нет вырезанного объекта! Сначала вырежи (Ctrl+Shift+X).");
            return;
        }

        if (Selection.activeGameObject == null) {
            Debug.LogWarning("Выбери объект, который нужно заменить!");
            return;
        }

        GameObject target = Selection.activeGameObject;

        Undo.RecordObject(_cutObject.transform, "Replace With Cut Object");
        Undo.RecordObject(target.transform, "Replace With Cut Object");

        // Переместить вырезанный объект на место целевого
        _cutObject.transform.SetParent(target.transform.parent);
        _cutObject.transform.SetPositionAndRotation(target.transform.position, target.transform.rotation);
        _cutObject.transform.localScale = target.transform.localScale;

        // Удалить заменяемый объект
        Undo.DestroyObjectImmediate(target);

        // Выделить вставленный объект
        Selection.activeGameObject = _cutObject;

        Debug.Log($"Объект {target.name} заменён объектом {_cutObject.name}");

        _cutObject = null; // очищаем буфер, чтобы не вставить снова случайно
    }
}
