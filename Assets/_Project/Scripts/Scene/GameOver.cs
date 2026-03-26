using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    private void Update()
    {
        if( _playerController.IsDead)
        {
            SceneManager.LoadScene(0);
        }
    }
}
