using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destrucc : MonoBehaviour
{
    public GameObject txtUI;
    private GameObject triggeringPMP;
    private GameObject Bp;
    private Movement mov;

    void Start()
    {
        Bp = GameObject.FindGameObjectWithTag("Player");
        txtUI.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "pmp")
        {
            triggeringPMP = other.gameObject;
            triggeringPMP.GetComponent<pmp>().Destroypmp();
            Bp.GetComponent<Movement>().AddBodyPart();
            
        }
        if(other.gameObject.tag == "wall")
        {
            txtUI.SetActive(true);
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "Player")
        {
            txtUI.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
