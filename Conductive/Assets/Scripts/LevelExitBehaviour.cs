using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class LevelExitBehaviour : MonoBehaviour
{
    // Remember to put a PowerModule onto the actual exit; link that module to the receiver.
    // Also, don't forget to input the name of the next level to load ya dingus.
    [SerializeField] string _levelToLoad;
    bool _exitIsEnabled = false;
    MeshRenderer _meshRenderer;


    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        GetComponent<PowerModule>().OnPowerStateChanged += inNewPowerState => _exitIsEnabled = inNewPowerState ? _exitIsEnabled = true : _exitIsEnabled == _exitIsEnabled;
    }


    void Update()
    {
        if (_exitIsEnabled)
            _meshRenderer.material.color = Color.green;
        else
            _meshRenderer.material.color = Color.red;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_exitIsEnabled)
            {
                Debug.Log("Do fancy fucking level sequence shit");
                StartCoroutine(LoadNextLevelSequence());
            }
            else
                Debug.Log("The exit isn't enabled");
        }
    }

    IEnumerator LoadNextLevelSequence()
    {
        SaveProgress();
        Debug.Log("Cool animation shit goes here");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(_levelToLoad, LoadSceneMode.Single);
    }

    void SaveProgress()
    {
        List<string> existingLevelSaves = new List<string>();
        if (File.Exists("SaveData.bob"))
        {
            string line;
            using (StreamReader file = new StreamReader("SaveData.bob", true))
                while ((line = file.ReadLine()) != null)
                    existingLevelSaves.Add(line);
        }

        using (StreamWriter file = new StreamWriter("SaveData.bob", true))
            if (!existingLevelSaves.Contains(SceneManager.GetActiveScene().name))
                file.WriteLine(SceneManager.GetActiveScene().name);

        // Nej
    }
}
