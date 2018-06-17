using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chessboard : MonoBehaviour
{
    internal static Queue<Point> ClickResult { get; private set; } = new Queue<Point>();

    void Start()
    {
    }

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
            {
                var p = transform.InverseTransformPoint(hit.point);
                var x = 7 - (int)Math.Round(p.x / Const.UnitSize);
                var y = 7 + (int)Math.Round(p.y / Const.UnitSize);
                ClickResult.Enqueue(new Point(x, y));
            }
        }
    }
}
