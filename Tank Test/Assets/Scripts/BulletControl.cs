using UnityEngine;

public class BulletControl : MonoBehaviour {

    public float speed;
    public ParticleSystem particle;
    public int damage;
    public float rangeTime; //Time until bullet is destroyed

    [HideInInspector]
    public GameObject shooter;

    private float startTime;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
        startTime = Time.time;
    }

    private void FixedUpdate()
    {
        if(Time.time >= startTime + rangeTime)
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
        Destroy(this.gameObject);
        Instantiate(particle, transform.position, transform.rotation);
    }
}