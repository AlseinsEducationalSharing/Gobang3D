using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chessboard : MonoBehaviour
{
    internal Resulter<(int x,int y)> Result { get; private set; } = new Resulter<(int x,int y)>();

    public void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
            {
                var p = transform.InverseTransformPoint(hit.point);
                var x = 7 - (int)Math.Round(p.x / Const.UnitSize);
                var y = 7 + (int)Math.Round(p.y / Const.UnitSize);

                Result.Result((x, y));
            }
        }
    }
}
