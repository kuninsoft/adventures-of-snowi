using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadLevels : MonoBehaviour
{
    public GameObject blockGroup;

    public GameObject carrot;
    public GameObject speedyCarrot;
    public GameObject slowyCarrot;
    public GameObject trapCarrot;

    int level;

    private void Start()
    {
        BetterStreamingAssets.Initialize();

        if(!PlayerPrefs.HasKey("PlayerProgress"))
        {
            PlayerPrefs.SetInt("PlayerProgress", 101);
        }

        level = PlayerPrefs.GetInt("PlayerProgress");

        ReadLevel();
    }

    public void ReadLevel()
    {
        level = PlayerPrefs.GetInt("PlayerProgress");

        var rows = new List<string>(BetterStreamingAssets.ReadAllLines($"Levels/{level}.chio"));
        var yOffsets = new float[43];

        var yOffsetStrings = rows[3].Split(',');
        
        for(byte i = 0; i < yOffsetStrings.Length; i++)
        {
            yOffsets[i] = float.Parse(yOffsetStrings[i]);
        }

        for(byte i = 0; i < blockGroup.transform.childCount; i++)
        {
            blockGroup.transform.GetChild(i).transform.position = new Vector3(blockGroup.transform.GetChild(i).transform.position.x, yOffsets[i]);
        }

        SpawnObjects();
    }

    public void SpawnObjects()
    {
        byte randomNum;
        for (byte i = 0; i < 100; i++)
        {
            randomNum = (byte)Random.Range(0, 100);

            if (randomNum >= 50)
            {
                Instantiate(carrot, new Vector3(1 + i * 1.8f, 9, 0), Quaternion.Euler(0, 0, 0));
            }
            else if (randomNum < 50 && randomNum > 47)
            {
                Instantiate(speedyCarrot, new Vector3(1 + i * 1.8f, 9, 0), Quaternion.Euler(0, 0, 0));
            }
            else if (randomNum <= 47 && randomNum > 45)
            {
                Instantiate(slowyCarrot, new Vector3(1 + i * 1.8f, 9, 0), Quaternion.Euler(0, 0, 0));
            }
            else if (randomNum == 45)
            {
                Instantiate(trapCarrot, new Vector3(1 + i * 1.8f, 9, 0), Quaternion.Euler(0, 0, 0));
            }
        }
    }
}
