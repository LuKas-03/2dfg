using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SECRET_SwitchScene : MonoBehaviour {
    [SerializeField] private string nextLevel;

    void OnTriggerStay2D(Collider2D other)    {
        if ((other.CompareTag("Player")) && (Input.GetAxis("Vertical")>0))
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
