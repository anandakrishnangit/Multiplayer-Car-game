using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Carguns : MonoBehaviour
{
    public GameObject gun1;
    bool gun1active = false;
    public RaycastHit hit;
    public Transform rayorgin;
    [SerializeField] public Transform bulletspawn;
    [SerializeField] public GameObject bulletprefab;
    float range = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gun1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (gun1active == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Gun1Shoot();
                ShootRay();
            }

            Debug.DrawRay(rayorgin.position, rayorgin.forward * range, Color.red);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gun1"))
        {
            StartCoroutine(GunActivation());

        }
    }

    IEnumerator GunActivation()
    {
        gun1.SetActive(true);
        gun1active = true;
        yield return new WaitForSeconds(20f);
        gun1.SetActive(false);
        gun1active = false;
    }

    void Gun1Shoot()
    {
        GameObject bullet1 = Instantiate(bulletprefab, bulletspawn.position, Quaternion.identity);
    }
    void ShootRay()
    {
        if (Physics.Raycast(rayorgin.position, rayorgin.forward, out hit, range))
        {
            if (hit.collider.CompareTag("enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
