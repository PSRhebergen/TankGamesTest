using System;
using UnityEngine;

public class StationaryTurretControl : MonoBehaviour
{

    public float turnSpeed;
    public GameObject bullet;
    public float fireRate;
    public Transform fireTransform;

    public float shootingAngleTolerance = 5f;

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
            Vector3 vectorToTarget = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, q, Time.deltaTime * 3f);

            if (Time.time >= nextTimeToFire && Quaternion.Angle(transform.rotation, q) <= Math.Abs(shootingAngleTolerance))
            { 
                var bulletClone = (GameObject)Instantiate(bullet, fireTransform.position, fireTransform.rotation);
                bulletClone.GetComponent<BulletControl>().shooter = gameObject.transform.parent.gameObject;
                nextTimeToFire = Time.time + fireRate;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.DrawRay(fireTransform.position, GameObject.FindGameObjectWithTag("Player").transform.position - fireTransform.position, Color.red);
            RaycastHit2D[] hit = Physics2D.RaycastAll(fireTransform.position, GameObject.FindGameObjectWithTag("Player").transform.position - fireTransform.position,  (1 << LayerMask.NameToLayer("Player")));
            if (hit[1].collider.gameObject.CompareTag("Player"))
            {
                firing = true;
            }
            else
            {
                firing = false;
            }
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