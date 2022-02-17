using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    public float SpeedTime = 25;
    public GameObject ball;
    Animator anim;
    SpriteRenderer sprit;
    Color colores;

    void Start()
    {
       anim = GetComponent<Animator>();
       sprit = GetComponent<SpriteRenderer>();
       colores = sprit.color;

    }
    void OnEnable() {
        transform.position = ball.transform.position;
    }
    void Update()
    {
        transform.position += (ball.transform.position - transform.position) * SpeedTime * Time.deltaTime;
    }

    public void ActiveInvencible(bool valor)
    {
        anim.SetBool("IsInvicible", valor);
    }

    public void Unable()
    {
        anim.SetBool("IsInvicible", false);
    }
}
