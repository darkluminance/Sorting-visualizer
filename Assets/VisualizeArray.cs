using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualizeArray : MonoBehaviour
{
    public Slider elementsfixer;
    public Text elements;
    public GameObject square;
    public Camera maincam;

    public GenerateArray array;

    public GameObject[] sqstore;

    private int limit, max;

    private float leftmost;
    // Start is called before the first frame update
    void Start()
    {
        maincam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //visualize();   
    }

    public void visualize()
    {
        int value =(int) elementsfixer.value;
        elements.text = "No of elements: " + value;
        for (int i = 0; i < limit; i++)
        {
            sqstore[i].transform.position = new Vector3(leftmost + i*0.5f, 0, 1);
            sqstore[i].transform.localScale = new Vector3(0.9f, 15f * ((float)limit / 30f)* (array.numbers[i]/(float)max), 1);
            maincam.orthographicSize = ((float) limit / 30f) * 5f;
            maincam.transform.position =new Vector3(0,(float) limit / 30f * 5f, -10);
        }
    }

    public void createvisualizer()
    {
        limit = array.amount;
        max = array.maxnum;
        sqstore = new GameObject[limit];
        for (int i = 0; i < limit; i++)
        {
            GameObject go = Instantiate(square) as GameObject;
            sqstore[i] = go;
        }

        leftmost = -0.5f * limit/2;   
    }

    public void destroyarrayvisualize()
    {
        for (int i = 0; i < limit; i++)
        {
            Destroy(sqstore[i].gameObject);
        }
    }

    public void close()
    {
        Application.Quit();
    }
}
