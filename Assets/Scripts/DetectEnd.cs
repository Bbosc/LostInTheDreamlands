using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnd : MonoBehaviour
{

    
    static protected GameObject[] fragment_list;
    public int nb_tot_frag = 1;
    public GameObject temple;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int nb_frag_snap = 0;
        fragment_list = GameObject.FindGameObjectsWithTag("fragment");
        for (int i = 0; i < fragment_list.Length; ++i){
            if (fragment_list[i].GetComponent<goToCauldron>().snapped){
                nb_frag_snap = nb_frag_snap + 1;
            }
        }

        Debug.Log(nb_tot_frag);
        Debug.Log("force");
        if (nb_frag_snap == nb_tot_frag){
            Debug.Log("forceeeeee");
            Debug.Log(nb_frag_snap);
        }
        
    }
}
