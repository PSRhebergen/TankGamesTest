using UnityEngine;

public class FireBullet : MonoBehaviour {

    public GameObject bullet;
    public GameObject smokeBullet;
    public float speed;
    public float fireRate;
    public AudioClip sound;

    public Transform fireTransform;
    public ParticleSystem particle;

    private float nextTimeToFire;
	
	void FixedUpdate ()
    {
        if (Time.time >= nextTimeToFire)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                nextTimeToFire = Time.time + fireRate;
                Fire(0);
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                nextTimeToFire = Time.time + fireRate;
                Fire(1);
            }
        }
    }

    void Fire(int bulletType)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = sound;
        audio.Play();

        Instantiate(particle, fireTransform.position, fireTransform.rotation);
        particle.Play();

        if (bulletType == 0)
        {
            var bulletClone = (GameObject)Instantiate(bullet, fireTransform.position, fireTransform.rotation);
            bulletClone.GetComponent<Rigidbody2D>().AddForce(bulletClone.transform.up * speed);
        }
        else if(bulletType == 1)
        {
            var bulletClone = (GameObject)Instantiate(smokeBullet, fireTransform.position, fireTransform.rotation);
            bulletClone.GetComponent<Rigidbody2D>().AddForce(bulletClone.transform.up * speed);
        }

       

        
    }
}
