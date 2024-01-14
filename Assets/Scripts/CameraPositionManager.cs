using UnityEngine;
public class CameraPositionManager : MonoBehaviour
{
    RaycastHit PositionRay;
    Camera CurrentCamera;
   
    private void Awake()
    {
        CurrentCamera = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ProbeCursorToWorldPosition();
        }
    }
    void ProbeCursorToWorldPosition()
    {
        Ray ray = CurrentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out PositionRay))
        {
            if (PositionRay.collider.gameObject.layer != LayerMask.NameToLayer("Obstacle"))
            {
                GameController.Instance.PrimaryHero.Move(PositionRay.point);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(PositionRay.point, 2);
    }
}
