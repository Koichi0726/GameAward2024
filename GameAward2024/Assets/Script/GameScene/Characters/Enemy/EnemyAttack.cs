using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] MeshRenderer m_meshRenderer;
    [Header("デバックで中身を確認する用なので空で大丈夫です"),SerializeField] Material[] m_Materials1;   //変更前のマテリアル
    [Space(10),SerializeField] Material[] m_Materials2;   //変更後のマテリアル
    [SerializeField] GameObject m_Object;       //敵の投擲するオブジェクト設定用の変数
    [SerializeField] float m_AttackRate = 5.0f; //敵の攻撃間隔設定用の変数
    private bool m_AttackFlag = false;        //攻撃時のカラー変更用のフラグ

    // Start is called before the first frame update
    void Start()
    {
        //m_meshRenderer = GetComponent<MeshRenderer>();
        //CreateObjを3.5秒後に呼び出し、以降は AttackRate 秒毎に実行
        m_Materials1 = m_meshRenderer.sharedMaterials;
        InvokeRepeating(nameof(CreateObj), 3.5f, m_AttackRate);
    }

    void Update()
    {
        //アタックフラグがtrueになったら敵のマテリアル変更用のコルーチンを呼び出す
        if (m_AttackFlag == true)
        {
            StartCoroutine("ATTACKFLAG");
        }
    }

    void CreateObj()　// 敵の投擲するオブジェクトを生成する
    {
        m_meshRenderer.materials = m_Materials2;

        //Instantiate( 生成するオブジェクト,  場所, 回転 );
        //現在はエネミーの頭上に生成するようにしています
        //Instantiate(Object, new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 2, this.transform.localPosition.z), Quaternion.identity);

        Debug.Log("敵からの攻撃!!");
        m_AttackFlag = true;
    }

    IEnumerator ATTACKFLAG()
    {

        yield return new WaitForSeconds(1.0f);  //処理の遅延
        m_meshRenderer.materials = m_Materials1;
        m_AttackFlag = false;
    }
}
