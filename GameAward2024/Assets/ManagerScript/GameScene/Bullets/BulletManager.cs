using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class BulletManager : MonoBehaviour
	{
		/// <summary>
		/// 弾を作成
		/// </summary>
		/// <typeparam name="T">BulletBaseを継承したクラスに限定</typeparam>
		/// <param name="bulletPrefab">弾のプレハブ</param>
		/// <returns>作成した弾への参照</returns>
		public T CreateBullet<T>(BulletBase bulletPrefab , Vector3 vector3) where T : BulletBase
		{

			BulletBase bullet = Instantiate(bulletPrefab,vector3 ,Quaternion.identity);	// 弾を作成
			bullet.transform.SetParent(this.transform);		// 親をBulletManagerに設定
			return bullet as T;	// 目的のクラスへキャスト
		}
	}
}