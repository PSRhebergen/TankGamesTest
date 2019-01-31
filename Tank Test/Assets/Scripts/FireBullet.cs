using UnityEngine;

public class FireBullet : MonoBehaviour
{

    public GameObject[] bulletTypes;
    public float speed;
    public float fireRate;
    public AudioClip sound;

    public Transform fireTransform;
    public ParticleSystem particle;

    private float nextTimeToFire;
    private int currentBullet;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchBullet(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchBullet(1);
        }

        if (Time.time >= nextTimeToFire)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
        }
    }

    private void SwitchBullet(int i) //Switch to arg bullet and a reload time
    {
        currentBullet = i;
        Reload();
    }

    private GameObject getBullet() //returns the name of the currently loaded bullet
    {
        return bulletTypes[currentBullet];
    }

    void Fire()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = sound;
        audio.Play();

        Instantiate(particle, fireTransform.position, fireTransform.rotation);
        particle.Play();

        var bulletClone = (GameObject)Instantiate(getBullet(), fireTransform.position, fireTransform.rotation);
        bulletClone.GetComponent<BulletControl>().shooter = gameObject.transform.parent.gameObject;

        Reload();
    }

    private void Reload()
    {
        nextTimeToFire = Time.time + fireRate;
    }
}
