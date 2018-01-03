using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{

    public float fireRate = 0.7f;
    public short bulletPools = 5;
    public GameObject bullet;
    List<GameObject> bullets;
    public bool willGrow = false;
    public GameObject children;
    public int speed;
    private float x2;
    private float z2;
    private float startTime;
    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        speed = 10;
        bullets = new List<GameObject>();
        for (int i = 0; i < bulletPools; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);

            obj.SetActive(false);
            bullets.Add(obj);
        }
    }

    // Update is called once per frame

    [Command]
    void CmdFire()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                NetworkServer.Spawn(bullets[i]);
                bullets[i].SetActive(true);
                bullets[i].transform.position = children.GetComponentInChildren<Transform>().position;
                bullets[i].GetComponent<Rigidbody>().velocity = -transform.forward * 10;

                break;
            }
        }
    }
    //public GameObject GetPoiledObject()
    //{
    //    for (int i =0; i< bullets.Count;i++)
    //    {
    //        if (!bullets[i].activeInHierarchy)
    //        {
    //            return bullets[i];
    //        }
    //        if (willGrow)
    //        {
    //            GameObject obj = Instantiate(bullet);
    //            bullets.Add(obj);
    //            return obj;
    //        }
    //        return null;
    //    }
    //}
    void Update()
    {
        if (!isLocalPlayer)
            return;
        ///<summary>
        ///v1 steering control without lerp
        /// </summary>
        var x1 = Input.GetAxis("Horizontal") * 0.1f;
        //x2 = Mathf.Lerp(x2, x1, speed*(Time.time - startTime));
        x2 = Mathf.Lerp(x2, x1, 0.5f);

        var z1 = Input.GetAxis("Vertical") * 0.1f;
        //z2 = Mathf.Lerp(z2, z1, speed * (Time.time - startTime));
        z2 = Mathf.Lerp(z2, z1, 0.5f);
        transform.Translate(x2, 0, z2);

        ///<sumary>
        ///v2 sterring control without lerp
        /// </sumary>
        //Vector3 offset = Vector3.zero;
        //if (Input.GetKey(KeyCode.A))
        //    offset.x -= 1;
        //if (Input.GetKey(KeyCode.D))
        //    offset.x += 1;
        //if (Input.GetKey(KeyCode.W))
        //    offset.z += 1;
        //if (Input.GetKey(KeyCode.S))
        //    offset.z -= 1;
        //offset = offset.normalized;
        //GetComponent<Rigidbody>().MovePosition(transform.position + offset * speed * Time.deltaTime);

        ///<summary>
        ///Fire
        /// </summary>
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Fire();
            CmdFire();
        }
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
