using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{

    private Rigidbody2D rig;
    [SerializeField]
    [Min(0)]
    private float SpeedBall = 2f;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(-145, -45)));
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.MovePosition(transform.position + transform.right * SpeedBall * Time.deltaTime);
    }
}
