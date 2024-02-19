using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GridGenerator : MonoBehaviour
{
    public int gridSize; // Başlangıçta 5x5 bir grid oluşturuyoruz

    public GameObject cubePrefab; // Kullanacağımız küp prefabı
    public float spacing;
    public List<Transform> NodeTransformList;

    public CinemachineVirtualCamera VirtualCamera;
    public Transform _bottomLeftNode;
    public Transform _topRightNode;

    public Transform[,] nodeTransformArray;
    

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

    public void GenerateGrid()
    {
        // Önceki gridi temizle
        ClearGrid();
         
        if (gridSize == 0) return;
        nodeTransformArray = new Transform[gridSize, gridSize];
        // Yeni gridi oluştur
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                // Her nesne için x ve z koordinatlarına hafif bir ofset ekleyerek boşluk oluştur
                float xOffset = x * (cubePrefab.transform.localScale.x + spacing);
                float zOffset = y * (cubePrefab.transform.localScale.z + spacing);

                // Yeni nesneyi Instantiate ederken ofsetli pozisyonu kullan
                GameObject node = Instantiate(cubePrefab, new Vector3(xOffset, 0, zOffset), Quaternion.identity, transform);
                NodeTransformList.Add(node.transform);
                node.GetComponent<NodeController>().XPos = x;
                node.GetComponent<NodeController>().YPos = y;
                node.GetComponent<NodeController>().GridGenerator = this;
                nodeTransformArray[x, y] = node.transform;
                
            }
        }

        _bottomLeftNode = NodeTransformList[0];
        _topRightNode = NodeTransformList[^1];

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

        Vector3 center = (_bottomLeftNode.position + _topRightNode.position) / 2f;
        VirtualCamera.transform.position = new Vector3(center.x, VirtualCamera.transform.position.y, center.z);
    }
}
