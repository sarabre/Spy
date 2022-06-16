
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorAlerts : EditorWindow
{
    public static List<Alert> AlertList = new List<Alert>();
    public static Alert NewAlert = new Alert();

    private static int WidthWindow = 600;
    private static int HeightWindow = 700;
    private static int MarginWindow = 20;
    private static int Section1Height = 470;
    private static int Section2Height = HeightWindow - Section1Height ;

    Vector2 ScrollPos;

    private string NewCode;
    private string NewMessage;


    [MenuItem("Spy-Mobile/Alerts")]
    public static void ShowWindow()
    {
        Alerts ScriptableObjLoaded = Resources.Load<Alerts>("Alerts-SO");
        if (ScriptableObjLoaded != null)
        {
            AlertList = ScriptableObjLoaded.AlertList;
        }

        EditorAlerts WindowName = (EditorAlerts)GetWindow(typeof(EditorAlerts));
        Rect WinSize = new Rect(80, 80, WidthWindow, HeightWindow);
        WindowName.position = WinSize;
        WindowName.Show();
    }

    private void OnGUI()
    {
        DrawWindow();
    }

    public void DrawWindow()
    {
        GUILayout.BeginArea(new Rect(MarginWindow, MarginWindow, WidthWindow - MarginWindow *2 , Section1Height - MarginWindow * 2));
        ScrollPos = GUILayout.BeginScrollView(ScrollPos);
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        EditorGUI.BeginDisabledGroup(false);

        DrawAlert();

        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();



        GUILayout.BeginArea(new Rect(MarginWindow, Section1Height , WidthWindow - MarginWindow*2,Section2Height - MarginWindow*2));
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        EditorGUI.BeginDisabledGroup(false);

        DrawMenu();

        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();
    }

    void DrawAlert()
    {
        for (int i = 0; i < AlertList.Count; i++)
        {
            DrawAlertInfo(i);
        }
    }

    void DrawAlertInfo(int index)
    {
        GUILayout.Label("-----------------------------------------------------------------------------------------");
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        GUILayout.Label("Code = " + AlertList[index].code);
        GUILayout.Label("Massage = " + AlertList[index].massage.faConvert());
        GUILayout.EndVertical();
        GUI.color = Color.red;
        if (GUILayout.Button("X",GUILayout.MaxWidth(50) ,GUILayout.MinHeight(50)))
        {
            AlertList.RemoveAt(index);
        }
        GUI.color = Color.white;
        GUILayout.EndHorizontal();

    }
    void StartHorizontalSegmentation()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
    }
    void EndHorizontalSegmentation()
    {
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    void DrawMenu()
    {
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        GUILayout.Space(5);
        GUILayout.Label("    Code = ", EditorStyles.boldLabel);
        GUILayout.EndVertical();
        NewCode = GUILayout.TextField(NewCode, GUILayout.MinWidth(100),GUILayout.MinHeight(30));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUILayout.Label("    message =     ");

        StartHorizontalSegmentation();
        NewMessage = GUILayout.TextField(NewMessage, GUILayout.MaxWidth(520), GUILayout.MinHeight(30));
        EndHorizontalSegmentation();

        GUILayout.Space(10);
        StartHorizontalSegmentation();
        if (NewMessage != null)
            GUILayout.Label(NewMessage.faConvert());
        else 
            GUILayout.Label("Message preview");
        EndHorizontalSegmentation();

        GUILayout.Space(10);
        StartHorizontalSegmentation();
        
        if (GUILayout.Button("Add to List",GUILayout.MaxWidth(520), GUILayout.MinHeight(30)))
        {
            NewAlert.code = int.Parse(NewCode);
            NewAlert.massage = NewMessage;
            Debug.Log("added");
            AlertList.Add(NewAlert);
            NewCode = "";
            NewMessage = null;
        }
        
        EndHorizontalSegmentation();
    }


}
