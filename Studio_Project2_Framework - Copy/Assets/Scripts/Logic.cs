using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

public class Logic : MonoBehaviour {
    public class Card
    {
        public string
            code,
            name,
            type;
        public int
            fixer_point,
            pimp_point,
            sleeper_point;
        public string card;
        public int bonus;
    }

    public class Video
    {
        public float ID;
        public string videoID;
        public string code;
    }

    [SerializeField]
    InputField cardInput;

    [SerializeField]
    TextAsset
        cardsFile,
        videosFile;

    [SerializeField]
    PlayVLC videoPlayer;

    private string CardCode;
    private string[] videoPaths;

    private int fixer = 0,
        pimp = 0,
        sleeper = 0,
        incidentIndex = 0;

    public List<Card> cards;
    public List<Video> videos;
    public string currIncident;


    // Use this for initialization
    void Start ()
    {
        Data dataLoader = GetComponent<Data>();
        string cardsJson = dataLoader.ReadFromJArray(cardsFile).ToString();
        cards = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Card>>(cardsJson);

        string videosJson = dataLoader.ReadFromJArray(videosFile).ToString();
        videos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Video>>(videosJson);
	}

    public void GetCardCode ()
    {
        CardCode = cardInput.text;
        cardInput.text = "";

        foreach(Card card in cards)
        {
            if(card.code == CardCode)
            {
                //Read incident cards
                if (card.type == "incident")
                {
                    currIncident = card.code;
                    incidentIndex++;
                    foreach (Video video in videos) if (video.code == currIncident)
                        {
                            videoPlayer.VideoPath = video.videoID;
                            videoPlayer.Play();
                        } 
                }


                //Divert Attention
                DetermineVideo("RCW5", "8RCM", "98ML", 1.1f);
                DetermineVideo("WCTF", "N28Q", "98ML", 1.2f);
                DetermineVideo("HZKZ", "QLQD", "98ML", 1.3f);

                //I do not Recall
                if(CardCode == "LKJU" && GetRandomNumber(1, 3) == 1) PlayVideo(11.1f);
                if(CardCode == "LKJU" && GetRandomNumber(1, 3) == 2) PlayVideo(11.2f);
                if(CardCode == "LKJU" && GetRandomNumber(1, 3) == 3) PlayVideo(11.3f);

                //Take the blame
                DetermineVideo("RCW5", null, "XEET", 13.1f);
                DetermineVideo("8RCM", null, "XEET", 13.2f);
                DetermineVideo("WCTF", null, "XEET", 13.3f);
                DetermineVideo("N28Q", null, "XEET", 13.4f);
                DetermineVideo("HZKZ", null, "XEET", 13.5f);
                DetermineVideo("QLQD", null, "XEET", 13.6f);

                //Direct Address
                DetermineVideo("RCW5", null, "VF2Y", 14.1f);
                DetermineVideo("8RCM", null, "VF2Y", 14.2f);
                DetermineVideo("WCTF", null, "VF2Y", 14.3f);
                DetermineVideo("N28Q", null, "VF2Y", 14.4f);
                DetermineVideo("HZKZ", null, "VF2Y", 14.5f);
                DetermineVideo("QLQD", null, "VF2Y", 14.6f);

                foreach (Video video in videos) if (video.code == CardCode && currIncident != null)
                    {
                        videoPlayer.VideoPath = video.videoID;
                        videoPlayer.Play();
                    }

                if (currIncident == "RCW5")
                {
                    pimp += 1 * card.bonus;
                    fixer += 0 * card.bonus;
                    sleeper += -1 * card.bonus;
                }
                else if (currIncident == "8RCM")
                {
                    pimp += 1 * card.bonus;
                    fixer += -1 * card.bonus;
                    sleeper += 0 * card.bonus;
                }
                else if (currIncident == "WCTF")
                {
                    pimp += -1 * card.bonus;
                    fixer += 1 * card.bonus;
                    sleeper += 0 * card.bonus;
                }
                else if (currIncident == "N28Q")
                {
                    pimp += 0 * card.bonus;
                    fixer += 1 * card.bonus;
                    sleeper += -1 * card.bonus;
                }
                else if (currIncident == "HZKZ")
                {
                    pimp += 0 * card.bonus;
                    fixer += -1 * card.bonus;
                    sleeper += 1 * card.bonus;
                }
                else if (currIncident == "QLQD")
                {
                    pimp += -1 * card.bonus;
                    fixer += 0 * card.bonus;
                    sleeper += 1 * card.bonus;
                }

                if (incidentIndex == 5)
                {
                    if (pimp >= fixer && pimp >= sleeper)
                    {
                        Debug.Log("Game finished, Pimp wins");
                    } else if (fixer >= sleeper)
                    {
                        Debug.Log("Game finished, Fixer wins");
                    }
                    else
                    {
                        Debug.Log("Game finished, Sleeper wins");
                    }
                }
            }
        }
    }

    void DetermineVideo(string incident1, string incident2, string card, float vID)
    {
        if (currIncident == incident1 && CardCode == card || currIncident == incident2 && CardCode == card)
        {
            PlayVideo(vID);
        }
    }

    void PlayVideo(float vID)
    {
        foreach (Video video in videos)
        {
            if (video.ID == vID)
            {
                videoPlayer.VideoPath = video.videoID;
                videoPlayer.Play();
            }
        }
    }

    public double GetRandomNumber(double minimum, double maximum)
    {
        System.Random random = new System.Random();
        return random.NextDouble() * (maximum - minimum) + minimum;
    }
}


/*
 * Incident 1 = RCW5
 * Incident 2 = 8RCM
 * Incident 3 = WCTF
 * Incident 4 = N28Q
 * Incident 5 = HZKZ
 * Incident 6 = QLQD
*/
