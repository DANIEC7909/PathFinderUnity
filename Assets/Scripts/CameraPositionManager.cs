using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionManager : MonoBehaviour
{
    RaycastHit PositionRay;
    [SerializeField] LayerMask LayerToExclue;
    void Update()
    {
      
    }
    void ProbeCursorToWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out PositionRay,~LayerToExclue))
        {

        }
    }
}
