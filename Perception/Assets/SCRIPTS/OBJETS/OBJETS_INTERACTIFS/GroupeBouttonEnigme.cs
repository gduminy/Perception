﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupeBouttonEnigme : MonoBehaviour {

    private int numEnigmeActuel;
    public List<ScriptableBoutonEnigmes> enigme;
    [HideInInspector]
    public bool solvableActuel;


    public List<int> bouttonAppuyer;

    private float time;
    private int timeCombinaisonCooldown;
    [HideInInspector]
    public List<ButtonEnigme> buttonEnigmes = new List<ButtonEnigme>();

    public AudioClip sonCorrect;
    public AudioClip sonIncorrect;
    private AudioSource audioSource;
    // Use this for initialization
    public bool correctCombinaison;
    void Start () {
        correctCombinaison = false;
        this.audioSource = GetComponent<AudioSource>();
        bouttonAppuyer = new List<int>();
        timeCombinaisonCooldown = 5;
        time = 0.0f;
        numEnigmeActuel = 0;
        if(enigme.Count>0)
        {
            solvableActuel = enigme[0].solvable;

        }
        

    }

    private void Update()
    {
        if (bouttonAppuyer.Count>0)
        {
            
            if (time+timeCombinaisonCooldown < Time.time)
            {
                if (!correctCombinaison)
                {
                    this.audioSource.PlayOneShot(sonIncorrect);
                }
                else
                {
                    correctCombinaison = false;
                }
                
                endLightAllButton();
                bouttonAppuyer = new List<int>();
                time = 0.0f;
            }
            
        }
    }

    private void endLightAllButton()
    {
        foreach (ButtonEnigme be in buttonEnigmes)
        {
            be.button.GetComponent<Button>().DesactivateLight();
        }
    }

    public void TestCombinaison()
    {
        ScriptableBoutonEnigmes enigmeActuel = getEnigmeActuel();

        bool combinaisonEchec = false;

        if (solvableActuel)
        {
            if (enigmeActuel.listeCombinaison.Count == bouttonAppuyer.Count)
            {
                for (int i = 0; i < bouttonAppuyer.Count; i++)
                {
                    if (enigmeActuel.listeCombinaison[i] != bouttonAppuyer[i])
                    {
                        combinaisonEchec = true;
                    }

                }
            }
            else
            {
                correctCombinaison = false;
                combinaisonEchec = true;
            }

            if (!combinaisonEchec)
            {
                LaunchEventReponse(enigmeActuel.bonnes);
                correctCombinaison = true;
                this.audioSource.PlayOneShot(sonCorrect);
                prochaineEnigme();
            }
        }
        else
        {
            this.audioSource.PlayOneShot(sonIncorrect);
            LaunchEventReponse(enigmeActuel.nonSolvable);
        }




    }


    public void TestOneButton(int numero)
    {

        if (solvableActuel)
        {
            if (enigme[numEnigmeActuel].boutonCorrect == numero)
            {
                LaunchEventReponse(enigme[numEnigmeActuel].bonnes);
                this.audioSource.PlayOneShot(sonCorrect);
                prochaineEnigme();
            }
            else
            {
                this.audioSource.PlayOneShot(sonIncorrect);
                LaunchEventReponse(enigme[numEnigmeActuel].mauvaises);
            }
        }
        else
        {
            LaunchEventReponse(enigme[numEnigmeActuel].nonSolvable);
        }



    }

    public void addCombinaison(int numero)
    {
        if (!(bouttonAppuyer.Contains(numero)))
        {
            bouttonAppuyer.Add(numero);
            Button be = findButton(numero);
            if (be != null)
            {
                be.ActivateLight();
            }
            time = Time.time;
        }
        
    }

    private Button findButton(int numero)
    {
        foreach (ButtonEnigme be in buttonEnigmes)
        {
            if (be.indice==numero)
            {
                return be.button.GetComponent<Button>();
            }
        }
        return null;
    }

    public void LaunchEventReponse(string bouton)
    {
        foreach (EventManager eM in GetComponents<EventManager>())
        {
            if (eM.nomEvent.Equals(bouton))
            {
                eM.activation();
            }
        }
    }


    private void prochaineEnigme()
    {
        if (enigme.Count> numEnigmeActuel+1)
        {
            numEnigmeActuel++;
            solvableActuel = enigme[numEnigmeActuel].solvable;
        }

    }

    public ScriptableBoutonEnigmes getEnigmeActuel()
    {
        return enigme[numEnigmeActuel];
    }

    public TypeEnigmeButton getTypeEnigmeActuel()
    {
        return getEnigmeActuel().typeEnigme;
    }



}

public enum TypeEnigmeButton
{
    Groupe,
    Combinaison,

}
