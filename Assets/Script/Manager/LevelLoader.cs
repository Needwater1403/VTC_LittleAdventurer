using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;
    private Animator transition;
    public float transitionDuration = 1;
    private static readonly int start = Animator.StringToHash("Start");

    private void Start()
    {
        Time.timeScale = 1;
        transition = GetComponentInChildren<Animator>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(int index)
    {
        StartCoroutine(Load(index));
    }

    private IEnumerator Load(int _index)
    {
        Time.timeScale = 1;
        transition.SetTrigger(start);
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(sceneBuildIndex: _index);
        yield return null;
    }
}
