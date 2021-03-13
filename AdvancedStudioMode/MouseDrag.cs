using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class MouseDrag : MonoBehaviour
{
	// Token: 0x0600004B RID: 75 RVA: 0x000B36A4 File Offset: 0x000B26A4
	public void OnMouseDown()
	{
		if (this.maid != null)
		{
			this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
			this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
			this.off2 = new Vector3(this.obj.transform.position.x - this.HandL.position.x, this.obj.transform.position.y - this.HandL.position.y, this.obj.transform.position.z - this.HandL.position.z);
			if (!this.initFlg)
			{
				this.IK.Init(this.UpperArmL, this.ForearmL, this.HandL, this.maid.body0);
				this.initFlg = true;
				if (!this.initFlg2)
				{
					this.initFlg2 = true;
					this.HandL2.transform.position = this.HandL.position;
					this.UpperArmL2.transform.position = this.UpperArmL.position;
					this.ForearmL2.transform.position = this.ForearmL.position;
					this.HandL2.transform.localRotation = this.HandL.localRotation;
					this.UpperArmL2.transform.localRotation = this.UpperArmL.localRotation;
					this.ForearmL2.transform.localRotation = this.ForearmL.localRotation;
				}
			}
			if (this.onFlg)
			{
				this.onFlg2 = true;
				this.IK.Init(this.UpperArmL, this.ForearmL, this.HandL, this.maid.body0);
			}
			this.mouseIti = Input.mousePosition;
			this.isPlay = this.maid.body0.m_Bones.GetComponent<Animation>().isPlaying;
			this.isSelect = true;
			this.rotate = this.HandL.localEulerAngles;
			this.rotate2 = this.UpperArmL.localEulerAngles;
			this.isMouseUp = false;
			this.isMouseDown = true;
			if (this.ido == 10)
			{
				Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z);
				Vector3 vector = Camera.main.ScreenToWorldPoint(position) + this.off - this.off2;
				this.IK.Init(this.UpperArmL2.transform, this.ForearmL2.transform, this.HandL2.transform, this.maid.body0);
				for (int i = 0; i < 10; i++)
				{
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
				}
			}
			if (this.shoki == -1000f)
			{
				this.shoki = this.UpperArmL.localEulerAngles.x;
				if (this.shoki > 300f)
				{
					this.shoki -= 360f;
				}
			}
		}
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000B3AE6 File Offset: 0x000B2AE6
	public void OnMouseUp()
	{
		this.isMouseUp = true;
		this.isMouseDown = false;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x000B3AF8 File Offset: 0x000B2AF8
	public void OnMouseDrag()
	{
		if (this.maid != null)
		{
			if (this.isPlay)
			{
				if (this.mouseIti != Input.mousePosition)
				{
					this.maid.body0.m_Bones.GetComponent<Animation>().Stop();
					this.isStop = true;
					this.isPlay = false;
				}
			}
			if (this.reset)
			{
				this.reset = false;
				this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
				this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
				this.off2 = new Vector3(this.obj.transform.position.x - this.HandL.position.x, this.obj.transform.position.y - this.HandL.position.y, this.obj.transform.position.z - this.HandL.position.z);
				this.rotate = this.HandL.localEulerAngles;
				this.rotate2 = this.UpperArmL.localEulerAngles;
				this.mouseIti = Input.mousePosition;
				if (this.onFlg)
				{
					this.IK.Init(this.UpperArmL, this.ForearmL, this.HandL, this.maid.body0);
				}
			}
			if (this.mouseIti != Input.mousePosition)
			{
				Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z);
				Vector3 vector = Camera.main.ScreenToWorldPoint(position) + this.off - this.off2;
				this.isMouseDrag = true;
				if (!this.isPlay)
				{
					this.isStop = true;
				}
				this.isIdo = false;
				if (this.ido == 0)
				{
					this.isIdo = true;
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					if (!this.onFlg)
					{
						this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					}
					this.HandLangles = new Vector3(this.HandL.localEulerAngles.x, this.HandL.localEulerAngles.y, this.HandL.localEulerAngles.z);
					this.UpperArmLangles = new Vector3(this.UpperArmL.localEulerAngles.x, this.UpperArmL.localEulerAngles.y, this.UpperArmL.localEulerAngles.z);
				}
				else if (this.ido == 11)
				{
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					if (!this.onFlg)
					{
						this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					}
					float num = this.UpperArmL.localEulerAngles.x;
					if (num > 250f + this.shoki && num < 352f + this.shoki)
					{
						num = 352f + this.shoki;
					}
					if (num < 100f + this.shoki && num > 8f + this.shoki)
					{
						num = 8f + this.shoki;
					}
					float num2 = this.UpperArmL.localEulerAngles.z;
					if (num2 > 140f && num2 < 240f)
					{
						num2 = 240f;
					}
					if (num2 <= 140f && num2 > 7f)
					{
						num2 = 7f;
					}
					this.HandL.localEulerAngles = new Vector3(this.rotate.x, this.rotate.y, this.HandL.localEulerAngles.z);
					this.UpperArmL.localEulerAngles = new Vector3(num, this.rotate2.y, num2);
				}
				else if (this.ido == 12)
				{
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					if (!this.onFlg)
					{
						this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					}
					float num = this.UpperArmL.localEulerAngles.x;
					if (num > 250f + this.shoki && num < 357f + this.shoki)
					{
						num = 357f + this.shoki;
					}
					if (num < 100f + this.shoki && num > 3f + this.shoki)
					{
						num = 3f + this.shoki;
					}
					float num2 = this.UpperArmL.localEulerAngles.z;
					if (num2 > 140f && num2 < 270f)
					{
						num2 = 270f;
					}
					if (num2 <= 140f && num2 > 15f)
					{
						num2 = 15f;
					}
					this.HandL.localEulerAngles = new Vector3(this.rotate.x, this.rotate.y, this.HandL.localEulerAngles.z);
					this.UpperArmL.localEulerAngles = new Vector3(num, this.rotate2.y, num2);
				}
				else if (this.ido == 17)
				{
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					if (!this.onFlg)
					{
						this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					}
					float num = this.UpperArmL.localEulerAngles.x;
					if (num > 250f + this.shoki && num < 357f + this.shoki)
					{
						num = 357f + this.shoki;
					}
					if (num < 100f + this.shoki && num > 3f + this.shoki)
					{
						num = 3f + this.shoki;
					}
					float num2 = this.UpperArmL.localEulerAngles.z;
					if (num2 > 220f && num2 < 345f)
					{
						num2 = 345f;
					}
					if (num2 <= 220f && num2 > 90f)
					{
						num2 = 90f;
					}
					this.HandL.localEulerAngles = new Vector3(this.rotate.x, this.rotate.y, this.HandL.localEulerAngles.z);
					this.UpperArmL.localEulerAngles = new Vector3(num, this.rotate2.y, num2);
				}
				else if (this.ido == 13)
				{
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					if (!this.onFlg)
					{
						this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					}
					float num = this.UpperArmL.localEulerAngles.x;
					if (num > 250f && num < 345f)
					{
						num = 345f;
					}
					if (num <= 250f && num > 25f)
					{
					}
					float num2 = this.UpperArmL.localEulerAngles.z;
					if (num2 > 160f && num2 < 275f)
					{
						num2 = 275f;
					}
					if (num2 <= 160f && num2 > 60f)
					{
						num2 = 60f;
					}
					float num3 = this.UpperArmL.localEulerAngles.y;
					if (num3 > 250f && num3 < 345f)
					{
						num3 = 345f;
					}
					if (num3 <= 250f && num3 > 20f)
					{
					}
					this.HandL.localEulerAngles = new Vector3(this.rotate.x, this.HandL.localEulerAngles.y, this.HandL.localEulerAngles.z);
					this.UpperArmL.localEulerAngles = new Vector3(this.UpperArmL.localEulerAngles.x, this.UpperArmL.localEulerAngles.y, num2);
				}
				else if (this.ido == 14)
				{
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					if (!this.onFlg)
					{
						this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					}
					float num = this.UpperArmL.localEulerAngles.x;
					if (num > 250f + this.shoki && num < 345f + this.shoki)
					{
						num = 345f + this.shoki;
					}
					if (num < 100f + this.shoki && num > 15f + this.shoki)
					{
						num = 15f + this.shoki;
					}
					float num2 = this.UpperArmL.localEulerAngles.z;
					if (num2 > 150f && num2 < 240f)
					{
						num2 = 240f;
					}
					if (num2 <= 150f && num2 > 30f)
					{
						num2 = 30f;
					}
					this.HandL.localEulerAngles = new Vector3(this.rotate.x, this.rotate.y, this.HandL.localEulerAngles.z);
					this.UpperArmL.localEulerAngles = new Vector3(num, this.rotate2.y, num2);
				}
				else if (this.ido == 15)
				{
					this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector, default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					if (!this.onFlg)
					{
						this.IK.Porc(this.UpperArmL, this.ForearmL, this.HandL, vector + (vector - this.HandL.position), default(Vector3), this.maid.body0.IKCtrl.GetIKData("左手", false));
					}
					float num = this.UpperArmL.localEulerAngles.x;
					if (num > 250f + this.shoki && num < 357f + this.shoki)
					{
						num = 357f + this.shoki;
					}
					if (num < 100f + this.shoki && num > 3f + this.shoki)
					{
						num = 3f + this.shoki;
					}
					float num2 = this.UpperArmL.localEulerAngles.z;
					if (num2 > 150f && num2 < 270f)
					{
						num2 = 270f;
					}
					if (num2 <= 150f && num2 > 30f)
					{
						num2 = 30f;
					}
					this.HandL.localEulerAngles = new Vector3(this.rotate.x, this.rotate.y, this.HandL.localEulerAngles.z);
					this.UpperArmL.localEulerAngles = new Vector3(num, this.rotate2.y, num2);
				}
				else if (this.ido == 16)
				{
					Vector3 vector2 = Input.mousePosition - this.mouseIti;
					Transform transform = GameMain.Instance.MainCamera.gameObject.transform;
					Vector3 vector3 = transform.TransformDirection(Vector3.right);
					Vector3 vector4 = transform.TransformDirection(Vector3.forward);
					if (this.mouseIti2 != Input.mousePosition)
					{
						this.UpperArmL.localEulerAngles = this.rotate2;
						this.UpperArmL.localRotation = Quaternion.Euler(this.UpperArmL.localEulerAngles) * Quaternion.AngleAxis(vector2.x / 2.2f, Vector3.right);
					}
				}
				else
				{
					Vector3 vector2 = Input.mousePosition - this.mouseIti;
					Transform transform = GameMain.Instance.MainCamera.gameObject.transform;
					Vector3 vector3 = transform.TransformDirection(Vector3.right);
					Vector3 vector4 = transform.TransformDirection(Vector3.forward);
					if (this.mouseIti2 != Input.mousePosition)
					{
						if (this.ido <= 4)
						{
							this.HandL.localEulerAngles = this.rotate;
						}
						else
						{
							this.UpperArmL.localEulerAngles = this.rotate2;
						}
						if (this.ido == 1)
						{
							this.HandL.localRotation = Quaternion.Euler(this.HandL.localEulerAngles) * Quaternion.AngleAxis(vector2.x / 1.5f, Vector3.up);
							this.HandL.localRotation = Quaternion.Euler(this.HandL.localEulerAngles) * Quaternion.AngleAxis(vector2.y / 1.5f, Vector3.forward);
						}
						if (this.ido == 2)
						{
							this.HandL.localRotation = Quaternion.Euler(this.HandL.localEulerAngles) * Quaternion.AngleAxis(vector2.x / 1.5f, Vector3.right);
						}
						if (this.ido == 3)
						{
							this.HandL.RotateAround(this.HandL.position, new Vector3(vector3.x, 0f, vector3.z), vector2.y / 1f);
							this.HandL.RotateAround(this.HandL.position, new Vector3(vector4.x, 0f, vector4.z), vector2.x / 1.5f);
						}
						if (this.ido == 4)
						{
							this.HandL.localRotation = Quaternion.Euler(this.HandL.localEulerAngles) * Quaternion.AngleAxis(-vector2.x / 1.5f, Vector3.right);
						}
						if (this.ido == 5)
						{
							this.UpperArmL.localRotation = Quaternion.Euler(this.UpperArmL.localEulerAngles) * Quaternion.AngleAxis(-vector2.x / 1.5f, Vector3.right);
						}
					}
				}
			}
			this.mouseIti2 = Input.mousePosition;
		}
	}

	// Token: 0x04000314 RID: 788
	private TBody.IKCMO IK = new TBody.IKCMO();

	// Token: 0x04000315 RID: 789
	private Vector3 worldPoint;

	// Token: 0x04000316 RID: 790
	public Vector3 off;

	// Token: 0x04000317 RID: 791
	public Vector3 off2;

	// Token: 0x04000318 RID: 792
	public Maid maid = null;

	// Token: 0x04000319 RID: 793
	public Transform HandL;

	// Token: 0x0400031A RID: 794
	public Transform UpperArmL;

	// Token: 0x0400031B RID: 795
	public Transform ForearmL;

	// Token: 0x0400031C RID: 796
	public bool isStop = false;

	// Token: 0x0400031D RID: 797
	public bool isPlay = false;

	// Token: 0x0400031E RID: 798
	public bool isSelect = false;

	// Token: 0x0400031F RID: 799
	private Vector3 mouseIti;

	// Token: 0x04000320 RID: 800
	public bool isIdo = false;

	// Token: 0x04000321 RID: 801
	public Vector3 HandLangles;

	// Token: 0x04000322 RID: 802
	public Vector3 UpperArmLangles;

	// Token: 0x04000323 RID: 803
	public bool isMouseUp = false;

	// Token: 0x04000324 RID: 804
	public bool isMouseDown = false;

	// Token: 0x04000325 RID: 805
	public bool isMouseDrag = false;

	// Token: 0x04000326 RID: 806
	public bool initFlg = false;

	// Token: 0x04000327 RID: 807
	public bool initFlg2 = false;

	// Token: 0x04000328 RID: 808
	public bool onFlg = false;

	// Token: 0x04000329 RID: 809
	public bool onFlg2 = false;

	// Token: 0x0400032A RID: 810
	public GameObject obj;

	// Token: 0x0400032B RID: 811
	public int ido;

	// Token: 0x0400032C RID: 812
	public bool reset;

	// Token: 0x0400032D RID: 813
	public float shoki = -1000f;

	// Token: 0x0400032E RID: 814
	private Vector3 rotate;

	// Token: 0x0400032F RID: 815
	private Vector3 rotate2;

	// Token: 0x04000330 RID: 816
	private Vector3 mouseIti2;

	// Token: 0x04000331 RID: 817
	private GameObject HandL2 = new GameObject();

	// Token: 0x04000332 RID: 818
	private GameObject UpperArmL2 = new GameObject();

	// Token: 0x04000333 RID: 819
	private GameObject ForearmL2 = new GameObject();
}
