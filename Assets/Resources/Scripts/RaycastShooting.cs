using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShooting : MonoBehaviour {

    public Transform firePoint;
    public GameObject player;
    public GameObject impactEffect;
    public LineRenderer lineRenderer;

    private int isBulletVisible;

    // Use this for initialization
    void Start()
    {
        isBulletVisible = 0;
    }

    // Update is called once per frame
    void Update () {
        // shoots whenever the primary mouse button is clicked
		if (Input.GetKey("space"))//Input.GetMouseButton(0))
        {
            Shoot();
        } else
        {
            lineRenderer.enabled = false;
        }
	}

    // shoots a bullet
    private void Shoot()
    {
        // sends a raycast out
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        // draws the line and damages the enemy if the hit is an enemy
        if (hitInfo)
        {
            // applies damage to the enemy if it's a damageable obstacle
            hitInfo.collider.gameObject.SendMessage("ApplyDamage", 20f);
            // draws the laser line
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
            // draws the impact effect
            StartCoroutine("ImpactEffect", hitInfo);
        } else
        {
            // only draws the line if nothing is hit
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 50f);
        }

        // shows the laser and impact effect, then hides it in the next frame
        // this if statement shows the laser from frames 1-3
        if (isBulletVisible <= 2)
        {
            lineRenderer.enabled = true;
        }
        else
        {
            // hides the laser from frames 4-6
            lineRenderer.enabled = false;
        }

        // resets the frame counter
        if (isBulletVisible == 5)
        {
            isBulletVisible = 0;
        }

        isBulletVisible++;
    }

    // shows the impact effect
    IEnumerator ImpactEffect(RaycastHit2D hitInfo)
    {
        // draws the impact effect
        GameObject impact = Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

        impact.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        impact.SetActive(false);
    }
}
