using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDamage : MonoBehaviour
{
    [SerializeField] float _force;          //���� ������������ ���������� ��� ��������� �����
    [SerializeField] float _timeToDisable;  //����� ���������� ������� �� ������
    [SerializeField] int _hp;               //�������� ����������
    private Rigidbody2D _rb;                //���������� ����
    private PingPongAI _ai;                 //������ �� �������� ����������
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ai = GetComponent<PingPongAI>();
    }
    public void Damage(PlayerController.AttackDirectional _attackDir)
    {
        if(_hp > 1)
        {
            _hp--;
        }
        else
        {
            Destroy(gameObject);
        }
        Vector2 _outDir = new Vector2();
        switch (_attackDir)
        {
            case PlayerController.AttackDirectional.Left:
                _outDir = new Vector2(-1f, 0);
                break;
            case PlayerController.AttackDirectional.Right:
                _outDir = new Vector2(1f, 0);
                break;
            case PlayerController.AttackDirectional.Up:
                _outDir = new Vector2(0, 1f);
                break;
            case PlayerController.AttackDirectional.Down:
                _outDir = new Vector2(0, -1f);
                break;
        }

        StartCoroutine(DisableMovement(_outDir));
    }

    IEnumerator DisableMovement(Vector2 _outDirect)
    {
        _ai.enabled = false;
        _rb.velocity = _outDirect * _force;
        yield return new WaitForSeconds(_timeToDisable);
        _ai.enabled = true;
    }
}
