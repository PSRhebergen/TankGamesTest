using UnityEngine;

public class StationaryTurretControl : MonoBehaviour
{

    public float turnSpeed;
    public GameObject bullet;
    public float fireRate;
    public Transform fireTransform;

    public float shootingAngleError = .5f;

    private float nextTimeToFire = 0f;
    private bool firing = false;

    void Update()
    {
        if (!firing)
        {
            this.transform.Rotate(Vector3.back * turnSpeed);
        }
        else
        {
            Vector3 dir = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 2).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation.z);
            if (Time.time >= nextTimeToFire && 
                Quaternion.Angle(transform.rotation, lookRotation) <= 90f + shootingAngleError && 
                Quaternion.Angle(transform.rotation, lookRotation) >= 90f - shootingAngleError)
            { 
                var bulletClone = (GameObject)Instantiate(bullet, fireTransform.position, fireTransform.rotation);
                bulletClone.GetComponent<BulletControl>().shooter = gameObject.transform.parent.gameObject;
                nextTimeToFire = Time.time + fireRate;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            firing = true;
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            firing = false;
        }
    }
}