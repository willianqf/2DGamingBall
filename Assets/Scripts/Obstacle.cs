using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField]
    [Min(0)]
    private float SpeedBall = 4f;
    [SerializeField]
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.tag == "Bonus")
        {
            transform.Rotate(new Vector3(0, 0, Random.Range(-117, -63)));
        }
        else{
            transform.Rotate(new Vector3(0, 0, Random.Range(-135, -55)));
        }
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.MovePosition(transform.position + transform.right * SpeedBall * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D enter){
        if(enter.CompareTag("Player"))
        {
            if(transform.name == "Obs(Clone)")
            {
                if(!Bola.instBola.valorInvencible())
                {
                    Bola.instBola.Destruir();
                }
            }
            else if(transform.name == "ObsSpeed(Clone)"){
                Bola.instBola.StartCoroutine(Bola.instBola.AlterVelocityTime(10, 7));
                Destroy(gameObject);
            }
            else if(transform.name == "ObsBonus(Clone)"){
               GameController.instance.AddPontuacao(3);
               Destroy(gameObject);
            }
            else if(transform.name == "ObsInvencivel(Clone)"){
                Bola.instBola.Invencible();
                Destroy(gameObject);
            }
            else if(transform.name == "ObsBonus5(Clone)"){
               GameController.instance.AddPontuacao(5);
               Destroy(gameObject);
            }
            else if(transform.name == "ObsDown(Clone)"){
                Spawn.spawn.DownVelocitySpawn();
                Destroy(gameObject);
            }
            else if(transform.name == "ObsBomba(Clone)"){
                Spawn.spawn.AllCollider();
                Destroy(gameObject);
            }
            else if (transform.name == "ObsCoin(Clone)")
            {
                GameController.instance.AddPontuacao(1);
                Destroy(gameObject);
            }
            else if (transform.name =="ObsCoinBonus(Clone)")
            {
                Spawn.spawn.IniciarTimeSpawn();
                Destroy(gameObject);
            }
            else if (transform.name =="ObsWeapon(Clone)")
            {
                Bola.instBola.ActiveTiro();
                Destroy(gameObject);
            }
            else
            {
              
            }
        }
    }
    public void AlterSpeed(float valor)
    {
        SpeedBall = valor;
    }
    public void IniciarDestruicaoBomba()
    {
        Destroy(gameObject);
    }
    public void IniciarDestruicao()
    {
        StartCoroutine(AnimDestroy());
    }
    IEnumerator AnimDestroy()
    {
        gameObject.name = "Destruido";
        SpeedBall = 0;
        GameObject Children = gameObject.transform.GetChild(1).gameObject;
        SpriteRenderer sprit = Children.GetComponent<SpriteRenderer>();
        sprit.enabled = false;
        GameObject exp = gameObject.transform.GetChild(0).gameObject;
        exp.SetActive(true);
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}
