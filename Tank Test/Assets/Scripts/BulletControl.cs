using UnityEngine;

public class BulletControl : MonoBehaviour {

    public float speed; //How fast the bullet should be traveling
    public int damage; //How many hit points to deduct
    public float rangeTime; //Time until bullet is destroyed
    public ParticleSystem explosion; //The explosion VFX to be played upon impact
    public AudioClip impactSound; //The SFX to be played upon impact
    
    [HideInInspector]
    public GameObject shooter;

    private float startTime;
    private AudioSource audioSource;
    private bool isDestroyed;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
        startTime = Time.time;
        audioSource = GetComponent<AudioSource>();
        isDestroyed = false;
    }

    private void FixedUpdate()
    {
        //Check if the bullet has travelled full range and is not in the Destroyed state
        if(Time.time >= startTime + rangeTime && !isDestroyed)
        {
            BulletHit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != shooter)
        {
            BulletHit();

            //The not as stupid way to check this
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
            {
               collision.gameObject.GetComponent<HealthControl>().subtractHealth(damage);
            }
        }    
    }

    private void BulletHit()
    {
        //Start up the impact FX
        audioSource.clip = impactSound;
        audioSource.Play();
        Instantiate(explosion, transform.position, transform.rotation);

        //Disable everything but the audio
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        isDestroyed = true;

        //Then destroy self after the explosion finishes
        Destroy(this.gameObject, audioSource.clip.length);
    }
}