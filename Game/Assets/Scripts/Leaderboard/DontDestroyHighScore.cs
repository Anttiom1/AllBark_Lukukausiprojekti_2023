using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyHighScore : MonoBehaviour
{
    public void Awake()
    {
        GameObject[] uiobjs = GameObject.FindGameObjectsWithTag("hiscoreui");
        if ( uiobjs.Length > 1 )
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }

}
