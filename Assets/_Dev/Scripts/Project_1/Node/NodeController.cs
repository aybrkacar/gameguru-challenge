using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using Project1.General;

public class NodeController : MonoBehaviour
{
    #region Variables
    public GridGenerator GridGenerator;
    public bool Selected;
    public List<Transform> SelectedNeighbourList = new List<Transform>();
    public int XPos;
    public int YPos;

    [SerializeField] private GameObject _xMarker;
    private Color _originalColor;
    #endregion

    #region Mono
    private void Start()
    {
        _originalColor = GetComponent<Renderer>().material.color;
    }
    #endregion

    #region  Methods
    public void SelectNode()
    {
        if (DOTween.IsTweening(GetComponent<Renderer>().material) || Selected) return;
        Selected = true;
        GetComponent<Renderer>().material.DOColor(Color.white, 0.15f);
        _xMarker.SetActive(true);
        CheckNeighbour();
    }

    public void ReturnColor()
    {
        GetComponent<Renderer>().material.DOColor(_originalColor, 0.2f);
    }

    private void Feedback()
    {
        transform.DOPunchScale(Vector3.one * 0.1f, 0.2f).OnComplete(() =>
        {
            _xMarker.SetActive(false);
            ReturnColor();
        });
    }

    //Main search function -> Search all neighbour node for selected node and add to list if they selected.
    private void CheckNeighbour()
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
            if (neighbourPosArray[i].x < 0 || neighbourPosArray[i].y < 0 ||
                 neighbourPosArray[i].x > gridSize - 1 || neighbourPosArray[i].y > gridSize - 1) continue;
            NodeController node = GridGenerator.nodeTransformArray
                                        [(int)neighbourPosArray[i].x, (int)neighbourPosArray[i].y]
                                        .GetComponent<NodeController>();

            if (node.Selected)
            {
                SelectedNeighbourList.Add(node.transform);
                node.SelectedNeighbourList.Add(transform);
                SelectedNeighbourList = SelectedNeighbourList.Concat(node.SelectedNeighbourList).ToList();
            }
        }

        CheckMatch();
    }

    //Seek & Destroy function -> works when unique node selected neighbour list count bigger than 2.
    private void CheckMatch()
    {
        var list = RemoveDuplicates(SelectedNeighbourList);

        if (list.Count >= 3)
        {
            foreach (Transform node in list)
            {
                NodeRestart(node);
            }

            GameManager.Instance.OnMatching();
        }
    }

    private void NodeRestart(Transform node)
    {
        var nodeController = node.GetComponent<NodeController>();
        nodeController.SelectedNeighbourList.Clear();
        nodeController.Selected = false;
        nodeController.Feedback();
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

    #endregion
}
