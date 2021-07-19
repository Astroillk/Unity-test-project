using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Info")]
    [SerializeField] private int currentLevelCount;
    
    [Header("Player Info")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject player;
    
    [Header("Effects")]
    [SerializeField] private GameObject endEffect;
    [SerializeField] private GameObject finishEffect;
    
    [Header("Level Interface")]
    [SerializeField] private Animation levelChangeAnimation;

    private void Start()
    {
        StartCoroutine(StartCurrentLevel());
    }

    public void Spawn()
    {
        Instantiate(player, spawnPoint.position, Quaternion.identity);
    }

    public void Respawn(GameObject playerGameObject)
    {
        Destroy(playerGameObject);
        Spawn();
    }

    public void FinishLevel()
    {
        endEffect.SetActive(true);
        finishEffect.SetActive(false);
        GoToNextLevel();
    }
    
    private void GoToNextLevel()
    {
        StartCoroutine(EndCurrentLevel());
    }

    private IEnumerator StartCurrentLevel()
    {
        levelChangeAnimation.gameObject.SetActive(true);
        levelChangeAnimation.Play("EndLoading");

        yield return new WaitForSeconds(1f);
        
        levelChangeAnimation.gameObject.SetActive(false);
        Spawn();
    }

    private IEnumerator EndCurrentLevel()
    {
        yield return new WaitForSeconds(2f);
        
        levelChangeAnimation.gameObject.SetActive(true);
        levelChangeAnimation.Play("StartLoading");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Level_1_" + (currentLevelCount + 1));
    }
}
