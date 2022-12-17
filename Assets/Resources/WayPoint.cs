using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class WayPoint : EditorWindow
{
    [MenuItem("Editor/Waypoint")]
    static void ShowWindow()
    {
        GetWindow(typeof(WayPoint)).Show();
    }

    public GameObject NodeList = null;

    private void OnGUI()
    {
        // ** Windoes 창에 소켓을 생성하기 위해 공간을 미리 만들어둔다.
        SerializedObject Obj = new SerializedObject(this);

        GUILayout.BeginVertical();

        /* Obj 공간에 소켓을 생성

            [최소값 설정]
         * GUILayout.MinWidth(0);
         * GUILayout.MinHeight(0);
          
            [기본값 설정]
         * GUILayout.Width(0);
         * GUILayout.Height(0);
          
            [최대값 설정]
         * GUILayout.MaxWidth(0);
         * GUILayout.MaxHeight(0);
         */

        EditorGUILayout.PropertyField(Obj.FindProperty("NodeList"));

        if (NodeList != null && GUILayout.Button("Create " + NodeList.name,
            GUILayout.Width(300), GUILayout.Height(23),
            GUILayout.MinWidth(250), GUILayout.MinHeight(15),
            GUILayout.MaxWidth(400), GUILayout.MaxHeight(30)))
        {
            CreateNode();
        }

        GUILayout.EndVertical();

        // ** 필드에 적용(갱신) 
        Obj.ApplyModifiedProperties();
    }

    public void CreateNode()
    {
        GameObject Obj = new GameObject(NodeList.transform.childCount.ToString());
        Obj.transform.parent = NodeList.transform;

        Obj.AddComponent<Point>();
        
        int childCount = NodeList.transform.childCount;

        while (childCount > 1)
		{
            Obj.transform.position = new Vector3(
                Random.Range(NodeList.transform.position.x - 10.0f, NodeList.transform.position.x + 10.0f),
                0.0f,
                Random.Range(NodeList.transform.position.y - 10.0f, NodeList.transform.position.y + 10.0f));

            Point p1 = NodeList.transform.GetChild(childCount - 2).GetComponent<Point>();
            p1.Node = Obj.GetComponent<Point>();

            Point p2 = Obj.transform.GetComponent<Point>();
            p2.Node = NodeList.transform.GetChild(0).GetComponent<Point>();

            float Distance = Vector3.Distance(p1.transform.position, Obj.transform.position);

            if (Distance > 10.0f)
                break;
		}
    }
}

/*
    [MenuItem("Editor/Waypoint")]
    static void ShowWindow()
    {
        GetWindow(typeof(WayPoint)).Show();
    }

    public GameObject NodeList = null;

    private void OnGUI()
    {
        // ** Windoes 창에 소켓을 생성하기 위해 공간을 미리 만들어둔다.
        SerializedObject Obj = new SerializedObject(this);

        GUILayout.BeginVertical();

         Obj 공간에 소켓을 생성

            [최소값 설정]
         * GUILayout.MinWidth(0);
         * GUILayout.MinHeight(0);
          
            [기본값 설정]
         * GUILayout.Width(0);
         * GUILayout.Height(0);
          
            [최대값 설정]
         * GUILayout.MaxWidth(0);
         * GUILayout.MaxHeight(0);
         

    EditorGUILayout.PropertyField(Obj.FindProperty("NodeList"));

    if (NodeList != null && GUILayout.Button("Create " + NodeList.name,
        GUILayout.Width(300), GUILayout.Height(23),
        GUILayout.MinWidth(250), GUILayout.MinHeight(15),
        GUILayout.MaxWidth(400), GUILayout.MaxHeight(30)))
    {
        CreateNode();
    }

    GUILayout.EndVertical();

    // ** 필드에 적용(갱신) 
    Obj.ApplyModifiedProperties();
        }

        private void CreateNode()
    {
        GameObject Obj = new GameObject(NodeList.transform.childCount.ToString());
        Obj.transform.parent = NodeList.transform;

        Obj.AddComponent<Point>();

        int childCount = NodeList.transform.childCount;

        while (childCount > 1)
        {
            Obj.transform.position = new Vector3(
                Random.Range(NodeList.transform.position.x - 25.0f, NodeList.transform.position.x + 25.0f),
                0.0f,
                Random.Range(NodeList.transform.position.y - 25.0f, NodeList.transform.position.y + 25.0f));

            Point p1 = NodeList.transform.GetChild(childCount - 2).GetComponent<Point>();
            p1.Node = Obj.GetComponent<Point>();

            Point p2 = Obj.transform.GetComponent<Point>();
            p2.Node = NodeList.transform.GetChild(0).GetComponent<Point>();

            float Distance = Vector3.Distance(p1.transform.position, Obj.transform.position);

            if (Distance > 10.0f)
                break;
        }
    }
*/