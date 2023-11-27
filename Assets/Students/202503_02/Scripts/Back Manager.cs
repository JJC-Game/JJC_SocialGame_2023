using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackManager : MonoBehaviour
{
    public Sprite[] backgrounds; // 複数の背景を保持するための配列

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // シーンが読み込まれた後に実行されるコード
        int backgroundIndex = scene.buildIndex; // シーンのビルドインデックスを取得して背景を選択

        // 背景が設定されている場合、それを表示する
        if (backgroundIndex < backgrounds.Length && backgrounds[backgroundIndex] != null)
        {
            // 例えば、背景を表示するためのコードをここに追加する
            // この例では、SpriteRendererを使って背景を表示すると仮定しています
            GetComponent<SpriteRenderer>().sprite = backgrounds[backgroundIndex];
        }
    }
}
