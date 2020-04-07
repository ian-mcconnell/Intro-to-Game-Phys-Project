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
    Vector3 AimingTarget;
    public GameObject AimIndicator;
    Vector3 DistanceFromCamera;

    // Shooting vars
    public Rigidbody Shell;
    public GameObject Explosion;
    public Transform FireTransform;
    public AudioSource ShootingAudio;
    public AudioClip ChargingClip;
    public AudioClip FireClip;
    public AudioClip DryFireClip;
    public float MinIndicatorScale = 1f;
    public float MaxIndicatorScale = 12f;
    public float MaxChargeTime = 1f;

    private GameController GameCon;
    private PauseMenu pauseMenu;

    private float CurrentIndicatorScale;
    private float ChargeSpeed;
    private bool Fired;
    private bool OnCooldown = false;
    private float CooldownDuration = .1f;

    void OnEnable()
    {
        CurrentIndicatorScale = MinIndicatorScale;
    }

    void Start()
    {
        ChargeSpeed = (MaxIndicatorScale - MinIndicatorScale) / MaxChargeTime;

        GameCon = GameObject.Find("Game Controller").GetComponent<GameController>();
        pauseMenu = GameObject.Find("PauseCanvas").GetComponent<PauseMenu>();

        AimIndicator = GameObject.Find("AimIndicator");
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
            AimingTarget = hit.point;

            AimIndicator.transform.localScale = new Vector3(CurrentIndicatorScale, CurrentIndicatorScale, CurrentIndicatorScale);
            //Cannon.transform.LookAt(hitPoint);

            //Debug.Log("Ray Hit!");
        }
        
    }


    #region Old Firing System
    //void Fire()
    //{
    //    // Track the current state of the fire button and make decisions based on the current launch force.

    //    if (CurrentLaunchForce >= MaxLaunchForce && !Fired)
    //    {
    //        CurrentLaunchForce = MaxLaunchForce;
    //        FireShell();

    //    }
    //    else if (Input.GetButtonDown("Fire1"))
    //    {
    //        Fired = false;
    //        CurrentLaunchForce = MinLaunchForce;

    //        ShootingAudio.clip = ChargingClip;
    //        ShootingAudio.Play();

    //    }
    //    else if (Input.GetButton("Fire1") && !Fired)
    //    {
    //        CurrentLaunchForce += ChargeSpeed * Time.deltaTime;

    //    }
    //    else if (Input.GetButtonUp("Fire1") && !Fired)
    //    {
    //        FireShell();
    //    }
    //}

    //private void FireShell()
    //{
    //    // Instantiate and launch the shell.
    //    Fired = true;

    //    ShootingAudio.Stop();

    //    if (GameCon.ammoCurrent > 0)
    //    {
    //        GameCon.ammoCurrent -= 1;


    //        Rigidbody shellInstance = Instantiate(Shell, FireTransform.position, FireTransform.rotation); // as Rigidbody

    //        shellInstance.velocity = CurrentLaunchForce * FireTransform.right;

    //        ShootingAudio.clip = FireClip;
    //        ShootingAudio.Play();

    //        CurrentLaunchForce = MinLaunchForce;
    //    }
    //    else { ShootingAudio.clip = DryFireClip; ShootingAudio.Play(); }
    //}
    #endregion

    #region New Firing System

    void Fire()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
        if (!OnCooldown)
        {
            if (CurrentIndicatorScale >= MaxIndicatorScale && !Fired)
            {
                CurrentIndicatorScale = MaxIndicatorScale;
                FireShell();

            }
            else if (Input.GetButtonDown("Fire1"))
            {
                Fired = false;
                CurrentIndicatorScale = MinIndicatorScale;

                ShootingAudio.clip = ChargingClip;
                ShootingAudio.Play();

            }
            else if (Input.GetButton("Fire1") && !Fired)
            {
                CurrentIndicatorScale += ChargeSpeed * Time.deltaTime;

            }
            else if (Input.GetButtonUp("Fire1") && !Fired)
            {
                FireShell();
            }
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

            GameObject NewExplosion = Instantiate(Explosion, AimIndicator.transform.position, AimIndicator.transform.rotation);
            NewExplosion.transform.localScale *= CurrentIndicatorScale;

            OnCooldown = true;
            Invoke("SetOnCooldownToFalse", CooldownDuration);
        }
        else { ShootingAudio.clip = DryFireClip; ShootingAudio.Play(); }

        CurrentIndicatorScale = MinIndicatorScale;
    }

    private void SetOnCooldownToFalse()
    {
        OnCooldown = false;
    }

    private void LateUpdate()
    {
        AimIndicator.transform.position = AimingTarget;
    }

    #endregion
}