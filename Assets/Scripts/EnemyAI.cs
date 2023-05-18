using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplosionPrefab; 
    
    private UI_Manager _uiManager;

    private float _speed = 5.0f;
     [SerializeField]
      private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -5.6f)
      {
         transform.position = new Vector3(Random.Range(-8.5f, 8.5f) , 5.6f ,0);
      }
    }

    private void OnTriggerEnter2D(Collider2D other) {
      if(other.tag == "Player"){
         Player player = other.GetComponent<Player>();
          if(player != null){
            player.takeDamage();
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();           
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
          }
      }

      if(other.tag == "Laser"){
         Laser laser = other.GetComponent<Laser>();
          if(laser != null){
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            
            if(other.transform.parent != null){ 
              AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
              Destroy(other.transform.parent);  
            }
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
            Destroy(laser.gameObject);
          }
      }
    }

    
}
