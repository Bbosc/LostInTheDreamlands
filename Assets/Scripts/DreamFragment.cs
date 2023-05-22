using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DreamFragment : MonoBehaviour
{
    float snapDistance = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogFormat("position of the fragment : {0}", gameObject.transform.position);
        Debug.LogFormat("position of the cauldron : {0}", GameObject.Find("Cauldron_Tuto").gameObject.transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, GameObject.Find("Cauldron_Tuto").gameObject.transform.position) < snapDistance)
        {
            Debug.LogFormat("position of the fragment : {0}", gameObject.transform.position);
            Debug.LogFormat("position of the cauldron : {0}", GameObject.Find("Cauldron_Tuto").gameObject.transform.position);
            gameObject.transform.position = GameObject.Find("Cauldron_Tuto").gameObject.transform.position + new Vector3(0,0.5f,0);
            StartCoroutine(LoadSceneDelay(2));
        }
        
    }

    IEnumerator LoadSceneDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Scenes/Game");
    }


}
