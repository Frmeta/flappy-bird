using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject pipe;
    public GameObject gameover;

    public float pipeSpawnX = 10;
    public float minimumY;
    public float maximumY;
    public float pipeSpeed = 3;
    public float delayPipe = 2;
    public int score = 0;
    public float distancePerDigit = 1;
    public float scoreY = 4;
    public Sprite[] numbers;

    public GameObject skor1;

    Bird bird;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;

        bird = FindObjectOfType<Bird>();
        
        StartCoroutine(Tutorial());

        GameObject a = Instantiate(skor1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RefreshScore(){
        string skor_str = score.ToString();
        int skor = score;

        GameObject[] u = GameObject.FindGameObjectsWithTag("Skor");
        for (int j = 0; j < u.Length; j++){
            Destroy(u[j]);
        }


        for(int i=skor_str.Length; i>0 ;i--){

            float xpos = (i-(skor_str.Length+1)/2) * distancePerDigit;
            Vector3 pos = new Vector3(xpos, scoreY, 0);
            GameObject instan = Instantiate(skor1, pos, Quaternion.identity);

            int satuan = skor % 10;
            skor = Mathf.FloorToInt(skor / 10);
            instan.GetComponent<SpriteRenderer>().sprite = numbers[satuan];
        }
    }
    public void GameOver(){
        StartCoroutine(GameOver2());
    }
    IEnumerator GameOver2(){
        gameover.SetActive(true);
        bird.SetEnable(false);
        pipeSpeed = 0;

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Main Menu");
    }
    IEnumerator Tutorial(){
        tutorial.SetActive(false);
        bird.SetEnable(false);

        yield return new WaitForSeconds(2.5f);

        tutorial.SetActive(true);
        bird.SetEnable(true);
        bird.Jump();

        InvokeRepeating("SpawnPipe", 1, delayPipe);

        yield return new WaitForSeconds(2);
        tutorial.SetActive(false);
    }

    private void SpawnPipe(){
        Vector3 pos = new Vector3(pipeSpawnX, Random.Range(minimumY, maximumY), 0);
        Instantiate(pipe, pos, Quaternion.identity);
    }
    public void AddScore(){
        score += 1;
        RefreshScore();
    }
}
