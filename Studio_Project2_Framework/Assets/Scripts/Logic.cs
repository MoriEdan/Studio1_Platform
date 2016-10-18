using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class Logic : MonoBehaviour {

    [SerializeField]
    InputField cardInput;

    [SerializeField]
    TextAsset cardsFile;

    [SerializeField]
    PlayVLC videoPlayer;

    private Data dataLoader;

    private string CardCode;
    private string[] videoPaths;

    private int[][] videoMetric;
    private int cityQuality = 0,
        contraband = 0,
        reception = 0;

    // Use this for initialization
    void Start ()
    {
        dataLoader = GetComponent<Data>();
        GetBestMatch();
	}
	
	void ReadCardInformation ()
    {
        string cardsFileText = dataLoader.ReadFromJArray(cardsFile).ToString();
        string[] cardsFileLines = File.ReadAllLines(cardsFileText);
        foreach (string line in cardsFileLines)
        {
            if (line.Contains(CardCode))
            {
                cityQuality += 0;
                contraband += 0;
                reception += 0;
                Debug.Log("Yay");
            }

        }
    }

    void GetBestMatch ()
    {
        int selectedVideoNumber = 0;
        int currentBestFit = 0;
        for (int videoIndex = 0; videoIndex < videoMetric.Length; ++videoIndex)
        {
            int currentfit = 0;
            if (videoMetric[0][videoIndex] == cityQuality)
                currentfit++;
            if (videoMetric[1][videoIndex] == contraband)
                currentfit++;
            if (videoMetric[2][videoIndex] == reception)
                currentfit++;

            if (currentfit > currentBestFit)
            {
                selectedVideoNumber = videoIndex;
                currentBestFit = currentfit;
            }

            if (currentBestFit == 3)
                break;
        }

        videoPlayer.VideoPath = videoPaths[selectedVideoNumber];
    }

    public void GetCardCode ()
    {
        CardCode = cardInput.text;
        ReadCardInformation();
    }
}