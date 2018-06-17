using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chessboard : MonoBehaviour
{
    internal static Queue<Point> ClickResult { get; private set; }

    void Start()
    {
        ClickResult = new Queue<Point>();
    }

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0)&&!EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                int x, y;
                var p = transform.InverseTransformPoint(hit.point);
                x = 7 - (int)Math.Round(p.x / Const.UnitSize);
                y = 7 + (int)Math.Round(p.y / Const.UnitSize);
                ClickResult.Enqueue(new Point(x, y));
            }
        }
    }
}
