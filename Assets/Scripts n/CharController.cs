using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharController : MonoBehaviour {

    bool alive = true;
    bool grounded = true;
    bool time = false;
    bool powerup = false;
    bool weapon = false;


    public int timeLeft = 3;
    public float timeLeftp;
    public float timeLeftw;
    public Text countdownText;

    int jump = 0;
    public ParticleSystem explosion;

    int MaxJumpCycles = 15;

    int left = 0;
    int right = 0;
    int maxLeftCycles = 5;
    int maxRightCycles = 5;
    public Canvas menu;
    public Canvas music;
    public Canvas win;
    public Canvas his2;

    public Text Wins;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    int score = 0;
    private float nextFire;

    public Camera MainCamera;

	// Use this for initialization
	void Start () {
        StartCoroutine("LoseTime");
        jump = MaxJumpCycles;
        left = maxLeftCycles;
        right = maxRightCycles;
        menu.enabled = false;
        win.enabled = false;
        his2.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        ActivateTrainsInRange();
        countdownText.text = ("" + timeLeft);

        if (timeLeft <= 0)
        {
            StopCoroutine("LoseTime");
            countdownText.text = "";
            music.GetComponent<AudioSource>().Stop();
            time = true;
        }
        if (timeLeftp <= 0)
        {
            StopCoroutine("LoseTimep");
            powerup = false;
        }
        if (timeLeftw <= 0)
        {
            StopCoroutine("LoseTimew");
            weapon = false;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        { right = 0;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            left = 0;
        }
        if (Input.GetKeyUp(KeyCode.Space)&& grounded)
        {
            jump = 0;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            menu.enabled = true;
        }
        if (Input.GetButton("Fire1") && Time.time > nextFire && weapon)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GameObject.Find("Shot Spawn").GetComponent<AudioSource>().Play();
        }


    }
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            music.GetComponent<AudioSource>().Play();
            timeLeft--;

        }
    }
    IEnumerator LoseTimep()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            timeLeftp--;
        }
    }
    IEnumerator LoseTimew()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeLeftw--;
        }
    }

    private void ActivateTrainsInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(this.gameObject.transform.position, 40f);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.GetComponent<TrainController>()!=null) {
                c.gameObject.GetComponent<TrainController>().SignalToMove();
            }
        }
    }
    private void FixedUpdate()
    {


        if (alive && time) {
            if (!powerup)
            {
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -20);
            }
            
        }
        if (alive && time && powerup)
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -60);
        }
        if (jump<MaxJumpCycles)
        {
            this.GetComponent<Rigidbody>().AddForce(0, 400, 300);
            jump++;
            grounded = false;
            Invoke("bajar", 0.7f);
        }
        if (left < maxLeftCycles)
        {
            this.GetComponent<Rigidbody>().MovePosition(
                new Vector3(
                    this.gameObject.transform.position.x + (4.5f / maxLeftCycles),
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z));
            MainCamera.SendMessage("AdjustCamera",-(2.5f/maxLeftCycles));
            left++;
        }
        if (right < maxRightCycles)
        {
            this.GetComponent<Rigidbody>().MovePosition(
                new Vector3(
                    this.gameObject.transform.position.x - (4.5f / maxLeftCycles),
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z));
            

            MainCamera.SendMessage("AdjustCamera", +(2.5f / maxRightCycles));
            right++;
        }
        
    }
    void bajar()
    {
        this.GetComponent<Rigidbody>().AddForce(0, -400, -300);
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle") )
        {
            Explode(col, explosion);
            col.collider.GetComponent<AudioSource>().Play();
            alive = false;

           
            menu.enabled = true;
        }
        else if (col.collider.gameObject.layer==LayerMask.NameToLayer("Ground")  )
        {
            this.GetComponent<Animator>().speed = 1;
            grounded = true;
        }
        else if (col.collider.gameObject.layer == LayerMask.NameToLayer("lava"))
        {
            Explode(col, explosion);
            col.collider.GetComponent<AudioSource>().Play();
            Destroy(this);
            menu.enabled = true;
        }
        else if (col.collider.gameObject.layer == LayerMask.NameToLayer("final"))
        {


            music.enabled = false;
            win.enabled = true;
            Debug.Log(""+score);
            Wins.text = "Score:" + this.score.ToString();
            his2.enabled = true;
        }


    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            this.GetComponent<AudioSource>().Play();
            this.score++;
            this.UpdateScore();
            
            Destroy(other.gameObject);
        }
        if (other.tag == "oil")
        {
            timeLeftp = 1.2f;
            powerup = true;
            other.gameObject.GetComponent<AudioSource>().Play();
            StartCoroutine("LoseTimep");
            Destroy(other.gameObject);
        }
        if (other.tag == "weapon")
        {
            timeLeftw = 3f;
            weapon = true;
            StartCoroutine("LoseTimew");
            Destroy(other.gameObject);
        }
        if (other.tag == "oilt")
        {
        
            SceneManager.LoadScene("Main 1");
        }
        

    }
    public void UpdateScore()
    {
       
        GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>().text = "Score:" + this.score;
        

    }
    void Explode(Collision other, ParticleSystem particles)
    {
        
        ParticleSystem exp = Instantiate(particles, this.transform.position, Quaternion.identity) as ParticleSystem;

        exp.Play();
        this.GetComponent<Animator>().speed = 0;
        MainCamera.GetComponent<CameraController>().DeathCamera();
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().AddForce(1500f, 1500f, 1000f);
    }

}
