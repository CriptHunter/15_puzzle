using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RecordManager : MonoBehaviour
{
    [SerializeField] public List<string> best_scores;
    [SerializeField] private int maxSize = 15;

    void Start() {
        best_scores = new List<string>();
    }

    public void AddEntry(string entry) {
        best_scores.Add(entry);
        best_scores.Sort();

        if(best_scores.Count > maxSize) // if we added an element to a full list remove the last element, that is the worst score
            best_scores.Remove(best_scores.Last());
    }

    public string ListToString() {
        return string.Join("\n", best_scores);
    }
}
