using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class RecordManager : MonoBehaviour
{
    [SerializeField] public List<string> best_scores;
    [SerializeField] private int maxSize = 15;

    void Start() {
        best_scores = new List<string>();
        DeSerializeFromJson();
        print(Application.persistentDataPath);
    }

    public void AddEntry(string entry) {
        best_scores.Add(entry);
        best_scores.Sort();

        if(best_scores.Count > maxSize) // if we added an element to a full list remove the last element, that is the worst score
            best_scores.Remove(best_scores.Last());

        SerializeToJson();
    }

    string SerializeToJson() {
        ListContainer<string> container = new ListContainer<string>(best_scores); // wrap list in a container
        string json = JsonUtility.ToJson(container); // container to json
        System.IO.File.WriteAllText(Application.persistentDataPath + "/bestScores.json", json); // write json to file
        return json;
    }

    string DeSerializeFromJson() {
        string json = "";
        try {
            json = System.IO.File.ReadAllText(Application.persistentDataPath + "/bestScores.json"); // read json from file
            ListContainer<string> container = JsonUtility.FromJson<ListContainer<string>>(json); // parse json
            best_scores = container.dataList;
        }

        catch(FileNotFoundException e) {
            print("No score saved");
            return "";
        }
        
        return json;
    }
}

// you can't directly serialize a list, a container is needed
public struct ListContainer<T> {
    public List<T> dataList;

    public ListContainer(List<T> _dataList) {
        dataList = _dataList;
    }
}
