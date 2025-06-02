using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GridDrawer : MonoBehaviour
{
    public int gridSize;
    public float cellSize;
    public Material lineMaterial;
    public Color lineColor = Color.white;

    private List<GameObject> _gridList = new List<GameObject>();





    private void Awake()
    {
        for (int i = 0; i <= gridSize; i++)
        {
            float offset = i * cellSize - (gridSize * cellSize / 2);
            DrawLine(new Vector3(-gridSize * cellSize / 2, 0, offset), new Vector3(gridSize * cellSize / 2, 0, offset)); // Z선
            DrawLine(new Vector3(offset, 0, -gridSize * cellSize / 2), new Vector3(offset, 0, gridSize * cellSize / 2)); // X선
        }
    }





    // 웹 제어용 그리드 On/Off 메서드
    public void GridOnOff(string msg)
    {
        ChangeGridState(msg == "on");
    }





    // Grid 라인 생성 메서드
    private void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject lineObj = new GameObject("GridLine");
        lineObj.transform.parent = this.transform;

        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.startWidth = 0.005f;
        lr.endWidth = 0.005f;
        lr.material = lineMaterial;
        lr.useWorldSpace = true;

        _gridList.Add(lineObj);
    }


    // 해당 오브젝트 내부에 있는 그리드 오브젝트를 On/Off
    private void ChangeGridState(bool state)
    {
        foreach (GameObject grid in _gridList)
        {
            grid.GetComponent<LineRenderer>().enabled = state;
        }
    }
}
