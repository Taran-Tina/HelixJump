using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int breakCount = 0;
    public bool gotPower = false;
    public Camera Camera;
    public float Velocity = 20f;
    public Transform  CurrentSliceTransform;
    public bool CanMoveCamera = false;
    public Rigidbody RigidBody;

    public float BounceSpeed;
    public GameObject manager;
    public Platform CurrentPlatform;
    public AudioSource _audio;
    public AudioClip ballBam;
    public AudioClip audioDie;

    private GameObject platformDestructionVFX;


    void Start()
    {
        manager = GameObject.Find("GameManager");
        Camera = Camera.main;
        //позиционируем камеру
        Camera.transform.position = new Vector3(0, 2, -9);

        RigidBody = gameObject.GetComponent<Rigidbody>();

        platformDestructionVFX = GameObject.Find("PlatformDestruction");
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.name == "Slice")
        {
            CanMoveCamera = true;
            CurrentSliceTransform = trigger.transform;
            breakCount++;
            if (breakCount >= 3)
            {
                gotPower = true;
            }
            else
            {
                gotPower = false;
            }
        }
    }

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.gameObject.name == "Slice")
        {
            Destroy(other.gameObject);
        }
        manager.GetComponent<Helix>().AddScore(1);

        _audio.Play();
        platformDestructionVFX.transform.position = new Vector3(platformDestructionVFX.transform.position.x, 
                                                                Camera.transform.position.y-3,
                                                                platformDestructionVFX.transform.position.z);
        platformDestructionVFX.GetComponent<ParticleSystem>().Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //если сталкиваюсь с объектами с тегом Piece
        if (collision.gameObject.tag == "Piece" && CanMoveCamera==true)
        {
            CanMoveCamera = false;
        }
        //если столкнулс€ с блоком - Bad
        if (collision.gameObject.name == "Bad")
        {
          //  Debug.Log("ѕроиграл");
        }
        //если столкнулс€ с блоком - Normal
        if (collision.gameObject.name == "Normal")
        {
          //  Debug.Log("ќтскочил");
        }
        //если столкнулс€ с блоком - Bonus
        if (collision.gameObject.name == "Bonus")
        {
           // Debug.Log("+1 очко, блок исчез");
        }
        //если столкнулс€ с блоком - End
        if (collision.gameObject.name == "End")
        {
          //  Debug.Log(" онец игры");
        }
    }

    public void Update()
    {
        if (CurrentSliceTransform != null && CanMoveCamera == true)
        {
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, new Vector3(Camera.transform.position.x, CurrentSliceTransform.position.y + 3, Camera.transform.position.z),  Time.deltaTime * Velocity);
        }
    }


    public void Bounce()
    {
        RigidBody.velocity = new Vector3(0, BounceSpeed, 0);
        breakCount = 0;
        gotPower = false;
        PlayAudioAtBallCollision(ballBam);
    }

    public void Die()
    {
        manager.GetComponent<Game>().OnBallDied();
        RigidBody.velocity = Vector3.zero;
        manager.GetComponent<Controls>().iCanMove = false;
        manager.GetComponent<Helix>().BallDies();
        PlayAudioAtBallCollision(audioDie);
    }

    public void ReachFinish()
    {
        manager.GetComponent<Game>().OnBallReachedFinish();
        RigidBody.velocity = Vector3.zero;
        manager.GetComponent<Controls>().iCanMove = false;
        manager.GetComponent<Helix>().GoNext();
    }

    public void Score(int value)
    {
        manager.GetComponent<Helix>().AddScore(value);
    }

    public AudioSource PlayAudioAtBallCollision(AudioClip clip)
    {
        GameObject go = new GameObject("Bam");
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        GameObject.Destroy(go, clip.length);
        return source;
    }

}
