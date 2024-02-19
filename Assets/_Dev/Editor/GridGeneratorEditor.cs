using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridGenerator))]
public class GridGeneratorEditor : Editor
{
    float previousAspectRatio;
    private void OnEnable()
    {
        EditorApplication.update += OnUpdate;
    }

    private void OnDisable()
    {
        EditorApplication.update -= OnUpdate;
    }

    private void OnUpdate()
    {

        EditorApplication.delayCall += () =>
        {
            GridGenerator gridGenerator = (GridGenerator)target;
            float currentAspectRatio = (float)Screen.width / Screen.height;
            if (currentAspectRatio != previousAspectRatio)
            {
                gridGenerator.AdjustCameraSize();
                previousAspectRatio = currentAspectRatio;
            }
        };


    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GridGenerator gridGenerator = (GridGenerator)target;

        if (GUI.changed)
        {
            gridGenerator.GenerateGrid();
        }
    }
}

