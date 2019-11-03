using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Aiming vars
    public GameObject Cannon;
    public float RotationSpeed = 3;
    private float RotationAccelerator = 0;

    // Shooting vars
    public Rigidbody Shell;
    public Transform FireTransform;
    public AudioSource ShootingAudio;
    public AudioClip ChargingClip;
    public AudioClip FireClip;
    public float MinLaunchForce = 1f;
    public float MaxLaunchForce = 40f;
    public float MaxChargeTime = 0.75f;

    private GameController GameCon;

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
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Fire();
    }

    void Aim()
    {
        float rotDirection = Input.GetAxis("Horizontal");

        if (rotDirection != 0)
        {
            if (RotationAccelerator < 60) { RotationAccelerator += 1f; }
        }
        else { RotationAccelerator = 0; }

        float turn = rotDirection * (RotationSpeed + RotationAccelerator) * Time.deltaTime;
        Cannon.transform.Rotate(new Vector3(0f, 0f, turn));
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
    }
}