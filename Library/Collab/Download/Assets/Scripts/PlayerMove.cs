using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{

    public float fireRate = 0.7f;
    public short bulletPools = 50;
    public GameObject bullet;
    List<GameObject> bullets;
    public bool willGrow = false;
    public GameObject children;
    // Use this for initialization
    void Start()
    {

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
                bullets[i].SetActive(true);
                bullets[i].transform.position = children.GetComponentInChildren<Transform>().position;
                bullets[i].GetComponent<Rigidbody>().velocity = -transform.forward * 10;
                NetworkServer.Spawn(bullets[i]);
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

        var x = Input.GetAxis("Horizontal") * 0.1f;
        var z = Input.GetAxis("Vertical") * 0.1f;
        transform.Translate(x, 0, z);

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
