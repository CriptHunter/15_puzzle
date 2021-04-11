using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StopWatchManager : MonoBehaviour {

    public Stopwatch sw;
    public bool running;
    [SerializeField] TextMeshProUGUI  txt;

    void Start() {
        Reset();
    }

    public void Go() {
        sw.Start();
        StartCoroutine("UpdateStopWatchText");
        running = true;
    }

    // reset the timer to start a new game
    public void Reset() {
        sw = new Stopwatch();
        running = false;
        txt.text = "00:00:00";
    }

    // update the timer slower than  unity Update() method
    IEnumerator UpdateStopWatchText() {
        for(;;) {
            txt.text = sw.Elapsed.ToString(@"mm\:ss\:ff");
            yield return new WaitForSeconds(.1f);
        }
    }
}
