using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Vector2 _offset;
    [SerializeField] float _smoothSpeed;
    [SerializeField] float _leftMarg = -45f;
    [SerializeField] float _rightMarg = 141f;

    void LateUpdate()
    {
        if (_target)
        {
            Vector2 desiredPosition = new Vector2(_target.position.x, _target.position.y) + _offset;

            if (IsTargetWithinMap())
            {
                transform.position = new Vector3(desiredPosition.x, desiredPosition.y, transform.position.z);
            }
        }
    }


private bool IsTargetWithinMap()
    {
        return _target.position.x >= _leftMarg && _target.position.x <= _rightMarg;
    }

}