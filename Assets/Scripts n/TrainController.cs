using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour {
    public bool moving = false;
    public float speed = 1f;

    private bool signaledmove = false;

    private void FixedUpdate()
    {
        if(moving && signaledmove)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.back * 200 * -speed);

        }
        if (this.gameObject.transform.position.z < -520 || this.gameObject.transform.position.y <-20)
        {
            Destroy(this.gameObject);
        }
    }
    public void SignalToMove()
    {
        signaledmove = true;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
