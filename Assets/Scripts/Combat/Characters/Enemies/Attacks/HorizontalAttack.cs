using System.Collections;
using UnityEngine;

public class HorizontalAttack : Attack
{
    [SerializeField] Transform bulletPrefab;
    int direction;
    public override IEnumerator ActivateAttack(float levelModifier)
    {
        for (int i = 0; i < 3; i++)
        {
            direction = Random.Range(1, 3) % 2 == 0? 1 : -1;
            yield return new WaitForSeconds(Random.Range(0.5f, 5f));
            GameObject instance = Instantiate(bulletPrefab.gameObject, CombatManager.Instance.soul.transform.position - new Vector3(7.5f * direction,0,0), bulletPrefab.rotation);
            instance.GetComponent<BasicBullet>().SetUp(Vector3.right * direction, Random.Range(4,7), 10 * levelModifier, 5);
        }
        yield return new WaitForSeconds(4);
        CombatManager.Instance.AttackDone();
    }
}