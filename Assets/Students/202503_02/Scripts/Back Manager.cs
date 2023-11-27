using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackManager : MonoBehaviour
{
    public int selectedBackgroundIndex = 0; // 選択された背景のインデックス

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
        SetBackground(selectedBackgroundIndex);
    }

    // 外部から背景を変更するためのメソッド
    public void ChangeBackgroundByIndex(int index)
    {
        selectedBackgroundIndex = index;
        SetBackground(selectedBackgroundIndex);
    }

    // 選択された背景を設定するメソッド
    void SetBackground(int index)
    {
        GameObject backgroundObject = GameObject.Find("GlobalInstance/GlobalSpriteAnchor/BackGround"); // バックグラウンドを含むオブジェクトの名前を指定

        if (backgroundObject != null)
        {
            SpriteRenderer spriteRenderer = backgroundObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // backgrounds配列から背景を取得する代わりに、インデックスに基づいて設定
                // 例えば、backgrounds配列が使えない場合は、直接スプライトを割り当てることもできます
                // spriteRenderer.sprite = 背景のスプライト;

                // 以下は仮の例です。スプライトを直接割り当てる場合は、適切なスプライトを設定してください。
                spriteRenderer.color = Color.white; // 仮の背景の色を赤に設定
            }
            else
            {
                Debug.LogWarning("SpriteRendererがアタッチされていません。");
            }
        }
        else
        {
            Debug.LogWarning("指定された背景オブジェクトが見つかりません。");
        }
    }
}
