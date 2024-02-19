/* #if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Cinemachine;

[CustomEditor(typeof(GridGenerator))]
public class CameraAspectAutoAdjust : Editor
{
    public override void OnInspectorGUI()
    {
        // Virtual Camera bileşenini al
        GridGenerator gridGenerator = (GridGenerator)target;

        // Eğer aspect oranı değişirse, kameranın boyutunu ayarla
        if (Event.current.type == EventType.Layout)
        {
            Debug.Log("girdi");
            gridGenerator.AdjustCameraSize();
        }
    }
}
#endif */
