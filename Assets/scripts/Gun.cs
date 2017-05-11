using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField]
    private GameObject bulletImpact;
    private Camera mainCam;
    private ParticleSystem muzzleFlash;
	private float damage = 10f;
    private float range = 100f;
    private float bulletForce = 60f;

    private void Awake ()
    {
        mainCam = Camera.main;
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
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

}
