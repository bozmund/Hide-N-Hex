using Unity.VisualScripting;
using UnityEngine;

public class ListOfNPC : MonoBehaviour
{
    public GameObject[] npcList;

    private void Start()
    {
        npcList = new GameObject[6];
        npcList[0] = GameObject.Find("FirstNPC");
        npcList[1] = GameObject.Find("SecondNPC");
        npcList[2] = GameObject.Find("ThirdNPC");
        npcList[3] = GameObject.Find("FourthNPC");
        npcList[4] = GameObject.Find("FifthNPC");
        npcList[5] = GameObject.Find("SixthNPC");
    }

    public void SendThatOne(int chosenOne)
    {
        for (int i=0; i<=npcList.Length;i++) 
        {
            if (!(i == chosenOne))
            {
                npcList[i].GameObject().SetActive(false);
            }           
        }
    }

    public void dayTime()
    {
        for (int i = 0; i <= npcList.Length; i++)
        {
            npcList[i].GameObject().SetActive(true);
        }
    }
}
