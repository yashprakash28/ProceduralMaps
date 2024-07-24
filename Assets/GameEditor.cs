using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(MapGenerator))]
public class GameEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapGenerator mainScript = (MapGenerator)target;
    }
}
