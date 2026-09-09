using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitWin : MonoBehaviour
{
    public void ExitGame()
    {
        StartCoroutine(WaitForAudioAndQuit());
    }

    private IEnumerator WaitForAudioAndQuit()
    {
        yield return new WaitForSeconds(3);
        Application.Quit();
    }
}
