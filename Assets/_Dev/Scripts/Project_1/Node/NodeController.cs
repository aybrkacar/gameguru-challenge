using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Project1.General;
using System.Linq;
using UnityEditor;

public class NodeController : MonoBehaviour
{
    public GridGenerator GridGenerator;
    public bool Selected;
    public List<Transform> SelectedNeighbourList = new List<Transform>();
    public int XPos;
    public int YPos;

    Color _originalColor;

    private void Start() {
        _originalColor = GetComponent<Renderer>().material.color;
    }

    public void SelectNode()
    {
        if (DOTween.IsTweening(GetComponent<Renderer>().material) || Selected) return;
        Selected = true;
        GetComponent<Renderer>().material.DOColor(Color.white, 0.15f);

        CheckNeighbour();
    }

    public void ReturnColor(){
        GetComponent<Renderer>().material.DOColor(_originalColor, 0.2f);
    }

    public void Scale(){
        transform.DOPunchScale(Vector3.one * 0.1f, 0.2f).OnComplete(()=>{
            ReturnColor();
        });
    }

    void CheckNeighbour()
    {
        Vector2[] neighbourPosArray = new Vector2[]{
            new(XPos, YPos + 1),
            new(XPos, YPos - 1),
            new(XPos + 1, YPos),
            new(XPos - 1, YPos)
        };

        int gridSize = GridGenerator.gridSize;
        for (int i = 0; i < neighbourPosArray.Length; i++)
        {
            if(neighbourPosArray[i].x < 0 || neighbourPosArray[i].y < 0 ||
                 neighbourPosArray[i].x > gridSize - 1 || neighbourPosArray[i].y > gridSize - 1 ) continue;
            NodeController node = GridGenerator.nodeTransformArray
                                        [(int)neighbourPosArray[i].x,(int)neighbourPosArray[i].y]
                                        .GetComponent<NodeController>();

            if(node.Selected){
                SelectedNeighbourList.Add(node.transform);
                node.SelectedNeighbourList.Add(transform);
                SelectedNeighbourList = SelectedNeighbourList.Concat(node.SelectedNeighbourList).ToList();
            }
        }

        Bingo();
    }

    void Bingo(){
        var list = RemoveDuplicates(SelectedNeighbourList);
        if(list.Count >= 3){
            Debug.Log("Buyah");
            //SelectedNeighbourList.Add(transform);
            foreach(Transform node in list){
                var nodeController = node.GetComponent<NodeController>();
                nodeController.SelectedNeighbourList.Clear();
                nodeController.Selected = false;
                nodeController.Scale();
            }
        }
    }

    public static List<T> RemoveDuplicates<T>(List<T> list)
    {
        List<T> uniqueList = new();

        foreach (T item in list)
        {
            if (!uniqueList.Contains(item))
            {
                uniqueList.Add(item);
            }
        }

        return uniqueList;
    }
}
