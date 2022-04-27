/*using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Editor : EditorWindow
{
    public static List<ListTypeClassName> ListName = new List<ListTypeClassName>();


    [MenuItem("TabName/OptionName")]
    public static void ShowWindow()
    {
        ScriptableObjectClassName ScriptableObjLoaded = Resources.Load<ScriptableObjectClassName>("AssetDefaultName");
        if (ScriptableObjLoaded != null)
        {
            ListName = ScriptableObjLoaded.FieldName;
        }

        EditorName WindowName = (EditorName)GetWindow(typeof(EditorName));
        Rect WinSize = new Rect(300, 50, 800, 500); //position in screen and size of window
        WindowName.position = WinSize;
        WindowName.Show();
    }
}
*/
