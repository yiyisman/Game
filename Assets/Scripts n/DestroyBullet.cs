using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {

    public ParticleSystem explosion;
    public AudioSource a;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   
    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            a.Play();
            Explode(col, explosion);
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }
 


    }
    void Explode(Collision other, ParticleSystem particles)
    {

        ParticleSystem exp = Instantiate(particles, other.transform.position, Quaternion.identity) as ParticleSystem;

        exp.Play();
    }
}
