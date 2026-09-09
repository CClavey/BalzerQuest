using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestAttack : MonoBehaviour
{
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    public float destroyTime = 2;

    private void Update()
    {
        //button 0 is left click
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectileTest = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = transform.position;
            Vector2 direction = (mousePos - myPos).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectileTest.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            projectileTest.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            projectileTest.GetComponent<TestHit>().damage = Random.Range(minDamage, maxDamage);
            Destroy(projectileTest, destroyTime);
        }
    }
}
