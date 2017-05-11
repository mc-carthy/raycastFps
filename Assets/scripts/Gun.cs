using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    [SerializeField]
    private GameObject bulletImpact;
    [SerializeField]
	private float damage;
    [SerializeField]
    private float range;
    [SerializeField]
    private float bulletForce;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private int clipSize;
    [SerializeField]
    private float reloadTime;

    private Camera mainCam;
    private ParticleSystem muzzleFlash;
    private float nextTimeToFire = 0f;
    private int ammoInClip;
    private bool isReloading = false;

    private void Awake ()
    {
        mainCam = Camera.main;
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        ammoInClip = clipSize;
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isReloading)
        {
           return; 
        }

        if (ammoInClip <= 0)
        {
            StartCoroutine(Reload());
            isReloading = true;
            return;
        }

        if (Input.GetButton("Fire1") && Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();

        ammoInClip--;

        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.gameObject.GetComponent<Target>();

            if (target)
            {
                Debug.Log(target.gameObject.name);
                target.TakeDamage(damage);
            }

            if (hit.rigidbody)
            {
                hit.rigidbody.AddForce(-hit.normal * bulletForce);
            }

            GameObject impactGO = Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject;
            Destroy(impactGO, 2f);
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        ammoInClip = clipSize;
    }

}
