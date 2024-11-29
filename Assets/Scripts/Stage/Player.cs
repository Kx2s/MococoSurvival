using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;

    public Vector2 inputVec;
    public Hand[] hands;
    public SpriteRenderer spriter;
    public VariableJoystick joystick;
    public RuntimeAnimatorController animCon;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    void OnEnable()
    {
        anim.runtimeAnimatorController = animCon;
    }
    
    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (joystick.gameObject.activeSelf)
        {
            inputVec = joystick.Direction.normalized;
        }

        anim.SetFloat("Speed", inputVec.magnitude);
        if (inputVec.x != 0){
            spriter.flipX = inputVec.x < 0 ;
            foreach (Hand hand in hands)
                hand.setHand(spriter.flipX);
        }

        Vector2 nextVec = inputVec * GameManager.instance.Speed * Time.deltaTime;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;
        
        GameManager.instance.subHealth(collision.gameObject.GetComponent<Enemy>().attack * (100 - GameManager.instance.Reduces) / 100);
        if (GameManager.instance.health <= 0) {
            for (int i=3; i<transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }

        Vibration.Vibrate(50);
    }

    public void DeadAnim()
    {
        anim.SetTrigger("Dead");
    }

}

