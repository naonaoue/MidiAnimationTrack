using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] Material[] color;
    public NoteCntr _NoteCntr;
    public float Tick;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = color[0];

    }

    // Update is called once per frame
    void Update()
    {
        if (_NoteCntr.RealTimeInTicks >= Tick)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = color[1];
        }
        if(_NoteCntr.RealTimeInTicks >= (Tick*60f/125.95f/480f+1f)*480f*125.95f/60f)
        {
            Destroy(this.gameObject);
        }
    }
}
