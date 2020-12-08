using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LoadMinigame))]
public class StringDropdownEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LoadMinigame script = (LoadMinigame)target;

        GUIContent arrayLabel = new GUIContent("SceneName");
        script.arrayIdx = EditorGUILayout.Popup(arrayLabel, script.arrayIdx, script.SceneNames);
    }
}
