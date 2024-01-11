using UnityEngine;
public class CameraPositionManager : MonoBehaviour
{
    RaycastHit PositionRay;
    Camera CurrentCamera;
    [SerializeField] LayerMask LayerToExclue;
    private void Awake()
    {
        CurrentCamera = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ProbeCursorToWorldPosition();
        }
    }
    void ProbeCursorToWorldPosition()
    {
        Ray ray = CurrentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out PositionRay, LayerToExclue))
        {
            GameController.Instance.PrimaryHero.Move(PositionRay.point);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(PositionRay.point, 2);
    }
}
