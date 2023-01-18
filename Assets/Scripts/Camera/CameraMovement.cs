using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Range(0f, 30)] [SerializeField] float CameraMovementSpeed;
    [Range(0f, 30)] [SerializeField] float CameraZoomSpeed;
    [Header("Movement Limits")]
    [SerializeField] GameObject minCameraLimitPoint;
    [SerializeField] GameObject maxCameraLimitPoint;
    [Range(-30f, 0f)] [SerializeField] float minCameraYCoord;
    [Range(0f, 30f)] [SerializeField] float maxCameraYCoord;

    private float _minYCoord, _maxYCoord;
    private Vector3 _minCameraLimitPointPos, _maxCameraLimitPointPos;

    void Start()
    {
        _minYCoord = transform.position.y + minCameraYCoord;
        _maxYCoord = transform.position.y + maxCameraYCoord;
        _minCameraLimitPointPos = minCameraLimitPoint.transform.position;
        _maxCameraLimitPointPos = maxCameraLimitPoint.transform.position;
    }

    void Update()
    {
        SetNewPosition();
    }

    void SetNewPosition()
    {
        Vector3 fromKeyboard = CameraMovementWithKeyboard();
        Vector3 fromMouse = CameraZoomWithMouseScrollWheel();

        transform.position = new Vector3(fromKeyboard.x, fromMouse.y, fromKeyboard.z);
    }

    Vector3 CameraMovementWithKeyboard()
    {
        Vector3 movementDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movementDirection += transform.up;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movementDirection -= transform.up;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movementDirection -= transform.right;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movementDirection += transform.right;
        }

        return CameraNewPositionConsideringTheLimits(movementDirection);
    }

    Vector3 CameraNewPositionConsideringTheLimits(Vector3 movementDirection)
    {
        Vector3 newPositions = transform.position + (movementDirection * CameraMovementSpeed * Time.deltaTime);

        if (newPositions.x < _minCameraLimitPointPos.x) { newPositions.x = _minCameraLimitPointPos.x; }
        if (newPositions.z < _minCameraLimitPointPos.z) { newPositions.z = _minCameraLimitPointPos.z; }

        if (newPositions.x > _maxCameraLimitPointPos.x) { newPositions.x = _maxCameraLimitPointPos.x; }
        if (newPositions.z > _maxCameraLimitPointPos.z) { newPositions.z = _maxCameraLimitPointPos.z; }

        return newPositions;
    }

    Vector3 CameraZoomWithMouseScrollWheel()
    {
        Vector3 newPositions = transform.position + (transform.forward * Input.GetAxis("Mouse ScrollWheel") * CameraZoomSpeed);

        if (newPositions.y < _minYCoord) { newPositions.y = _minYCoord; }
        if (newPositions.y > _maxYCoord) { newPositions.y = _maxYCoord; }

        return newPositions;
    }
}
