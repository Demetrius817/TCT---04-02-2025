using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour {

    private bool isFirstUpdate = true;


    private void Update() {
        if (isFirstUpdate == true) {
            StartCoroutine(DelayCheckFirstUpdate());

        }
    }

    IEnumerator DelayCheckFirstUpdate() {
        // suspend execution for x seconds
        yield return new WaitForSeconds(1.5f);
        isFirstUpdate = false;
        Loader.LoaderCallBack();
    }
}


    

