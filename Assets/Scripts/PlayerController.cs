using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;

    private Entity entity;
    private Stats stats;

    [SerializeField] private float midairMoveMult = 0.1f;
    [SerializeField] private float maxSpeed = 7f;
    [SerializeField] private float rotateSpeed = 5f;

    private bool _jumping = false;
    public float jumpMultiplier = 1.0f;

    private Rigidbody rb;
    private CapsuleCollider capsule;

    [SerializeField] private float groundCheckDistance = 0.005f;

    private bool Grounded => Physics.SphereCast(transform.position, capsule.radius, Vector3.down, out _, capsule.height / 2 - capsule.radius + groundCheckDistance, (1<<3));

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        entity = GetComponent<Entity>();
    }

    private void Start()
    {
        stats = entity.Stats;
    }

    private void FixedUpdate()
    {
        if (movement != Vector3.zero)
        {
            Vector3 moveDir = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * movement; //Turn movement direction based on viewpoint direction
            rb.MovePosition(rb.position + (stats.MoveAcceleration * Time.fixedDeltaTime * new Vector3(moveDir.x, 0, moveDir.z)));
            transform.forward = Vector3.Slerp(new(transform.forward.x, transform.forward.y, transform.forward.z), new(moveDir.x, transform.forward.y, moveDir.z), rotateSpeed * Time.fixedDeltaTime);
            if (!Grounded)
            {
                return;
                rb.MovePosition(rb.position + (stats.MoveAcceleration * Time.fixedDeltaTime * moveDir * midairMoveMult));
                transform.forward = Vector3.Slerp(new(transform.forward.x, transform.forward.y, transform.forward.z), new(moveDir.x, transform.forward.y, moveDir.z), rotateSpeed * Time.fixedDeltaTime * midairMoveMult);
            }
        }
    }

    void Update() 
    {
        if (!Pause.paused)
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            if (Input.GetButtonDown("Jump") && Grounded)
            {
                _jumping = true;
            }

            Actions();
        }
    }

    private void Actions()
    {
        if (_jumping)
        {
            rb.AddForce(new Vector3(rb.linearVelocity.x, stats.JumpHeight * jumpMultiplier, rb.linearVelocity.z), ForceMode.VelocityChange);
            _jumping = false;
        }
    }
}