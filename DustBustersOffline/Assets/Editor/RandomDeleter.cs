using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RandomDeleter : EditorWindow
{
    GameObject parentObj;

    [MenuItem("Tools/Delete")]
    static void DeleteObject()
    {
        EditorWindow window = GetWindow(typeof(RandomDeleter));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Select parent.");

        if (!Selection.activeGameObject)
        {
            return;
        }

        if (GUILayout.Button("Set Parent"))
        {
            parentObj = Selection.activeGameObject;
        }

        if (!parentObj)
        {
            return;
        }

        GUILayout.Label(parentObj.name);

        if (GUILayout.Button("Delete"))
        {
            Transform[] children = parentObj.GetComponentsInChildren<Transform>();
            Debug.Log("Deleting");
            for (int i = children.Length - 1; i >= 0; i--)
            {
                if (children[i].parent.gameObject != parentObj)
                {
                    continue;
                }
                    
                
                int r = Random.Range(0, 9);
                if (r == 0)
                {
                    DestroyImmediate(children[i].gameObject);
                }
            }
                
        }
    }
}
