using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class GridGenerator : MonoBehaviour
{
    #region Variables
    public int gridSize; // Başlangıçta 5x5 bir grid oluşturuyoruz
    public GameObject cubePrefab; // Kullanacağımız küp prefabı
    public float spacing;
    public List<Transform> NodeTransformList;
    public CinemachineVirtualCamera VirtualCamera;
    public Transform[,] nodeTransformArray;

    public Transform BottomLeftNode;
    public Transform TopRightNode;
    #endregion

    #region Mono
    private void Start() {
        GenerateGrid();
    }
    void Update()
    {
        if (VirtualCamera != null && VirtualCamera.m_Lens.Aspect != Screen.width / (float)Screen.height)
        {
            AdjustCameraSize();
        }
    }
    #endregion

    #region Methods
    public void GenerateGrid()
    {

        ClearGrid();
         
        if (gridSize == 0) return;
        nodeTransformArray = new Transform[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                // Her nesne için x ve z koordinatlarına hafif bir ofset ekleyerek boşluk oluştur
                float xOffset = x * (cubePrefab.transform.localScale.x + spacing);
                float zOffset = y * (cubePrefab.transform.localScale.z + spacing);

                GameObject node = Instantiate(cubePrefab, new Vector3(xOffset, 0, zOffset), Quaternion.identity, transform);
                NodeTransformList.Add(node.transform);
                NodeController nodeController = node.GetComponent<NodeController>();
                nodeController.XPos = x;
                nodeController.YPos = y;
                nodeController.GridGenerator = this;
                nodeTransformArray[x, y] = node.transform;
                
            }
        }

        BottomLeftNode = NodeTransformList[0];
        TopRightNode = NodeTransformList[^1];

        AdjustCameraSize();
    }

    private void ClearGrid()
    {
        // Grid içindeki tüm çocukları temizle
        foreach (Transform grid in NodeTransformList)
        {
            DestroyImmediate(grid.gameObject);
        }
        NodeTransformList.Clear();
    }

    public void AdjustCameraSize()
    {

        float gridWidth = gridSize * (cubePrefab.transform.localScale.x + spacing);
        float gridHeight = gridSize * (cubePrefab.transform.localScale.x + spacing);


        float aspectRatio = (float)Screen.width / Screen.height;
        float targetAspectRatio = gridWidth / gridHeight;

        float targetOrthoSize;

        if (aspectRatio >= targetAspectRatio)
        {
            targetOrthoSize = (gridHeight / 2f) + spacing;
        }
        else
        {
            targetOrthoSize = (gridWidth / 2f) / aspectRatio + spacing;
        }

        VirtualCamera.m_Lens.OrthographicSize = targetOrthoSize;

        Vector3 center = (BottomLeftNode.position + TopRightNode.position) / 2f;
        VirtualCamera.transform.position = new Vector3(center.x, VirtualCamera.transform.position.y, center.z);
    }
    #endregion
}
