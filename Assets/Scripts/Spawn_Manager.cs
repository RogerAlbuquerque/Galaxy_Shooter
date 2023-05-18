using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawn_Manager : MonoBehaviour
{   
    [SerializeField]
    private GameObject enemyShipPrefab;
    
    [SerializeField]
    private GameObject[] Powerups;

    [SerializeField]
    private GameObject asteroidPreFab;

    private UI_Manager _uiManager;
    // Start is called before the first frame update
    void Start()
    {

        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        StartCoroutine(enemySpawnRoutine());
        StartCoroutine(powerupSpawnRoutine());
        StartCoroutine(asteroidSpawnRoutine());

       // _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
    }

    public IEnumerator enemySpawnRoutine(){
      while(true) {
        int score = _uiManager.score;
        
         GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
         if(enemys.Length < 100)
         {
          Instantiate(enemyShipPrefab, new Vector3(Random.Range(-8.5f, 8.5f), 5.6f ,0), Quaternion.identity);
         }        
        
        if(score < 100)
        {
          yield return new WaitForSeconds(4f);
        }
        else if(score >= 100  && score < 200)
        {
          yield return new WaitForSeconds(3.0f);
        }
         else if(score >= 200 && score < 500)
        {
          yield return new WaitForSeconds(2.0f);
        }
        
        else if(score >= 500 && score < 1000)
        {
          yield return new WaitForSeconds(1.0f);
        }
        else if(score >= 1000)
        {
          yield return new WaitForSeconds(0.6f);
        }        
        
      }
    }

    public IEnumerator powerupSpawnRoutine(){
         while(true) {
            byte randomPower = (byte) Random.Range(0, 3);
            Instantiate(Powerups[randomPower], new Vector3(Random.Range(-8.5f, 8.5f), 5.6f ,0), Quaternion.identity);
            yield return new WaitForSeconds(10.0f);
        }
    }

    public IEnumerator asteroidSpawnRoutine(){
        while(true) 
        {
          if(_uiManager.score >= 500)
          {            
            Instantiate(asteroidPreFab, new Vector3(Random.Range(-8.5f, 8.5f), 5.6f ,0), Quaternion.identity);
            yield return new WaitForSeconds(20.0f);
          }
          else
          {
            yield return new WaitForSeconds(30.0f);
          }
        }
    }
}
