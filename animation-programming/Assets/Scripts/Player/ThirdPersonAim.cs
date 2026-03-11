using UnityEngine;

public class ThirdPersonAim : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private Transform _spineAimTarget;
    [SerializeField] private float _spineAimDistance = 50f;
    [SerializeField] private Transform _armsAimTarget;
    [SerializeField] private float _armsAimDistance = 5f;
    [SerializeField] private Transform _shoulderPoint;

    [SerializeField] private LayerMask _aimMask;

    [SerializeField] private GameObject _ikTargetLeftArm;
    [SerializeField] private GameObject _ikTargetRighttArm;
    public float distance = 10f;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        MoveSpine(_spineAimDistance, _spineAimTarget);

    }

    private void LateUpdate()
    {
        MoveArm(_ikTargetLeftArm);
        MoveArm(_ikTargetRighttArm);
    }

    private void MoveArm(GameObject target)
    {

        Vector3 aimDir = _camera.transform.forward;
        Transform character = _shoulderPoint.parent;
        Vector3 localDir = character.InverseTransformDirection(aimDir);
        localDir.x = 0f;
        localDir.z = Mathf.Max(0.1f, localDir.z);
        localDir.y = Mathf.Clamp(localDir.y, -0.8f, 0.8f);
        Vector3 verticalAim = character.TransformDirection(localDir);
        target.transform.position = _shoulderPoint.position + verticalAim * distance;
    }

    private void MoveSpine(float aimDistance, Transform aimTarget)
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, aimDistance, _aimMask))
        {
            aimTarget.position = hit.point;
        }
        else
        {
            aimTarget.position = ray.GetPoint(aimDistance);
        }
    }

}
