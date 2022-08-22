using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Header("Input Stuff:")]
    [SerializeField]KeyCode fireKey = KeyCode.Mouse0;
    [SerializeField]KeyCode aimKey = KeyCode.Mouse1;
    [SerializeField]KeyCode reloadKey = KeyCode.R;
    
    [Header("Gun Stuff:")]
    [SerializeField] float shootDistance;
    [SerializeField]Transform gunpos;
    [SerializeField]LayerMask whatToShoot;
    [SerializeField]float hitForce;
    [SerializeField]float magazineSize;
    [SerializeField]float currentMagazine;
    
    [Header("Aiming Stuff;")]
    [SerializeField]Vector3 aimPos;
    [SerializeField]float aimFOV;

    
    [Header("Auto Stuff:")]
    [SerializeField]float fireRate;
    [SerializeField]bool autoShoot;

    [Header("Visual Stuff:")]
    [SerializeField] ParticleSystem HitEffect;
    [SerializeField] ParticleSystem MuzzleFlash;
    
    [Header("Debug Stuff:")]
    [SerializeField]bool debug;
    
    Camera cam;
    Vector3 tempGunPos;
    float orgFOV;
    Animator anim;
    float nextFire = 0;

    void Start(){
        cam = Camera.main;
        anim = GetComponentInParent<Animator>();
        tempGunPos = gunpos.localPosition;
        orgFOV = cam.fieldOfView;
        currentMagazine = magazineSize;
    }

    void Update(){
        if(Input.GetKeyDown(fireKey) && currentMagazine > 0){
            Shoot();
        }
        
        if(Input.GetKeyDown(aimKey)){
            Aim();
        }
        
        if(Input.GetKeyUp(aimKey)){
            UnAim();
        }

        if(Input.GetKey(fireKey) && autoShoot && currentMagazine > 0){
            if(Time.time >= nextFire){
                nextFire = Time.time + (1 / fireRate);
                Shoot();
            }
        }

        if(Input.GetKeyDown(reloadKey)){
            currentMagazine = magazineSize;
        }
        UIScript.Instance.currentMagazine = currentMagazine;
    }

    private void Shoot(){
        
        MuzzleFlash.Stop();
        MuzzleFlash.Play();
        currentMagazine --;
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position + cam.transform.forward, cam.transform.forward, out hit, shootDistance, whatToShoot)){
            ParticleSystem effect = Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            effect.transform.parent = hit.collider.gameObject.transform;
            Destroy(effect,HitEffect.main.duration);            
            if(debug){Debug.Log(hit.collider.name);}
            if(hit.collider.TryGetComponent(out Target target)){
                target.Hit();
            }
            if(hit.collider.TryGetComponent(out Rigidbody targetRb)){
                targetRb.AddForceAtPosition(-hit.normal*hitForce, hit.point);
            }
        }
    }

    private void Aim(){
        gunpos.localPosition = aimPos;
        StartCoroutine(Zoom(aimFOV,1));
    }

    IEnumerator Zoom(float targetFOV,float duration){
        float timer = 0;
        float target = (cam.fieldOfView - targetFOV) - cam.fieldOfView;
        while(cam.fieldOfView <= target){
            cam.fieldOfView = target / duration * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
        cam.fieldOfView = targetFOV;
    }

    private void UnAim(){
        gunpos.localPosition = tempGunPos;
        cam.fieldOfView = orgFOV;
    }
}
