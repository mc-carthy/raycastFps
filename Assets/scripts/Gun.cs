using UnityEngine;

public class Gun : MonoBehaviour {

    private Camera mainCam;
	private float damage = 10f;
    private float range = 100f;

    private void Awake ()
    {
        mainCam = Camera.main;
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
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.gameObject.GetComponent<Target>();

            if (target)
            {
                Debug.Log(target.gameObject.name);
                target.TakeDamage(damage);
            }
        }
    }

}
