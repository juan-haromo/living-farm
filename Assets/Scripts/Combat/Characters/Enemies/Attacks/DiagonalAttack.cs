using System.Collections;
using UnityEngine;

public class DiagonalAttack : Attack
{
    [SerializeField] Transform bulletPrefab;
    Vector3 direction;

    void Start()
    {
        direction = new Vector3();
    }
    public override IEnumerator ActivateAttack(float levelModifier)
    {
        for (int i = 0; i < 3; i++)
        {
            do
            {
                direction.x = Random.Range(-1.0f, 1.0f);
                direction.y = Random.Range(-1.0f, 1.0f);
            } while (direction.x == 0 && direction.y == 0);
            
            direction.Normalize();
            yield return new WaitForSeconds(Random.Range(0.5f, 5f));
            GameObject instance = Instantiate(bulletPrefab.gameObject, CombatManager.Instance.soul.transform.position - (direction * 5.0f), bulletPrefab.rotation);
            instance.GetComponent<BasicBullet>().SetUp(direction, Random.Range(4, 7), 10 * (1 + levelModifier) , 5);
        }
        yield return new WaitForSeconds(4);
        CombatManager.Instance.AttackDone();
    }
}
