using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    [SerializeField] private Vector2 _horizontalAttack;
    [SerializeField] private Vector2 _verticalAttack;
    [SerializeField] private float _attackRadius;
    [SerializeField] private LayerMask _enemyLayer;

    private Collider2D[] hits;
    public void Attack(PlayerController.AttackDirectional _attackDir)
    {
        //Удар по выбранному направлению
        switch (_attackDir)
        {
            case PlayerController.AttackDirectional.Left:
                hits = Physics2D.OverlapCircleAll(_horizontalAttack + new Vector2(transform.position.x, transform.position.y), _attackRadius, _enemyLayer);
                break;
            case PlayerController.AttackDirectional.Right:
                hits = Physics2D.OverlapCircleAll(new Vector2(_horizontalAttack.x * -1, _horizontalAttack.y) + new Vector2(transform.position.x, transform.position.y), _attackRadius, _enemyLayer);
                break;
            case PlayerController.AttackDirectional.Up:
                hits = Physics2D.OverlapCircleAll(_verticalAttack + new Vector2(transform.position.x, transform.position.y), _attackRadius, _enemyLayer);
                break;
            case PlayerController.AttackDirectional.Down:
                hits = Physics2D.OverlapCircleAll(new Vector2(_verticalAttack.x, _verticalAttack.y * -1) + new Vector2(transform.position.x, transform.position.y), _attackRadius, _enemyLayer);
                break;
        }
        //Триггер получения урона на всех объектах, которые попали под удар
        foreach(var hit in hits)
        {
            if(hit.GetComponent<GetDamage>() != null)
            {
                hit.GetComponent<GetDamage>().Damage(_attackDir);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(_horizontalAttack + new Vector2(transform.position.x, transform.position.y), _attackRadius);
        Gizmos.DrawWireSphere(new Vector2(_horizontalAttack.x * -1, _horizontalAttack.y) + new Vector2(transform.position.x, transform.position.y) , _attackRadius);
        Gizmos.DrawWireSphere(_verticalAttack + new Vector2(transform.position.x, transform.position.y), _attackRadius);
        Gizmos.DrawWireSphere(new Vector2(_verticalAttack.x, _verticalAttack.y * -1) + new Vector2(transform.position.x, transform.position.y), _attackRadius);
    }
}
