using UnityEngine;

public class CameraAimFollowController : MonoBehaviour
{
    public GameObject FollowTarget;

    [SerializeField] private float cameraSensitivity = 10f;
    [SerializeField, Range(0f, 1f)] private float distanceFromCameraHit = 0.1f;
    [SerializeField, Range(0f, 15.0f)] private float targetDistance = 8.0f;
    [SerializeField] private Vector3 offset = new(0f, 3f, 0f);

    private Vector2 mouse;

    void Start()
    {
        transform.position = offset;
    }

    void Update()
    {
        if (!Pause.paused)
        {
            mouse.x += Input.GetAxis("Mouse X") * cameraSensitivity;
            mouse.y -= Input.GetAxis("Mouse Y") * cameraSensitivity;

            mouse.y = Mathf.Clamp(mouse.y, -90f, 90f); //Restrict vertical camera view to not go past 90 degrees up or down
            transform.localEulerAngles = new(mouse.y, mouse.x, 0f); //Vertical mouse movement -> X rotation is vertical (up/down)

            if (Physics.Raycast(FollowTarget.transform.position + Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * offset, -transform.forward.normalized, out RaycastHit hitInfo, targetDistance, 1<<3))
            {
                transform.position = (FollowTarget.transform.position + Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * offset) + transform.forward.normalized * -(hitInfo.distance - distanceFromCameraHit); //offset is the distance from target to closest point camera collides with
            }
            else
            {
                transform.position = (FollowTarget.transform.position + Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * offset) + transform.forward.normalized * -(targetDistance); //How far back the camera is from target
            }
        }
    }

    private void LateUpdate()
    {
    }
}
