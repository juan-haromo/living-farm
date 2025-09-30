using Unity.VisualScripting;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{

    Vector3 direction = Vector3.zero;
    float speed = 0;
    float damageAmount = 0;

    void Awake()
    {
        CombatManager.Instance.OnTurnEnd += Clear;
    }

    void OnDestroy()
    {
        CombatManager.Instance.OnTurnEnd -= Clear;
    }

    public void SetUp(Vector3 direction, float speed, float damageAmount, float lifeTime)
    {
        this.direction = direction;
        this.speed = speed;
        this.damageAmount = damageAmount;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * direction.normalized);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.TryGetComponent(out Soul soul))
        {
            soul.ally.Damage(damageAmount);
        }
    }

    void Clear()
    {
        Destroy(gameObject);
    }

}