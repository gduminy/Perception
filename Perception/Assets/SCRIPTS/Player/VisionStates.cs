﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum VisionStates
{
    BlackWhiteVision,
    GreenVision,
    RedVision,
    BlueVision,
    ChromaVision,
    ThermalVision,
    NightVision,
    MidBlurVision
}

public class BlackWhiteVision : State
{
    public  BlackWhiteVision(GameObject target) : base(target) { }

    public override void Enter()
    {
        target.GetComponentInChildren<ShaderEffect>().enabled = true;
        Material m = target.GetComponentInChildren<ShaderEffect>().material;

        m.SetInt("_GrayScale", 0);
        m.SetInt("_RedChannel", 0);
        m.SetInt("_GreenChannel", 0);
        m.SetInt("_BlueChannel", 0);
    }

    public override void Exit()
    {
       // target.GetComponentInChildren<ShaderEffect>().enabled = false;
    }
}

public class GreenVision : State
{

    public float lerp = 0f, duration = 50f;
    public GreenVision(GameObject target) : base(target) { }

    public override void Execute() {


        Material m = target.GetComponentInChildren<ShaderEffect>().material;
       
            if(m.GetInt("_RedChannel") == 0 && m.GetInt("_BlueChannel") == 0)
            {
                m.SetInt("_GreenChannel", 1);
                float score = m.GetFloat("_GrayScale");
                lerp += Time.deltaTime / duration;
                score = Mathf.Lerp(score,1,lerp); 
                m.SetFloat("_GrayScale", score);
                if(m.GetFloat("_GrayScale") >= 0.999f){
                        m.SetFloat("_GrayScale" , 1);
                    }
            }
            else
            {
                float score = m.GetFloat("_GreenChannel");
                lerp += Time.deltaTime / duration;
                score = Mathf.Lerp(score,1,lerp); 
                m.SetFloat("_GreenChannel", score);
                if(m.GetFloat("_GreenChannel") >= 0.999f){
                        m.SetFloat("_GreenChannel" , 1);
                    }
            }
        
    
     }
    public override void Exit() { }
}

public class BlurVision : State
{
    public float lerp = 0f, duration = 50f;
    public float flou = 30f;
    
    public BlurVision(GameObject target) : base(target) { }
    
    Material m = GameObject.Find("FPSController/FirstPersonCharacter/Canvas/BlurImage").GetComponent<UnityEngine.UI.Image>().material;
    public override void Execute() {
        float score = m.GetFloat("_Size");
                lerp += Time.deltaTime / duration;
                score = Mathf.Lerp(score,flou,lerp); 
                m.SetFloat("_Size", score);
     }
    public override void Exit() { }
}

public class LostBlurVision : State
{
    public float lerp = 0f, duration = 50f;
    public float flou = 0f;
    
    public LostBlurVision(GameObject target) : base(target) { }
    
    Material m = GameObject.Find("FPSController/FirstPersonCharacter/Canvas/BlurImage").GetComponent<UnityEngine.UI.Image>().material;
    public override void Execute() {
        float score = m.GetFloat("_Size");
                lerp += Time.deltaTime / duration;
                score = Mathf.Lerp(score,flou,lerp); 
                m.SetFloat("_Size", score);
     }
    public override void Exit() { }
}


public class RedVision : State
{
    public float lerp = 0f, duration = 50f;
    
    public RedVision(GameObject target) : base(target) { }

    public override void Enter() {  
        

    }   

    public override void Execute(){
         Material m = target.GetComponentInChildren<ShaderEffect>().material;
       
            if(m.GetFloat("_GreenChannel") == 0 && m.GetFloat("_BlueChannel") == 0)
            {
                m.SetInt("_RedChannel", 1);
                float score = m.GetFloat("_GrayScale");
                lerp += Time.deltaTime / duration;
                score = Mathf.Lerp(score,1,lerp); 
                m.SetFloat("_GrayScale", score);
                if(m.GetFloat("_GrayScale") >= 0.999f){
                        m.SetFloat("_GrayScale" , 1);
                    }
                
            }
            else
            {
                
                float score = m.GetFloat("_RedChannel");
                lerp += Time.deltaTime / duration;
                score = Mathf.Lerp(score,1,lerp); 
                m.SetFloat("_RedChannel", score);
                if(m.GetFloat("_RedChannel") >= 0.999f){
                        m.SetFloat("_RedChannel" , 1);
                    }
            }
        
    }
    
    public override void Exit() { }
}

public class BlueVision : State
{
    public float lerp = 0f, duration = 50f;
    public BlueVision(GameObject target) : base(target) { }

    public override void Execute() { 
         Material m = target.GetComponentInChildren<ShaderEffect>().material;

            if(m.GetInt("_GreenChannel") == 0 && m.GetInt("_RedChannel") == 0)
            {
                m.SetInt("_BlueChannel", 1);
                float score = m.GetFloat("_GrayScale");
                lerp += Time.deltaTime / duration;
                score = Mathf.Lerp(score,1,lerp); 
                m.SetFloat("_GrayScale", score);
                if(m.GetFloat("_GrayScale") >= 0.999f){
                        m.SetFloat("_GrayScale" , 1);
                    }
            }
            else
            {
                float score = m.GetFloat("_BlueChannel");
                    lerp += Time.deltaTime / duration;
                    score = Mathf.Lerp(score,1,lerp); 
                    m.SetFloat("_BlueChannel", score);
                    if(m.GetFloat("_BlueChannel") >= 0.999f){
                        m.SetFloat("_BlueChannel" , 1);
                    }
            }
        
    }
    public override void Exit() { }
}

public class ChromaVision : State
{
    
    public ChromaVision(GameObject target) : base(target) { }

    public override void Enter() { }
    public override void Exit() { }
}