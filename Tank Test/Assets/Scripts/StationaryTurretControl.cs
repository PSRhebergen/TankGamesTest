using System;
using UnityEngine;

public class StationaryTurretControl : MonoBehaviour
{

    public float turnSpeed = .5f;
    public float fireRate = 2f;
    public float range = 5f;
    public float shootingAngleTolerance = 5f;
    public float aimSpeed = 3f;
    public GameObject bullet;
    public Transform fireTransform;
    public AudioClip sound;
    public ParticleSystem particle;
    public float addRotation = 0f;

    private float nextTimeToFire = 0f;
    private bool firing = false;

    void Update()
    {
        LookForPlayer();
        if (firing)
        {
            FireAtPlayer();
        }
        else
        {
            this.transform.Rotate(Vector3.back * turnSpeed);
        }
    }

    void LookForPlayer()
    {
        Debug.DrawRay(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position - transform.position, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position - transform.position, range, ~(1 << LayerMask.NameToLayer("Enemy")));
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player")) //If we see the player we should be shooting
        {
            firing = true;
        }
        else
        {
            firing = false;
        }
    }

    void FireAtPlayer()
    {
        Vector3 vectorToTarget = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + addRotation, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, q, Time.deltaTime * aimSpeed);

        if (Time.time >= nextTimeToFire && Quaternion.Angle(transform.rotation, q) <= Math.Abs(shootingAngleTolerance))
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = sound;
            audio.Play();

            Instantiate(particle, fireTransform.position, fireTransform.rotation);
            particle.Play();
            var bulletClone = (GameObject)Instantiate(bullet, fireTransform.position, fireTransform.rotation);
            bulletClone.GetComponent<BulletControl>().shooter = gameObject.transform.parent.gameObject;
            Reload();
        }        
    }
    private void Reload()
    {
        nextTimeToFire = Time.time + fireRate;
    }
}