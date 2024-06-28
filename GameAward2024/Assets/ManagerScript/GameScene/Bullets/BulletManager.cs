using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
	public class BulletManager : ManagerBase
	{
		[SerializeField]
		BulletDataList m_bulletDataList;
		/// <summary>
		/// “G‚Ìƒf[ƒ^ƒŠƒXƒg‚ğæ“¾
		/// </summary>
		public BulletDataList bulletDataList => m_bulletDataList;

		/// <summary>
		/// ’e‚ğì¬
		/// </summary>
		/// <param name="bulletPrefab">’e‚Ìí—Ş‚ğ¦‚·—ñ‹“’è”</param>
		/// <param name="pos">’e‚Ì‰ŠúÀ•W</param>
		/// <returns>ì¬‚µ‚½’e‚Ö‚ÌQÆ</returns>
		public BulletBase CreateBullet(BulletDataList.E_BULLET_KIND bulletKind , Vector3 pos)
		{
			BulletBase prefab = m_bulletDataList.GetBulletPrefab(bulletKind);
			BulletBase bullet = Instantiate(prefab, pos ,Quaternion.identity);	// ’e‚ğì¬
			bullet.transform.SetParent(this.transform);     // e‚ğBulletManager‚Éİ’è
			return bullet;
		}

		/// <summary>
		/// ’e‚ğì¬
		/// </summary>
		/// <param name="bulletPrefab">’e‚Ìí—Ş‚ğ¦‚·—ñ‹“’è”</param>
		/// /// <param name="pos">’e‚Ì‰ŠúÀ•W</param>
		/// /// <param name="rot">’e‚Ì‰ñ“]</param>
		/// <returns>ì¬‚µ‚½’e‚Ö‚ÌQÆ</returns>
		public BulletBase CreateBullet(BulletDataList.E_BULLET_KIND bulletKind, Vector3 pos, Quaternion rot)
		{
			BulletBase prefab = m_bulletDataList.GetBulletPrefab(bulletKind);
			BulletBase bullet = Instantiate(prefab, pos, rot);  // ’e‚ğì¬
			bullet.transform.SetParent(this.transform);			// e‚ğBulletManager‚Éİ’è
			return bullet;
		}
	}
}