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

    private float tempointervalo = 0.2f;
    private float tempo;
    private float contInv = 0;
    private bool Piscar = false;
    private bool IniCont = false;

    void Awake() {
        instBola = this;
        Anim = GetComponent<Animator>();
    }
    void Update()
    {
        MovimentBall();
        VerificyBug();
        if (IniCont)
        {
            TempoInvencibilidade();
            if (contInv < 4 && contInv > 0)
            {
                PiscarIcon();
            }
        }
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
    void TempoInvencibilidade()
    {
        contInv -= Time.deltaTime;
    }
    void PiscarIcon()
    {
        tempo += Time.deltaTime;
        if(tempo >= tempointervalo)
        {
            if(InvencibleUI.gameObject.activeSelf)
            {
                InvencibleUI.gameObject.SetActive(false);
                Anim.SetBool("IsInvicible", false);
            }
            else
            {
                InvencibleUI.gameObject.SetActive(true);
                Anim.SetBool("IsInvicible", true);
            }
            tempo = 0;
        }
    }

    public bool IsSpeedActive()
    {
        return IsSpeed;
    }

    public IEnumerator AlterVelocityTime(float time, float speed)
    {
        speedball = speed;
        IsSpeed = true;
        VelocityUI.gameObject.SetActive(true);
        SpeedBall.SetActive(true);
        yield return new WaitForSeconds(time);
        VelocityUI.gameObject.SetActive(false);
        SpeedBall.SetActive(false);
        IsSpeed = false;
        speedball = 4;
    }
    public void Invencible()
    {
        StartCoroutine(AsInvencible());
    }
    public bool valorInvencible()
    {
        return IsInvencible;
    }
    IEnumerator AsInvencible()
    {
        contInv = Spawn.spawn.TimeInvencible;
        IniCont = true;
        IsInvencible = true;
        Anim.SetBool("IsInvicible", true);
        InvencibleUI.gameObject.SetActive(true);
        /*
        Transform[] speeds = SpeedBall.GetComponentsInChildren<Transform>();
        foreach(Transform x in speeds)
        {
            try{
                PlayerSpeed players = x.GetComponent<PlayerSpeed>();
                players.ActiveInvencible(true);
            }catch{

            }
        }
        */
        yield return new WaitForSeconds(Spawn.spawn.TimeInvencible);
        /*
        foreach(Transform x in speeds)
        {
            try{
                PlayerSpeed players = x.GetComponent<PlayerSpeed>();
                players.ActiveInvencible(false);
            }catch{

            }
        }
        */
        InvencibleUI.gameObject.SetActive(false);
        Anim.SetBool("IsInvicible", false);
        IsInvencible = false;
        contInv = 0;
        IniCont = false;
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
        StartCoroutine(destruir());
    }

    IEnumerator destruir()
    {
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
