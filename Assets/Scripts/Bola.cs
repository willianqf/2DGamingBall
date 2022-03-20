using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Bola : MonoBehaviour
{
    public GameObject SpeedBall;
    public static Bola instBola;
    private bool isRight;
    [SerializeField]
    [Min(2)]
    private float speedball = 4f;
    [SerializeField]
    private bool IsInvencible = false;
    [SerializeField]
    private bool IsSpeed = false;
    public Transform R;
    public Transform L;

    public Transform InvencibleUI;
    public Transform VelocityUI;
    private Animator Anim;
    private bool GameOver = false;

    [SerializeField]
    private GameObject Tiro;
    [SerializeField]
    private GameObject WeaponTiro;
    public bool TiroAtivado = false;
    void Awake() {
        instBola = this;
        Anim = GetComponent<Animator>();
    }
    void Update()
    {
        MovimentBall();
        VerificyBug();
        //IsClick();
        //CheckPoint();
    }

    void MovimentBall(){
        float TecladoPress = Input.GetAxisRaw("Horizontal");
        if(ButtonL.ButL.IsPressButtonL() || TecladoPress == -1)
        {
            if((Vector2.Distance(transform.position, R.position) > 0.100000)){
                transform.Translate(-Vector2.right * speedball *  Time.deltaTime);
            }
        }
        if(ButtonR.ButR.IsPressButtonR() || TecladoPress == 1)
        {
            if((Vector2.Distance(transform.position, L.position) > 0.100000)){
                transform.Translate(Vector2.right * speedball *  Time.deltaTime);
            }
        }
        /*
        if(isRight){
            transform.Translate(Vector2.right * speedball *  Time.deltaTime);
        }else{
            transform.Translate(-Vector2.right * speedball *  Time.deltaTime);
        }
        */
    }
    /*
    void IsClick(){
        if(Input.GetMouseButtonDown(0)){
            isRight = !isRight;
        }
    }

    void CheckPoint(){
        if(Vector2.Distance(transform.position, L.position) < 0.1f || Vector2.Distance(transform.position, R.position) < 0.1f){
            isRight = !isRight;
        }
    }
    */
    public void AlterSpeedBall(float valor, float time)
    {
        speedball = valor;
    }

    public void ActiveTiro()
    {
        StartCoroutine(IniciarTiro());
    }
    IEnumerator IniciarTiro()
    {
        TiroAtivado = true;
        float time = 10;
        float cooldawn = 0.3f;
        while(time > 0)
        {
            time -= cooldawn;
            SpawnTiro();
            yield return new WaitForSeconds(cooldawn);
        }
        TiroAtivado = false;

    }
    void SpawnTiro()
    {
        GameObject novotiro = Instantiate(Tiro, transform.position, transform.rotation);
        novotiro.transform.position = WeaponTiro.transform.position;
        Destroy(novotiro, 1);
    }
    IEnumerator UITempoSpeed(float time)
    {
        float tempocontagem = time;
        while(true)
        {
            yield return new WaitForSeconds(0.2f);
            tempocontagem-= 0.2f;
            if(tempocontagem <= 0)
            {
                break;
            }
            else
            {
                if (tempocontagem > 0 && tempocontagem < 4)
                {
                    if(VelocityUI.gameObject.activeSelf)
                    {
                        VelocityUI.gameObject.SetActive(false);
                    }
                    else
                    {
                        VelocityUI.gameObject.SetActive(true);
                    }
                }
            }
        }
        VelocityUI.gameObject.SetActive(false);
    }

    IEnumerator UITempoInvencible(float time)
    {
        float tempocontagem = time;
        IsInvencible = true;
        Anim.SetBool("IsInvicible", true);
        InvencibleUI.gameObject.SetActive(true);
        activeinvencible(SpeedBall, true);
        while(true)
        {
            yield return new WaitForSeconds(0.2f);
            tempocontagem-= 0.2f;
            if(tempocontagem <= 0)
            {
                Anim.SetBool("IsInvicible", false);
                IsInvencible = false;
                activeinvencible(SpeedBall, false);
                break;
            }
            else
            {
                if (tempocontagem > 0 && tempocontagem < 4)
                {
                    if(InvencibleUI.gameObject.activeSelf)
                    {
                        InvencibleUI.gameObject.SetActive(false);
                        Anim.SetBool("IsInvicible", false);
                        activeinvencible(SpeedBall, false);
                    }
                    else
                    {
                        InvencibleUI.gameObject.SetActive(true);
                        Anim.SetBool("IsInvicible", true);
                        activeinvencible(SpeedBall, true);
                    }
                }
            }
        }
        InvencibleUI.gameObject.SetActive(false);
    }

    public bool IsSpeedActive()
    {
        return IsSpeed;
    }

    public IEnumerator AlterVelocityTime(float time, float speed)
    {
        if(!GameOver)
        {
            speedball = speed;
        }
        IsSpeed = true;
        VelocityUI.gameObject.SetActive(true);
        //SpeedBall.SetActive(true);
        activespeed(SpeedBall, true);
        StartCoroutine(UITempoSpeed(time));
        yield return new WaitForSeconds(time);
        VelocityUI.gameObject.SetActive(false);
        activespeed(SpeedBall, false);
        //SpeedBall.SetActive(false);
        IsSpeed = false;
        if(!GameOver)
        {
            speedball = 4;
        }

    }

    void activespeed(GameObject Speedball, bool active)
    {
        Transform[] balls = SpeedBall.GetComponentsInChildren<Transform>();
        foreach (Transform x in balls)
        {
            if (x.tag == "PlayerSpeed")
            {
                PlayerSpeed valor = x.GetComponent<PlayerSpeed>();
                valor.ActiveSprite(active);
            }
        }
    }

    void activeinvencible(GameObject Speedball, bool active)
    {
        Transform[] balls = SpeedBall.GetComponentsInChildren<Transform>();
        foreach (Transform x in balls)
        {
            if (x.tag == "PlayerSpeed")
            {
                PlayerSpeed valor = x.GetComponent<PlayerSpeed>();
                valor.ActiveInvencible(active);
            }
        }
    }
    public void Invencible()
    {
        //StartCoroutine(AsInvencible());
        StartCoroutine(UITempoInvencible(Spawn.spawn.TimeInvencible));
    }
    public bool valorInvencible()
    {
        return IsInvencible;
    }

    void VerificyBug()
    {
        if(transform.position.x < -2.2f)
        {
            transform.position = new Vector2(-2.15f, transform.position.y);
        }
        if (transform.position.x > 2.3f)
        {
            transform.position = new Vector2(2.3f, transform.position.y);
        }
    }
    public void Destruir()
    {
        StopAllCoroutines();
        StartCoroutine(destruir());
    }

    IEnumerator destruir()
    {
        GameOver = true;
        SpeedBall.SetActive(false);
        GameController.instance.gameover = true;
        gameObject.name = "GameOver";
        gameObject.tag = "Finish";
        GameObject exp = gameObject.transform.GetChild(0).gameObject;
        SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
        render.enabled = false;
        speedball = 0;
        exp.SetActive(true);
        yield return new WaitForSeconds(2);
        GameController.instance.ShowGameOver();
    }

}
