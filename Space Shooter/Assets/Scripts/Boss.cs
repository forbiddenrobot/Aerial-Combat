using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float health;
    public float speed = 5.0f;
    public float rotationSpeed = 5.0f;
    [SerializeField] private Vector2 movePos;

    [SerializeField] GameObject explosion;
    [SerializeField] HealthBar healthBar;

    [SerializeField] private List<BossComponent> parts;

    // Start is called before the first frame update
    void Start()
    {
        GameMaster.totalEnemies += 1;
        foreach (BossComponent child in transform.GetComponentsInChildren<BossComponent>())
        {
            parts.Add(child);
        }

        healthBar.maxHealth = health;
        healthBar.health = health;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(0, 0) * Mathf.Rad2Deg + 180;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        float distanceToPos = Vector2.Distance(transform.position, movePos);
        if (distanceToPos > 0.02f)
        {
            transform.position = Vector2.MoveTowards(transform.position, movePos, speed * Time.deltaTime);
        }

        parts.Clear();
        foreach (BossComponent child in transform.GetComponentsInChildren<BossComponent>())
        {
            parts.Add(child);
        }
    }

    public void TakeDamage(float amount)
    {
        if (parts.Count <= 0)
        {
            health -= amount;
            healthBar.health -= amount;
            healthBar.HealthBarFiller();
            if (health <= 0)
            {
                Killed();
            }
        }
    }

    void Killed()
    {
        GameMaster.enemiesDestroyed += 1;
        Debug.Log(GameMaster.enemiesDestroyed);
        GameObject _explosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(_explosion, 2f);
        //GameObject canvas = healthBar.transform.gameObject;
        //Destroy(canvas);
        Destroy(gameObject);
    }
}
