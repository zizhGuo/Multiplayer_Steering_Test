using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsDestroy : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        
    }
    private void OnEnable()
    {
        Invoke("Destroy", 2f);
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
