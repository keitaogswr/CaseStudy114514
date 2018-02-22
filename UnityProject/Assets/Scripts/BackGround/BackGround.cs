using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {

    public Renderer render;
    [Range(0, 1)]
    public float speed;

    public float fadeSpeed;
    public float waitTime;
    float countWaitTime;
    
    public Material finalFloorMaterial;

    Material floorMaterial;
    float materialColor;
    public bool isFinalFloor;

	// Use this for initialization
	void Start () {
        countWaitTime = 0.0f;

        floorMaterial = render.material;
        materialColor = floorMaterial.color.a;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(isFinalFloor)
        {
            if(floorMaterial == render.material)
            {
                materialColor -= Time.deltaTime * fadeSpeed;
                if(materialColor <= 0.0f)
                {
                    materialColor = 0.0f;
                    countWaitTime += Time.deltaTime;
                    if(countWaitTime >= waitTime)
                    {
                        render.material = finalFloorMaterial;
                        SoundManager.PlayBGM("yuusaku");
                    }                                    
                }
            }
            else
            {
                if(materialColor <= 1.0f)
                {
                    materialColor += Time.deltaTime * fadeSpeed;
                    if (materialColor >= 1.0f)
                    {
                        materialColor = 1.0f;
                    }
                }
            }

            render.material.color = new Color(materialColor,
            materialColor,
            materialColor, 1);
            return;
        }
        float scroll = Mathf.Repeat(Time.time* speed, 1);
        Vector2 offset = new Vector2(0, scroll);
        render.sharedMaterial.SetTextureOffset("_MainTex",offset);
	}

    public void GoLastBattle()
    {
        isFinalFloor = true;
    }
}
