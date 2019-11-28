using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    private Transform bar;
    private void Start()
    {
        if(bar == null) bar = transform.Find("Bar");
    }

    public void SetHealthPercent(float newPercent){
        //Prevent wrong input
        if(newPercent < 0f || newPercent > 1f) return;
        bar.localScale = new Vector3(newPercent, 1f, 1f);
    }

    public void SetHealthColor(Color color){
        bar.Find("HealthColor").GetComponent<SpriteRenderer>().color = color;
    }

    
}
