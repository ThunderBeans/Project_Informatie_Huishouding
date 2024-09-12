using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabControle : MonoBehaviour
{
    public List<GameObject> stuff;
    public int currentorder = 0;
    public TTS tts;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !Input.GetKey(KeyCode.LeftShift))
        {
            GoToNextStuff(1);
        }

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Tab))
        {
            GoToNextStuff(-1);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (stuff[currentorder].GetComponent<Button>() != null)
            {
                stuff[currentorder].GetComponent<Button>().OnSubmit(null);
            }
            if (stuff[currentorder].GetComponent<BetterButton>() != null)
            {
                stuff[currentorder].GetComponent<BetterButton>().onLeftClick.Invoke();
                stuff[currentorder].GetComponent<BetterButton>().onRightClick.Invoke();
            }
        }
    }

    public void GoToNextStuff(int offset)
    {
        GameObject lastBtn = stuff[currentorder];
        currentorder += offset;
        for (int i = 0; i < 100; i++)
        {
            if (currentorder > stuff.Count - 1)
            {
                currentorder = 0;
            }else if(currentorder < 0)
            {
                currentorder = stuff.Count - 1;
            }

            if (stuff[currentorder].activeSelf)
            {
                bool isGood = false;
                Transform ob = stuff[currentorder].transform;
                for (int j = 0; j < 100; j++)
                {
                    if (ob.parent != null)
                    {
                        if (ob.parent.gameObject.activeSelf)
                        {
                            ob = ob.parent;
                        }
                        else
                        {
                            currentorder += offset;
                            j = 100;
                        }
                    }
                    else
                    {
                        j = 100;
                        isGood = true;
                    }
                }
                if (isGood)
                {
                    i = 100;
                }
            }
            else
            {
                currentorder += offset;

            }
        }
        string read = "";
        if (stuff[currentorder].GetComponent<Button>() != null)
        {
            stuff[currentorder].GetComponent<Image>().color = new Color(118f / 255f, 210f / 255f, 182f / 255f, 100);
            read = stuff[currentorder].transform.GetChild(0).GetComponent<TMP_Text>().text;
        }
        if (stuff[currentorder].GetComponent<TMP_Text>() != null)
        {
            read = stuff[currentorder].GetComponent<TMP_Text>().text;
        }
        if (stuff[currentorder].GetComponent<TMP_InputField>() != null)
        {
            read = "Input Field";
        }

        if (lastBtn.GetComponent<Button>() != null)
        {
            lastBtn.GetComponent<Image>().color = new Color(255, 255, 255, 100);
        }

        TTS.instance.Talk(read);
        print(stuff[currentorder].name);
        
    }
}
