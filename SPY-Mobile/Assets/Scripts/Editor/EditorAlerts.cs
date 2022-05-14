
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorAlerts : EditorWindow
{
    public static List<Alert> PersonalLists = new List<Alert>();
    public static Alert NewList = new Alert();

    private static int WidthWindow = 600;
    private static int HeightWindow = 700;
    private static int MarginWindow = 20;
    private static int Section1Height = 400;
    private static int Section2Height = HeightWindow - Section1Height ;

    Vector2 ScrollPos;

    private string NewCode;


    [MenuItem("Spy-Mobile/Alerts")]
    public static void ShowWindow()
    {
        Alerts ScriptableObjLoaded = Resources.Load<Alerts>("Alerts-SO");
        if (ScriptableObjLoaded != null)
        {
            PersonalLists = ScriptableObjLoaded.AlertList;
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

    }

    void DrawMenu()
    {
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Code = ", EditorStyles.boldLabel);
        NewCode = GUILayout.TextField(NewCode, GUILayout.MinWidth(170),GUILayout.MinHeight(30));
        GUILayout.EndHorizontal();
    }
}
