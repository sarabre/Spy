using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Collections;


public class EditorPersonalList : EditorWindow
{
    public static List<ListContent> PersonalLists = new List<ListContent>();
    public static ListContent NewList = new ListContent();


    static float MarginWindow = 10;
    static float HeightWindow = 600;
    static float WidthWindow = 700;
    static float Culomns1Width = 200;
    static float Culomns2Width = WidthWindow - (Culomns1Width + MarginWindow * 3);
    static float HeaderHeight = 70;
    static float FooterHeight = 200;
    static float BodyHeight = HeightWindow - ( HeaderHeight + FooterHeight);

    static float breadthButtonDelete = 15;
    static float breadthBoxWordTitle = 70;
    static float breadthBoxWord = 120 ;

    private int ActiveList;
    string NewWord;
    string NewListName;
    bool IsAddNewList;
    bool IsShowContent;

    Vector2 ScrollPos;
    Vector2 ScrollPosListContent;

    [MenuItem("Spy-Mobile/PersonalWordsEditor")]
    public static void ShowWindow()
    {
        ListPersonalWords ScriptableObjLoaded = Resources.Load<ListPersonalWords>("PersonalList-SO");
        if (ScriptableObjLoaded != null)
        {
            PersonalLists = ScriptableObjLoaded.WordsList;
        }

        EditorPersonalList WindowName = (EditorPersonalList)GetWindow(typeof(EditorPersonalList));
        Rect WinSize = new Rect(80, 80, WidthWindow, HeightWindow);
        WindowName.position = WinSize;
        WindowName.Show();
    }

    private void OnGUI()
    {
        DrawWindow();
    }

    void DrawWindow()
    {
        GUILayout.BeginArea(new Rect(MarginWindow, MarginWindow, Culomns1Width, HeightWindow - MarginWindow * 2));
        ScrollPos = GUILayout.BeginScrollView(ScrollPos);
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        EditorGUI.BeginDisabledGroup(false);

        DrawMenu();

        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();



        GUILayout.BeginArea(new Rect(Culomns1Width + MarginWindow * 2, MarginWindow, Culomns2Width, HeightWindow - MarginWindow * 2));
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        EditorGUI.BeginDisabledGroup(false);

        DrawContent();

        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();
    }

    void DrawMenu()
    {
        if (IsAddNewList)
            GUI.color = Color.cyan;

        if (GUILayout.Button("Add List", GUILayout.MinHeight(100)))
        {
            //ListTypeClassName item = new ListTypeClassName();
            //ListName.Add(item);
            IsAddNewList = true;
            IsShowContent = true;
        }

        GUI.color = Color.white;

        GUILayout.Space(10);


        for (int i = 0; i < PersonalLists.Count; i++)
        {
            if (ActiveList == i && !IsAddNewList && IsShowContent)
                GUI.color = Color.cyan;

            if (GUILayout.Button(PersonalLists[i].ListName.faConvert(), GUILayout.MinHeight(30)))
            {
                
                    IsAddNewList = false;
                    IsShowContent = true;
                    ActiveList = i;
                
            }

            GUI.color = Color.white;

            GUILayout.Space(5);

        }

    }

    void DrawContent()
    {
        if(IsShowContent)
            
            if (!IsAddNewList)
            {
                DrawContentHeader();
                DrawContentBody();
                DrawContentFooter();
            }
            else
            {
                DrawContentNewList();
            }
    }

    void DrawContentHeader()
    {
        GUILayout.BeginArea(new Rect(0, MarginWindow, Culomns2Width, HeaderHeight));
        GUILayout.Space(30);

        GUILayout.Label("Current List name :  "+ PersonalLists[ActiveList].ListName.faConvert(), EditorStyles.boldLabel);

        GUILayout.Space(20);
        GUILayout.EndArea();
    }

    void DrawContentBody()
    {
        GUILayout.BeginArea(new Rect(0,(HeaderHeight + MarginWindow), Culomns2Width, BodyHeight));
        
        ScrollPosListContent = GUILayout.BeginScrollView(ScrollPosListContent);

        
            for (int j = 0; j < PersonalLists[ActiveList].Words.Count; j++)
            {
                
                if (j % 2 == 0)
                    GUILayout.BeginHorizontal();
                
                GUILayout.BeginHorizontal();
                
                GUILayout.Label("Word  "+(j+1).ToString()+" :" , GUILayout.MaxWidth(breadthBoxWordTitle));
                GUILayout.Label(PersonalLists[ActiveList].Words[j].faConvert(),GUILayout.MaxWidth(breadthBoxWord));
                GUI.color = Color.red;
                if (GUILayout.Button("X", GUILayout.MaxWidth(breadthButtonDelete), GUILayout.MaxHeight(breadthButtonDelete)))
                {
                    PersonalLists[ActiveList].Words.RemoveAt(j);
                }
                GUI.color = Color.white;

                
                GUILayout.EndHorizontal();
                GUILayout.Space(20);

                if (j % 2 == 1)
                    GUILayout.EndHorizontal();
            }
        

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    void DrawContentFooter()
    {
        GUILayout.BeginArea(new Rect(0,(MarginWindow + BodyHeight + HeaderHeight),Culomns2Width,FooterHeight));
        GUILayout.Label("      New word :");
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        NewWord = GUILayout.TextField(NewWord, GUILayout.MaxWidth(300)) ;
        if(NewWord != null)
            GUILayout.Label(NewWord.faConvert());
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUI.color = Color.cyan;
        if (GUILayout.Button("Add",GUILayout.MaxHeight(30)))
        {
            PersonalLists[ActiveList].Words.Add(NewWord);
            NewWord = null;
        }
        GUI.color = Color.white;

        GUILayout.Space(10);
        GUI.color = Color.red;
        if (GUILayout.Button(" ! Delete this list ! ", GUILayout.MinHeight(50)))
        {
            PersonalLists.RemoveAt(ActiveList);
            IsShowContent = false;
        }
        GUI.color = Color.white;

        GUILayout.EndArea();
        


    }

    void DrawContentNewList()
    {
       
        GUILayout.BeginArea(new Rect(0,MarginWindow,Culomns2Width,(HeightWindow-MarginWindow*2)));
        GUILayout.Space(30);
        GUILayout.Label("New list name:  ");
        GUILayout.Space(10);
        
        GUILayout.BeginHorizontal();

        NewListName = GUILayout.TextField(NewListName,GUILayout.MaxWidth(300));
        if(NewListName != null)
            GUILayout.Label(NewListName.faConvert());
        GUILayout.EndHorizontal();

        GUI.color = Color.cyan;
        GUILayout.Space(10);
        if (GUILayout.Button("Create new list", GUILayout.MaxHeight(50)))
        {
            NewList.ListName = NewListName;
            PersonalLists.Add(NewList);
            NewList = null;
            NewListName = null;
        }
        GUI.color = Color.white;
        GUILayout.EndArea();
    }


}
