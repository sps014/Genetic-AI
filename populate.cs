using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class populate : MonoBehaviour
{
    List<GameObject> cubes = new List<GameObject>();
    public int popSize = 100;
    public GameObject cube;
    public static float LifeTime = 400;
    int counter=0;
    public int gen = 0;
    public GameObject Target;
    public Text genText;
    public Text counterText;
    void Start()
    {
        counter = 0;
        cubes = new List<GameObject>();
        for (int i = 0; i < popSize; i++)
        {
            cubes.Add(Instantiate(cube, transform));
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(counter<LifeTime)
        {
            counter++;
            counterText.text = "LifeTime: " + counter;
        }
        else
        {
            CreateNewGen();
            genText.text = "Gen: " + ++gen;
        }

        evalFitness().GetComponent<Renderer>().material.SetColor("_EmissionColor",Color.green);
    }
    public static Color getCol()
    {
        string seed = Time.time.ToString();
        System.Random random = new System.Random(seed.GetHashCode());
        Color background = new Color(
            (float)random.Next(0, 255),
            (float)random.Next(0, 255),
            (float)random.Next(0, 255)
        );
        return background;
    }
    private void CreateNewGen()
    {
        GameObject maxFit = evalFitness();
        List<Vector3> genes = maxFit.GetComponent<cubes>().Genes;
        int score = maxFit.GetComponent<cubes>().Fitness;
        DestroyAll();
        cubes.Clear();
        counter = 0;
        for (int i = 0; i < popSize; i++)
        {
            cubes.Add(Instantiate(cube, transform));
            cubes[i].GetComponent<cubes>().ModifyGenes(genes);
        }
    }

    private void DestroyAll()
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            Destroy(cubes[i]);
        }
    }

    private GameObject evalFitness()
    {
        GameObject maxFit = cubes[0];
        for (int i = 0; i < cubes.Count; i++)
        {
            if (Vector3.Distance(maxFit.transform.position, Target.transform.position) > Vector3.Distance(cubes[i].transform.position, Target.transform.position))
            {
                maxFit = cubes[i];
            }
            //if (cubes[i].GetComponent<cubes>().Fitness>maxFit.GetComponent<cubes>().Fitness)
            //{
            //    maxFit = cubes[i];
            //}
        }
        return maxFit;
    }
}
