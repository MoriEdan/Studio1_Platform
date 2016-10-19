using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;

public class Data : MonoBehaviour {
    public enum DataType {
        JSON,
        XML,
        TXT,
        INI,
        CSV
    }

    public JObject ReadFromJObject(TextAsset file) {
        return JObject.Parse(file.text);
    }

    public JArray ReadFromJArray(TextAsset file) {
        return JArray.Parse(file.text);
    }

    public void WriteJObjectToFile(TextAsset file, JObject data) {
        File.WriteAllText(new FileInfo(file.name + ".json").FullName, data.ToString());
    }

    public void WriteJArrayToFile(TextAsset file, string path, JArray data) {
        File.WriteAllText(new FileInfo(file.name + ".json").Directory.FullName + "/" + path + file.name + ".json", data.ToString());
    }
}
