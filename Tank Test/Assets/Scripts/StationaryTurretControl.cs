using UnityEngine;

public class StationaryTurretControl : MonoBehaviour
{

    public float turnSpeed;
    public GameObject bullet;

    public float speed;
    public Transform fireTransform;

    private Vector3 player;

    private float nextTimeToFire;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform.position;
    }

    private void Update()
    {
        transform.LookAt(new Vector3(player.x, player.y, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //RaycastHit2D hit = Physics2D.Raycast(fireTransform.position, Vector2.up);
            //if (hit.collider != null)
            //{
            if (Time.time >= nextTimeToFire)
            {
                var bulletClone = (GameObject)Instantiate(bullet, fireTransform.position, fireTransform.rotation);
                bulletClone.GetComponent<Rigidbody2D>().AddForce(bulletClone.transform.up * speed);

                nextTimeToFire = Time.time + 1;
            }
            //}
            //else
            //    this.transform.Rotate(Vector3.back * turnSpeed);
        }
        
    }
}