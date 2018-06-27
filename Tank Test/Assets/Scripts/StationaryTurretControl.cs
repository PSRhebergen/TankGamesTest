using UnityEngine;

public class StationaryTurretControl : MonoBehaviour
{

    public float turnSpeed;
    public GameObject bullet;

    public float bulletSpeed;
    public Transform fireTransform;

    private float nextTimeToFire = 0f;

    void Update()
    {
        
        //this.transform.Rotate(Vector3.back * turnSpeed);
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collision");
        if (col.gameObject.CompareTag("Player"))
        {
            Vector3 dir = col.gameObject.transform.position - this.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            if (Time.time >= nextTimeToFire)
            {
                var bulletClone = (GameObject)Instantiate(bullet, fireTransform.position, fireTransform.rotation);
                bulletClone.GetComponent<Rigidbody2D>().AddForce(bulletClone.transform.up * bulletSpeed);

                nextTimeToFire = Time.time + 2;
                
            }
        }
        
    }
}