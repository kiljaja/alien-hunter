using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    private Transform bar;
    private SpriteRenderer spriteRenderer;
    private void Awake(){
        if(bar == null) bar = transform.Find("Bar");
        if(spriteRenderer == null) spriteRenderer = bar.Find("HealthColor").GetComponent<SpriteRenderer>();
    }

    public void SetHealthPercent(float newPercent){
        //Prevent out of bounds input
        if(newPercent < 0f || newPercent > 1f) return;
        bar.localScale = new Vector3(newPercent, 1f, 1f);
    }

    public void SetHealthColor(Color color){
        spriteRenderer.color = color;
    }

    
}
