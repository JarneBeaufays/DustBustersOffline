using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ProceduralGenWindow : EditorWindow
{
    GameObject m_SpawnBox = null;

    string m_MinX = "0";
    string m_MinY = "0";
    string m_MinZ = "0";
    string m_MaxX = "0";
    string m_MaxY = "0";
    string m_MaxZ = "0";

    string m_MinRotX = "0";
    string m_MaxRotX = "0";
    string m_MinRotY = "0";
    string m_MaxRotY = "0";
    string m_MinRotZ = "0";
    string m_MaxRotZ = "0";

    string m_Amount = "1";

    [MenuItem("Tools/Spawn")]
    static void SpawnObject()
    {
        EditorWindow window = GetWindow(typeof(ProceduralGenWindow));
        window.Show();
    }

    private void OnGUI()
    {
        SetUpPositionFromBox();
        if (!m_SpawnBox)
        {
            return;
        }

        GUILayout.Label("Select the objects you would like to spawn");
        if (Selection.gameObjects.Length <= 0)
        {
            return;
        }

        string selectedObjectNames = GetSelectedObjectNames(Selection.gameObjects);
        GUILayout.Label("You have selected: " + selectedObjectNames);

        //SetUpPosition();
        SetUpRotation();
        SetUpAmount();

        if (GUILayout.Button("Spawn Object"))
        {
            SpawnObjects(Selection.gameObjects, int.Parse(m_Amount));
        }

        Repaint();
    }

    string GetSelectedObjectNames(GameObject[] selectedObjects)
    {
        string nameList = "";
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            nameList += selectedObjects[i].name;

            if (i < selectedObjects.Length - 1)
            {
                nameList += ", ";
            }
            else
            {
                nameList += ".";
            }
        }

        return nameList;
    }

    void SetUpPositionFromBox()
    {
        GUILayout.Label("Select SpawnBox.");

        if (!Selection.activeGameObject)
        {
            return;
        }

        if (GUILayout.Button("Set Spawn Box"))
        {
            m_SpawnBox = Selection.activeGameObject;
        }

        if (!m_SpawnBox)
        {
            return;
        }

        BoxCollider box = m_SpawnBox.GetComponent<BoxCollider>();
        if (!box)
        {
            m_SpawnBox = null;
            return;
        }

        m_MinX = box.bounds.min.x.ToString();
        m_MaxX = box.bounds.max.x.ToString();
        m_MinY = box.bounds.min.y.ToString();
        m_MaxY = box.bounds.max.y.ToString();
        m_MinZ = box.bounds.min.z.ToString();
        m_MaxZ = box.bounds.max.z.ToString();

        GUILayout.Label("Min X: " + m_MinX);
        GUILayout.Label("Max X: " + m_MaxX);
        GUILayout.Label("Min Y: " + m_MinY);
        GUILayout.Label("Max Y: " + m_MaxY);
        GUILayout.Label("Min Z: " + m_MinZ);
        GUILayout.Label("Max Z: " + m_MaxZ);
    }

    void SetUpPosition()
    {
        GUILayout.Label("Where do you want to spawn the objects?");

        m_MinX = EditorGUILayout.TextField("Min X", m_MinX);
        m_MaxX = EditorGUILayout.TextField("Max X", m_MaxX);

        m_MinY = EditorGUILayout.TextField("Min Y", m_MinY);
        m_MaxY = EditorGUILayout.TextField("Max Y", m_MaxY);

        m_MinZ = EditorGUILayout.TextField("Min Z", m_MinZ);
        m_MaxZ = EditorGUILayout.TextField("Max Z", m_MaxZ);
    }

    void SetUpRotation()
    {
        GUILayout.Label("Determine the rotation of the spawned objects");

        m_MinRotX = EditorGUILayout.TextField("Min X", m_MinRotX);
        m_MaxRotX = EditorGUILayout.TextField("Max X", m_MaxRotX);

        m_MinRotY = EditorGUILayout.TextField("Min Y", m_MinRotY);
        m_MaxRotY = EditorGUILayout.TextField("Max Y", m_MaxRotY);

        m_MinRotZ = EditorGUILayout.TextField("Min Z", m_MinRotZ);
        m_MaxRotZ = EditorGUILayout.TextField("Max Z", m_MaxRotZ);
    }

    void SetUpAmount()
    {
        GUILayout.Label("How many object do you want to spawn?");
        m_Amount = EditorGUILayout.TextField("Number of objects: ", m_Amount);
    }

    void SpawnObjects(GameObject[] selectedObjects, int amount)
    {
        GameObject parentObj = new GameObject("SpawnParentObject");

        for (int i = 0; i < amount; i++)
        {
            float posX = Random.Range(float.Parse(m_MinX), float.Parse(m_MaxX));
            float posY = Random.Range(float.Parse(m_MinY), float.Parse(m_MaxY));
            float posZ = Random.Range(float.Parse(m_MinZ), float.Parse(m_MaxZ));

            float rotX = Random.Range(float.Parse(m_MinRotX), float.Parse(m_MaxRotX));
            float rotY = Random.Range(float.Parse(m_MinRotY), float.Parse(m_MaxRotY));
            float rotZ = Random.Range(float.Parse(m_MinRotZ), float.Parse(m_MaxRotZ));

            int randomIndex = Random.Range(0, selectedObjects.Length);

            Ray ray = new Ray(new Vector3(posX, posY, posZ), Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                GameObject spawnedObj = Instantiate(selectedObjects[randomIndex], hit.point, Quaternion.Euler(rotX, rotY, rotZ), null);
                spawnedObj.transform.parent = parentObj.transform;
            }
        }
    }
}
