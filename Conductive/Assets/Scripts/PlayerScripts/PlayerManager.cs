using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Switch character parameters")]
    [SerializeField] List<PlayerMovement> _playersInScene;
    int _currentPlayerID = 0;

    public event System.Action<PlayerMovement> OnTargetPlayerChanged;


    void Update()
    {
        _playersInScene[_currentPlayerID].ManualUpdate();
    }


    public void CyclePlayer(bool inForward)
    {
        if (_playersInScene.Count == 1)
            Debug.Log("There is only on bulb in the scene!");

        else
        {
            _playersInScene[_currentPlayerID].ToggleMovement(false);

            if (inForward)
            {
                if ((_currentPlayerID + 1) < _playersInScene.Count)
                    _currentPlayerID++;

                else if ((_currentPlayerID + 1) == _playersInScene.Count)
                    _currentPlayerID = 0;
            }

            else if (!inForward)
            {
                if ((_currentPlayerID - 1) > 0)
                    _currentPlayerID--;

                else if (_currentPlayerID == 1)
                    _currentPlayerID = 0;

                else if (_currentPlayerID == 0)
                    _currentPlayerID = (_playersInScene.Count - 1);
            }

            _playersInScene[_currentPlayerID].ToggleMovement(true);

            if (OnTargetPlayerChanged != null)
                OnTargetPlayerChanged(_playersInScene[_currentPlayerID]);
        }
    }
    
     
    public void ToggleCurrentPlayerMovement(bool inToggle)
    {
        if (inToggle)
            _playersInScene[_currentPlayerID].ToggleMovement(inToggle); 
        else
            _playersInScene[_currentPlayerID].ToggleMovement(inToggle);
    }


    public PlayerMovement GetCurrentPlayer()
    {
        return _playersInScene[_currentPlayerID];
    }
}