using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    public static Spawn spawn;
    [SerializeField]
    private GameObject Obstaculos;
    [SerializeField]
    private GameObject Bonus;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private GameObject Coin;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private float timeCount;
    [SerializeField]
    [Min(0.5f)]
    private float spawnTimeCoin = 2.0f;

    [SerializeField]
    private GameObject[] SpecialBall = new GameObject[7];
    private int NivelSpeed = 0;
    [SerializeField]
    private int NivelSpeedQuant = 5;
    public Text TextNivel;
    private int NivelMax = 150;
    /// <>
    /// SPAWN DECREMENT 
    private float SpawnDecrement = 0.025f;
    private float SpawnInicial = 0.75f;
    /// TIME INVENCIBLE
    public float TimeInvencible {get{return 13;} private set{}}

    // TIME BOMB DESTROY
    private bool bombactive = false;
    private float accumulatedscore = 0;
    
    // TIME SPAWN COIN BONUS
    [SerializeField]
    GameObject SpawnBonusTema;

    private float spawnvelocity = 0.5f;

   
    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        spawn = this;
        spawnTime = SpawnInicial;
        scene = SceneManager.GetActiveScene();
        StartCoroutine(SpawnCoin());
        if (scene.name != "Inicio")
        {
            StartCoroutine(SpawnSpecial());
        }
    }

    void Abort(){
        StopAllCoroutines();
    }
    void SetTextNivelUI()
    {       
        if(scene.name != "Inicio")
        {
            TextNivel.text = "Nivel: " + (NivelSpeed + 1).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnObs();
        CheckVelocitySpawn();
        SetTextNivelUI();
    }
    IEnumerator SpawnCoin()
    {
        while(true){
            GameObject coin = Instantiate(Coin, transform.position, transform.rotation);
            coin.transform.SetParent(Bonus.transform);
            Destroy(coin, 5f);
            yield return new WaitForSeconds(spawnTimeCoin);
        }
    }
    //public GameObject[] SpecialBallteste;
    IEnumerator SpawnSpecial()
    {
        while(true){
            //GameObject especial = Instantiate(SpecialBallteste[Random.Range(0, 2)], transform.position, transform.rotation);
            GameObject especial = Instantiate(SpecialBall[Random.Range(0, SpecialBall.Length)], transform.position, transform.rotation);
            //GameObject especial = Instantiate(SpecialBall[2], transform.position, transform.rotation);
            Obstacle obs = especial.GetComponent<Obstacle>();
            obs.AlterSpeed(Random.Range(2, 5));
            if(Bola.instBola.valorInvencible() == true && especial.name == "ObsInvencivel(Clone)")
            {
                Destroy(especial, 0);
                especial = Instantiate(SpecialBall[1], transform.position, transform.rotation);
            }
            if (bombactive && especial.name == "ObsBomba(Clone)")
            {
                Destroy(especial, 0);
                especial = Instantiate(SpecialBall[1], transform.position, transform.rotation);
                obs.AlterSpeed(4.5f);
            }
            if (Bola.instBola.IsSpeedActive() && especial.name == "ObsSpeed(Clone)")
            {
                Destroy(especial, 0);
                especial = Instantiate(SpecialBall[1], transform.position, transform.rotation);
                obs.AlterSpeed(Random.Range(3.5f, 5));
            }
            if (especial.name == "ObsBonus5(Clone)")
            {
                obs.AlterSpeed(5);
            }
            if (especial.name == "ObsCoinBonus(Clone)")
            {
                if (SpawnBonusTema.activeSelf)
                {
                    Destroy(especial, 0);
                    especial = Instantiate(SpecialBall[1], transform.position, transform.rotation);
                    obs.AlterSpeed(5);
                }
            }
            especial.transform.SetParent(Bonus.transform);
            Destroy(especial, 5);
            yield return new WaitForSeconds(Random.Range(2, 6));
            //Random.Range(2, 6)
        }
    }

    void CheckVelocitySpawn()
    {
        if (GameController.instance.pontuacao > NivelSpeedQuant)
        {
            if (!(NivelSpeed == 14))
            {
                NivelSpeedQuant += 5;
                NivelSpeed++;
                if(bombactive)
                {
                    accumulatedscore += SpawnDecrement;
                }
                else
                {
                    spawnTime -= accumulatedscore;
                    spawnTime -= SpawnDecrement;
                    accumulatedscore = 0;
                }
            }
        }
    }
    public void DownVelocitySpawn()
    {
        if (NivelSpeed > 0)
        {
            NivelSpeedQuant+=5;
            NivelSpeed--;
            spawnTime += SpawnDecrement;
        }
    }
    private void SpawnObs()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= spawnTime){
            GameObject go = Instantiate(prefab, transform.position, transform.rotation);
            go.transform.SetParent(Obstaculos.transform);
            Destroy(go, 5f);
            timeCount = 0;
        }
    }
    public void AllCollider()
    {
        StartCoroutine(TimeSpawn());
        Transform[] get = Obstaculos.GetComponentsInChildren<Transform>();
        Transform[] getbomb = Bonus.GetComponentsInChildren<Transform>();
        
        foreach(Transform valor in get)
        {
            if (valor.name == "Obs(Clone)")
            {
                Obstacle obs = valor.GetComponent<Obstacle>();
                obs.IniciarDestruicao();
            }
            //Destroy(valor);
        }
        foreach (Transform valor in getbomb)
        {
            if(valor.name == "ObsBomba(Clone)")
            {
                Obstacle obs = valor.GetComponent<Obstacle>();
                obs.IniciarDestruicaoBomba();
            }
        }
    }
    IEnumerator TimeSpawn()
    {
        float valor = spawnTime;
        spawnTime = 3;
        bombactive = true;
        yield return new WaitForSeconds(3);
        spawnTime = valor;
        bombactive = false;
    }

    public void IniciarTimeSpawn()
    {
        StartCoroutine(TimeSpawnCoinBonus());
    }
    IEnumerator TimeSpawnCoinBonus()
    {
        spawnTimeCoin = spawnvelocity;
        SpawnBonusTema.SetActive(true);
        yield return new WaitForSeconds(10);
        SpawnBonusTema.SetActive(false);
        spawnTimeCoin = 2.0f;
    }
}
