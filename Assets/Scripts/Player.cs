using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
  public bool canTripleShot = false;
  public bool isSpeedBoostActive = false;
  public bool isShieldActive = false;
  public sbyte lifeCount;
  
  

  [SerializeField]
  private GameObject _playerExplosionPrefab;
  
  [SerializeField]
  private GameObject _laserPrefab;

  [SerializeField]
  private GameObject _tripleShotPreFab;

  [SerializeField]
  private GameObject _shieldGameObject;

  [SerializeField]
  private GameObject _playerLeftDamage;
  
  [SerializeField]
  private GameObject _playerRightDamaga;

  [SerializeField]
  private GameObject _playerCenterDamage;

  [SerializeField]
  private float _speed = 5.0f;

  [SerializeField]
  private AudioSource _audioSource;
  
  
 

  [SerializeField]
  private float _fireRate = 0.25f;
  private float _nextFire = 0.0f;

  private UI_Manager _uiManager;

  private MainMenu _mainMenu;
    

    // Start is called before the first frame update
    void Start()
    {
      transform.position = new Vector3(0,-4,0);

     
      
       _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        if(_uiManager != null)
       {
        _uiManager.UpdateLives(lifeCount);
        _uiManager.resetScore();
       } 

       _mainMenu = GameObject.Find("MainMenu").GetComponent<MainMenu>();
       _mainMenu.isGameStarted = true;

       _audioSource = GetComponent<AudioSource>();    // Isso aqui funciona porque o componente de audio ja está no mesmo componente que esse 
                                                      // script está, então não precisa indicar uma tag para ele procurar é só pegar direito com o GetComponent
      
    }

    // Update is called once per frame
    void Update()
    {
      Movement();
      Shoot();

      

      
    }

    private void Movement(){
      float horizontalInput = Input.GetAxis("Horizontal");
      float verticalInput = Input.GetAxis("Vertical");

      if(isSpeedBoostActive){
        transform.Translate(Vector3.right * Time.deltaTime * _speed * 2f * horizontalInput);  // horizontalInput Isso aqui é um valor padrão da unity tem ans configurações
        transform.Translate(Vector3.up * Time.deltaTime * _speed * 2f * verticalInput);
      }
      else{
      transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);  // horizontalInput Isso aqui é um valor padrão da unity tem ans configurações
      transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);
      }

      //MOVIMENT ENDS---------------------------------------
      
      if(transform.position.x >= 9.3f)
      {
        transform.position = new Vector3(-9.3f,transform.position.y,0);
      }
      if(transform.position.x < -9.3f)
      {
        transform.position = new Vector3(9.3f,transform.position.y,0);
      }

      if(transform.position.y >= 0.5f )
      {
         transform.position = new Vector3(transform.position.x,0.5f,0);
      }

      if(transform.position.y < -4.3f )
      {
         transform.position = new Vector3(transform.position.x,-4.3f,0);
      }
    }

    private void Shoot(){

      if(canTripleShot){

         if(Input.GetMouseButtonDown(0)  && _nextFire < Time.time) {
            _nextFire = Time.time + _fireRate;
            Instantiate(_tripleShotPreFab, transform.position, Quaternion.identity);
            _audioSource.Play();
          }
      }
      else if(Input.GetMouseButtonDown(0)  && _nextFire < Time.time) {
        _nextFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0,0.33f,0), Quaternion.identity);
        _audioSource.Play();
      }
    }

    

    public void takeDamage(){
      if(isShieldActive){
        isShieldActive = false;
        _shieldGameObject.SetActive(false);
      }
      else
      {
        if(lifeCount > 0)
        {
          --lifeCount;
          _uiManager.UpdateLives(lifeCount);

          if(lifeCount == 2){
            _playerLeftDamage.SetActive(true);
            
          }else if( lifeCount == 1){
            _playerRightDamaga.SetActive(true);
          }else if( lifeCount == 0){
          _playerCenterDamage.SetActive(true);
          }
        }
        else if(lifeCount == 0)
        {
          Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
          _mainMenu.GameOver();
          
          Destroy(this.gameObject);
        }
      }
    }



    public void TripleShotPowerupOn(){
      canTripleShot = true;
       StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostPowerupOn(){
       isSpeedBoostActive = true;
       StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void ShieldPowerupOn(){
       isShieldActive = true;
       _shieldGameObject.SetActive(true);

    }

    public IEnumerator SpeedBoostPowerDownRoutine(){
      yield return new WaitForSeconds(5.0f);
      isSpeedBoostActive = false;
    }

    public IEnumerator TripleShotPowerDownRoutine(){
      yield return new WaitForSeconds(5.0f);
      canTripleShot = false;
    }
}
