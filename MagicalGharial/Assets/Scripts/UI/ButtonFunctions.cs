using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ChangeLayout(GameObject newPanel)
    {
        if(newPanel.activeSelf == true)
        {
            newPanel.SetActive(false);
        }
        else
        {
            newPanel.SetActive(true);
        }
    }
}
