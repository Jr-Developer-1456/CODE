using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject slot1;
    public GameObject slot2;
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Equip2();
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            Equip1();
        }
    }
    public void Equip1()
    {
        slot1.SetActive(true);
        slot2.SetActive(false);
    }
    public void Equip2()
    {
        slot1.SetActive(false);
        slot2.SetActive(true);
    }
}
