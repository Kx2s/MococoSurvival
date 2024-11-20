using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    public Vector2 inputVec;
    public Hand[] hands;
    public VariableJoystick joystick;
    public RuntimeAnimatorController[] animCon;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    void OnEnable()
    {
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }
    
    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        anim.SetFloat("Speed", joystick.Direction.magnitude);
        if (joystick.Direction.x != 0){
            spriter.flipX = joystick.Direction.x < 0 ;
            foreach (Hand hand in hands)
                hand.setHand(spriter.flipX);
        }

        Vector2 nextVec = joystick.Direction.normalized * GameManager.instance.Speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void OnBack()
    {
        GameManager.instance.Pause();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        GameManager.instance.subHealth(Time.deltaTime * collision.gameObject.GetComponent<Enemy>().attack * (100 - GameManager.instance.Reduces) / 100);
        Vibration.Vibrate(50);
        if (GameManager.instance.health <= 0) {
            for (int i=2; i<transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }

    public void DeadAnim()
    {
        anim.SetTrigger("Dead");
    }

}

