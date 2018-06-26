using UnityEngine;

public class MoveTurret : MonoBehaviour {

    public float turnSpeed;
    
	void FixedUpdate ()
    {
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(Vector3.back * turnSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(Vector3.forward * turnSpeed);
        }

    }
}
