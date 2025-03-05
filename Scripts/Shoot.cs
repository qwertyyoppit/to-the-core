using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform attackPoint;
    public GameObject Bullet;

    public float BulletForce;

    public float BulletRange = 15.0f;

    private void Update()
    {
        if (!Pause.paused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShootWeapon();
            }
        }
    }

    private void ShootWeapon()
    {
        Vector3 targetPoint;
        Ray ray = Camera.main.ViewportPointToRay(new(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, BulletRange))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(BulletRange);
        }

        Vector3 Direction = targetPoint - attackPoint.position;

        GameObject currentBullet = Instantiate(Bullet, attackPoint.position, Quaternion.LookRotation(targetPoint - attackPoint.position));

        currentBullet.GetComponent<Rigidbody>().AddForce(Direction.normalized * BulletForce, ForceMode.Impulse);
        
    }
}
