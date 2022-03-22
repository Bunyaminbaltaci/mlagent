using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodButton : MonoBehaviour
{
    // Start is called before the first frame update
   
    public bool canBeUse;
    private void Awake()
    {
        canBeUse = true;
    }
    public bool CanBeUse()
    {

        return canBeUse;
    }

    public void UseButton()
    {
        Debug.Log("button kullanýldý");


    }
}
