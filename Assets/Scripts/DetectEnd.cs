using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DetectEnd : MonoBehaviour
{

    
    static protected GameObject[] fragment_list;
    int nb_tot_frag = 5;
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

        if (nb_frag_snap == nb_tot_frag){
            Debug.Log(nb_frag_snap);
            SceneManager.LoadScene("Scenes/EndScene");

        }
        
    }
}
