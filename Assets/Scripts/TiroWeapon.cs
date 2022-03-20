using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroWeapon : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(0, 1, 0) * 7 * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Obs(Clone)")
        {
            Obstacle obs = other.transform.GetComponent<Obstacle>();
            obs.IniciarDestruicao();
            Destroy(gameObject);
        }
    }

}
