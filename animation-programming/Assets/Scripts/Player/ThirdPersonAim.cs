using UnityEngine;

public class ThirdPersonAim : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _aimTarget;
    [SerializeField] private float _aimDistance = 50f;
    [SerializeField] private LayerMask _aimMask;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if(Physics.Raycast(ray, out RaycastHit hit, _aimDistance, _aimMask))
        {
            _aimTarget.position = hit.point;
        } else
        {
            _aimTarget.position = ray.GetPoint(_aimDistance);
        }
    }

}
