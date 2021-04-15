using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BtnBest : MonoBehaviour {
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI panelText;
    private Animator animator;
    private StopWatchManager sw;
    private RecordManager recordManager;

    void Start() {
        animator = panel.GetComponent<Animator>();
        sw = FindObjectOfType<StopWatchManager>();
        recordManager = FindObjectOfType<RecordManager>();
    }

    public void OpenBestPanel() {
        bool isOpen = animator.GetBool("open");  // check if the panel is open
        animator.SetBool("open", !isOpen);  // reverse value to play animation
        sw.Stop(); // stop the stopwatch

        string text = "";

        int i = 0;
        foreach (var s in recordManager.best_scores) {
            i++;
            text = text + $"{i}) {s} \n";
        }

        panelText.text = text;
    }
}
