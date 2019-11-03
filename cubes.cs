using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cubes : MonoBehaviour
{
    // Start is called before the first frame update
    public int Fitness;
    public List<Vector3> Genes = new List<Vector3>();
    public float maxForce = 1;
    private int counter = 0;
    private bool die=false;

    void Start()
    {
        transform.position = Vector3.zero + Vector3.up * 0.5f;
        if (Genes.Count != 0)
            return;
        Genes.Clear();
        for (int i = 0; i < populate.LifeTime; i++)
        {
            Random.InitState(i*System.DateTime.Now.Millisecond);
            Genes.Add(new Vector3(Random.Range(0,5), 0, Random.Range(-10, 10)) *maxForce);
        }
        GetComponent<Renderer>().material.SetColor("_EmissionColor",Color.yellow);
    }
    public void ModifyGenes(List<Vector3> genes)
    {
        Genes.Clear();
        for (int i = 0; i < genes.Count; i++)
        {
            if(Random.value>0.11f)
            Genes.Add(genes[i]);
            else
            {
                Genes.Add(new Vector3(Random.Range(0, 5)*maxForce, 0, Random.Range(-20, 20)*maxForce));
            }
        }
        for (int i = genes.Count; i < populate.LifeTime; i++)
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            Genes.Add(new Vector3(Random.Range(0, 5)*maxForce, 0, Random.Range(-20, 20)) * maxForce);
        }
    }

    void Update()
    {
        if(transform.position.y>=0 && !die)
        {
            if (counter < populate.LifeTime)
            {
                if(GetComponent<Renderer>().material.GetColor("_EmissionColor")!=Color.green)
                GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
                transform.Translate(Genes[counter++]);
            }
            else
            {
                GetComponent<Renderer>().material.SetColor("_EmissionColor",Color.white);
                die = true;
            }
        }
        else
        {
            die=true;
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
        }
        Fitness = counter;

    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if(collision.gameObject.tag!="ground" && collision.gameObject.tag!="cubes")
        {
            die = true;
        }
        if(collision.gameObject.name=="target")
        {
            collision.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Random.ColorHSV());
        }
    }

}
