using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DreamFragment : MonoBehaviour
{
    float snapDistance = 1.5f;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            Debug.LogFormat("player position temple : {0}", GameObject.Find("OVRPlayerController").transform.position);
            if (Vector3.Distance(gameObject.transform.position, GameObject.Find("Cauldron_Tuto").gameObject.transform.position) < snapDistance)
            {
                gameObject.transform.position = GameObject.Find("Cauldron_Tuto").gameObject.transform.position + new Vector3(0, 0.5f, 0);
                StartCoroutine(LoadSceneDelay(2));
            }
        }
        
    }

    IEnumerator LoadSceneDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Scenes/Game");
    }


}
