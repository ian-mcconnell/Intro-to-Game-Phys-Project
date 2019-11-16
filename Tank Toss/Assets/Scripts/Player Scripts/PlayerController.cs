using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // Aiming vars
    public GameObject Cannon;

    public float DistanceZ;
    public GameObject PlaneObject;
    int aimingPlane;
    Vector3 DistanceFromCamera;

    // Shooting vars
    public Rigidbody Shell;
    public Transform FireTransform;
    public AudioSource ShootingAudio;
    public AudioClip ChargingClip;
    public AudioClip FireClip;
    public AudioClip DryFireClip;
    public float MinLaunchForce = 1f;
    public float MaxLaunchForce = 40f;
    public float MaxChargeTime = 0.75f;

    private GameController GameCon;
    private PauseMenu pauseMenu;

    private float CurrentLaunchForce;
    private float ChargeSpeed;
    private bool Fired;

    void OnEnable()
    {
        CurrentLaunchForce = MinLaunchForce;
    }

    void Start()
    {
        ChargeSpeed = (MaxLaunchForce - MinLaunchForce) / MaxChargeTime;

        GameCon = GameObject.Find("Game Controller").GetComponent<GameController>();
        pauseMenu = GameObject.Find("PauseCanvas").GetComponent<PauseMenu>();

        aimingPlane = LayerMask.GetMask("Raycast Plane");
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu.GameIsPaused == false)
        {
            Aim();
            Fire();
        }
    }

    void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, DistanceZ, aimingPlane))
        {
            Vector3 hitPoint = hit.point;

            Cannon.transform.LookAt(hitPoint);

            //Debug.Log("Ray Hit!");
        }
        
    }

    void Fire()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.

        if (CurrentLaunchForce >= MaxLaunchForce && !Fired)
        {
            CurrentLaunchForce = MaxLaunchForce;
            FireShell();

        }
        else if (Input.GetButtonDown("Fire1"))
        {
            Fired = false;
            CurrentLaunchForce = MinLaunchForce;

            ShootingAudio.clip = ChargingClip;
            ShootingAudio.Play();

        }
        else if (Input.GetButton("Fire1") && !Fired)
        {
            CurrentLaunchForce += ChargeSpeed * Time.deltaTime;

        }
        else if (Input.GetButtonUp("Fire1") && !Fired)
        {
            FireShell();
        }
    }

    private void FireShell()
    {
        // Instantiate and launch the shell.
        Fired = true;

        ShootingAudio.Stop();

        if (GameCon.ammoCurrent > 0)
        {
            GameCon.ammoCurrent -= 1;


            Rigidbody shellInstance = Instantiate(Shell, FireTransform.position, FireTransform.rotation); // as Rigidbody

            shellInstance.velocity = CurrentLaunchForce * FireTransform.right;

            ShootingAudio.clip = FireClip;
            ShootingAudio.Play();

            CurrentLaunchForce = MinLaunchForce;
        }
        else { ShootingAudio.clip = DryFireClip; ShootingAudio.Play(); }
    }
}