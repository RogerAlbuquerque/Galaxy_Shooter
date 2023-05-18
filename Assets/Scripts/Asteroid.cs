using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private byte asteroidLife;
    
    [SerializeField]
    private GameObject _ExplosionPrefab;

    [SerializeField]
    private AudioClip _clip;

    
    private UI_Manager _uiManager;

    private float _speed = 2.0f;
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
            if(asteroidLife > 0)
            {
                asteroidLife--;
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
                
            }
            else if(asteroidLife == 0)
            {
                Instantiate(_ExplosionPrefab, transform.position, Quaternion.identity);               
                
                _uiManager.UpdateScore(50);
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
                Destroy(this.gameObject);
                

            }
          }
      }

      if(other.tag == "Laser"){
         Laser laser = other.GetComponent<Laser>();
          if(laser != null){

            if(asteroidLife > 0)
            {
                asteroidLife--;
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
                Destroy(laser.gameObject);
            }
            else if(asteroidLife == 0)
            {
                Instantiate(_ExplosionPrefab, transform.position, Quaternion.identity);
               
                if(other.transform.parent != null){ 
                    AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
                    Destroy(other.transform.parent);  
                }
                _uiManager.UpdateScore(50);
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
                Destroy(this.gameObject);
                Destroy(laser.gameObject);

            }
           
            
            
          }
      }
    }
}
