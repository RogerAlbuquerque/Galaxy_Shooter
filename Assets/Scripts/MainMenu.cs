using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPreFab;

    public bool isGameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameStarted){
            this.gameObject.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isGameStarted)
        {
            startGame();
        }
    }

    public void startGame()
    {

            
            Instantiate(_playerPreFab, new Vector3(0,-4,0), Quaternion.identity);  
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
            for(byte i = 0 ; i < enemys.Length; i++){Destroy(enemys[i]);}
            for(byte i = 0 ; i < powerups.Length; i++){Destroy(powerups[i]);}

            
                  
    }

    public void GameOver()
    {          
            this.gameObject.SetActive(true);     
            isGameStarted = false;
    }
}
