using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] LineRenderer laserLine;
    [SerializeField] float laserRange = 600f;
    [SerializeField] float laserDuration = 0.01f;
    public bool shootGun()
    {
        if (Physics.Raycast(spawnPoint.position, transform.forward, out RaycastHit hit, laserRange))
        {
            laserLine.enabled = true;
            laserLine.SetPosition(0, spawnPoint.position);
            laserLine.SetPosition(1, hit.point);

            if (hit.transform.gameObject.tag == "Target")
            {
                StartCoroutine(ShootLaser());
                Destroy(hit.transform.gameObject);
                return true;
            }
            else if (hit.collider.gameObject.tag == "Wall")
            {
                StartCoroutine(ShootLaser());
                return false;
            }
        }
        StartCoroutine(ShootLaser());
        return false;
    }

    private IEnumerator ShootLaser()
    {
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}
