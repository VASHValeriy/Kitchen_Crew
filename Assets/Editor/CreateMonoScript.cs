// Assets/Editor/CreateMonoScript.cs

using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public static class CreateMonoScript {
    private const string DefaultScriptName = "NewMonoScript";

    [MenuItem("Tools/Create C# Script %#m")] // Ctrl + Shift + M
    private static void CreateScript() {
        string path = GetSelectedPathOrFallback();
        string fullPath = AssetDatabase.GenerateUniqueAssetPath($"{path}/{DefaultScriptName}.cs");

        // Просто запускаем переименование — Unity сам создаст файл потом
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            ScriptableObject.CreateInstance<DoCreateMonoScript>(),
            fullPath,
            EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D,
            null
        );
    }

    private static string GetSelectedPathOrFallback() {
        Object obj = Selection.activeObject;
        if (obj == null) return "Assets";

        string path = AssetDatabase.GetAssetPath(obj);
        if (string.IsNullOrEmpty(path)) return "Assets";

        return Directory.Exists(path) ? path : Path.GetDirectoryName(path);
    }
}

// Обработка после переименования
class DoCreateMonoScript : EndNameEditAction {
    public override void Action(int instanceId, string pathName, string resourceFile) {
        string className = Path.GetFileNameWithoutExtension(pathName);

        string scriptContent =
$@"using UnityEngine;

public class {className} : MonoBehaviour
{{
    void Start()
    {{

    }}

    void Update()
    {{

    }}
}}";

        File.WriteAllText(pathName, scriptContent);
        AssetDatabase.Refresh();

        Object asset = AssetDatabase.LoadAssetAtPath<Object>(pathName);
        ProjectWindowUtil.ShowCreatedAsset(asset);
    }
}
