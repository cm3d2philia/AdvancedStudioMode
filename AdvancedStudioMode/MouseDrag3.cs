using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class MouseDrag3 : MonoBehaviour
{
	// Token: 0x06000054 RID: 84 RVA: 0x000B574D File Offset: 0x000B474D
	public void Update()
	{
		this.doubleTapTime += Time.deltaTime;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000B5764 File Offset: 0x000B4764
	public void OnMouseDown()
	{
		if (this.maid != null)
		{
			this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
			this.mouseIti = Input.mousePosition;
			if (this.ido == 1 || this.ido == 4 || this.ido == 8)
			{
				this.rotate = this.head.localEulerAngles;
			}
			if (this.ido == 2 || this.ido == 5)
			{
				this.rotate1 = this.Spine1a.localEulerAngles;
				this.rotate2 = this.Spine1.localEulerAngles;
				this.rotate3 = this.Spine0a.localEulerAngles;
				this.rotate4 = this.Spine.localEulerAngles;
			}
			if (this.ido == 3 || this.ido == 6)
			{
				this.rotate5 = this.Pelvis.localEulerAngles;
			}
			if (this.ido == 7)
			{
				this.rotateR = this.maid.body0.quaDefEyeR.eulerAngles;
				this.rotateL = this.maid.body0.quaDefEyeL.eulerAngles;
				this.quaL = this.maid.body0.quaDefEyeL;
				this.quaR = this.maid.body0.quaDefEyeR;
			}
			if (this.ido == 9)
			{
				this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
				this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
				this.off2 = new Vector3(this.obj.transform.position.x - this.head.position.x, this.obj.transform.position.y - this.head.position.y, this.obj.transform.position.z - this.head.position.z);
				this.mouseIti = Input.mousePosition;
			}
			this.isSelect = true;
			this.isPlay = this.maid.body0.m_Bones.GetComponent<Animation>().isPlaying;
			if (!this.isPlay)
			{
				this.isStop = true;
			}
			if (this.doubleTapTime < 0.3f && this.isClick2 && this.ido == this.idoOld)
			{
				this.isHead = true;
			}
			if (this.doubleTapTime >= 0.3f && this.isClick)
			{
				this.isClick = false;
			}
			if (this.doubleTapTime >= 0.3f && this.isClick2)
			{
				this.isClick2 = false;
			}
			this.doubleTapTime = 0f;
			this.pos3 = Input.mousePosition - this.mouseIti;
		}
	}

	// Token: 0x06000056 RID: 86 RVA: 0x000B5AC8 File Offset: 0x000B4AC8
	public void OnMouseUp()
	{
		if (this.maid != null)
		{
			if (this.doubleTapTime < 0.3f)
			{
				if (this.ido == 7)
				{
					this.isClick = true;
				}
				this.isClick2 = true;
				this.doubleTapTime = 0f;
				this.idoOld = this.ido;
			}
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x000B5B38 File Offset: 0x000B4B38
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
				if (this.ido == 1 || this.ido == 4 || this.ido == 8)
				{
					this.rotate = this.head.localEulerAngles;
				}
				if (this.ido == 2 || this.ido == 5)
				{
					this.rotate1 = this.Spine1a.localEulerAngles;
					this.rotate2 = this.Spine1.localEulerAngles;
					this.rotate3 = this.Spine0a.localEulerAngles;
					this.rotate4 = this.Spine.localEulerAngles;
				}
				if (this.ido == 3 || this.ido == 6)
				{
					this.rotate5 = this.Pelvis.localEulerAngles;
				}
				if (this.ido == 7)
				{
					this.rotateR = this.maid.body0.quaDefEyeR.eulerAngles;
					this.rotateL = this.maid.body0.quaDefEyeL.eulerAngles;
					this.quaL = this.maid.body0.quaDefEyeL;
					this.quaR = this.maid.body0.quaDefEyeR;
				}
				this.mouseIti = Input.mousePosition;
				if (this.ido == 9)
				{
					this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
					this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
					this.off2 = new Vector3(this.obj.transform.position.x - this.head.position.x, this.obj.transform.position.y - this.head.position.y, this.obj.transform.position.z - this.head.position.z);
					this.mouseIti = Input.mousePosition;
				}
			}
			if (this.mouseIti != Input.mousePosition)
			{
				Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z);
				Vector3 vector = Input.mousePosition - this.mouseIti;
				Transform transform = GameMain.Instance.MainCamera.gameObject.transform;
				Vector3 vector2 = transform.TransformDirection(Vector3.right);
				Vector3 vector3 = transform.TransformDirection(Vector3.forward);
				if (this.mouseIti2 != Input.mousePosition)
				{
					if (this.ido == 1)
					{
						this.isIdo = true;
						this.head.localEulerAngles = this.rotate;
						this.head.RotateAround(this.head.position, new Vector3(vector2.x, 0f, vector2.z), vector.y / 3f);
						this.head.RotateAround(this.head.position, new Vector3(vector3.x, 0f, vector3.z), -vector.x / 4.5f);
					}
					if (this.ido == 4)
					{
						this.isIdo = true;
						this.head.localEulerAngles = this.rotate;
						this.head.localRotation = Quaternion.Euler(this.head.localEulerAngles) * Quaternion.AngleAxis(vector.x / 3f, Vector3.right);
					}
					if (this.ido == 8)
					{
						this.isIdo = true;
						this.head.localEulerAngles = this.rotate;
						this.head.localRotation = Quaternion.Euler(this.head.localEulerAngles) * Quaternion.AngleAxis(-vector.x / 3f, Vector3.forward);
					}
					if (this.ido == 9)
					{
						Vector3 vector4 = Camera.main.ScreenToWorldPoint(position) + this.off - this.off2;
						this.head.transform.position = new Vector3(this.head.transform.position.x, vector4.y, this.head.transform.position.z);
					}
					if (this.ido == 2)
					{
						this.Spine1a.localEulerAngles = this.rotate1;
						this.Spine1.localEulerAngles = this.rotate2;
						this.Spine0a.localEulerAngles = this.rotate3;
						this.Spine.localEulerAngles = this.rotate4;
						float num = 1.5f;
						float num2 = 1f;
						float num3 = 0.03f;
						float num4 = 0.1f;
						float num5 = 0.09f;
						float num6 = 0.07f;
						this.Spine1a.RotateAround(this.Spine1a.position, new Vector3(vector2.x, 0f, vector2.z), vector.y / num2 * num3);
						this.Spine1a.RotateAround(this.Spine1a.position, new Vector3(vector3.x, 0f, vector3.z), -vector.x / num * num3);
						this.Spine1.RotateAround(this.Spine1.position, new Vector3(vector2.x, 0f, vector2.z), vector.y / num2 * num4);
						this.Spine1.RotateAround(this.Spine1.position, new Vector3(vector3.x, 0f, vector3.z), -vector.x / num * num4);
						this.Spine0a.RotateAround(this.Spine0a.position, new Vector3(vector2.x, 0f, vector2.z), vector.y / num2 * num5);
						this.Spine0a.RotateAround(this.Spine0a.position, new Vector3(vector3.x, 0f, vector3.z), -vector.x / num * num5);
						this.Spine.RotateAround(this.Spine.position, new Vector3(vector2.x, 0f, vector2.z), vector.y / num2 * num6);
						this.Spine.RotateAround(this.Spine.position, new Vector3(vector3.x, 0f, vector3.z), -vector.x / num * num6);
					}
					if (this.ido == 5)
					{
						this.Spine1a.localEulerAngles = this.rotate1;
						this.Spine1.localEulerAngles = this.rotate2;
						this.Spine0a.localEulerAngles = this.rotate3;
						this.Spine.localEulerAngles = this.rotate4;
						this.Spine1a.localRotation = Quaternion.Euler(this.Spine1a.localEulerAngles) * Quaternion.AngleAxis(vector.x / 1.5f * 0.084f, Vector3.right);
						this.Spine0a.localRotation = Quaternion.Euler(this.Spine0a.localEulerAngles) * Quaternion.AngleAxis(vector.x / 1.5f * 0.156f, Vector3.right);
						this.Spine.localRotation = Quaternion.Euler(this.Spine.localEulerAngles) * Quaternion.AngleAxis(vector.x / 1.5f * 0.156f, Vector3.right);
					}
					if (this.ido == 3)
					{
						this.Pelvis.localEulerAngles = this.rotate5;
						this.Pelvis.RotateAround(this.Pelvis.position, new Vector3(vector2.x, 0f, vector2.z), vector.y / 4f);
						this.Pelvis.RotateAround(this.Pelvis.position, new Vector3(vector3.x, 0f, vector3.z), vector.x / 6f);
					}
					if (this.ido == 6)
					{
						this.Pelvis.localEulerAngles = this.rotate5;
						this.Pelvis.localRotation = Quaternion.Euler(this.Pelvis.localEulerAngles) * Quaternion.AngleAxis(vector.x / 3f, Vector3.right);
					}
					if (this.ido == 7)
					{
						Vector3 vector5 = new Vector3(this.rotateR.x, this.rotateR.y + vector.x / 10f, this.rotateR.z + vector.y / 10f);
						float x = this.maid.body0.quaDefEyeL.eulerAngles.x;
						float x2 = this.maid.body0.quaDefEyeR.eulerAngles.x;
						bool flag = false;
						if (this.maid.body0.Face.morph.bodyskin.PartsVersion < 120)
						{
							if (x > 4.1f && x < 5.1f)
							{
								flag = false;
							}
						}
						else if (x > 0f && x < 0.6f)
						{
							flag = true;
						}
						if (this.shodaiFlg)
						{
							if (vector5.z < 345.7f && vector5.z > 335.7f)
							{
								this.pos3.y = vector.y;
							}
							if (vector5.y < 347.6f && vector5.y > 335.6f)
							{
								this.pos3.x = vector.x;
							}
						}
						else if (!flag)
						{
							if (vector5.z < 354.8f && vector5.z > 344.8f)
							{
								this.pos3.y = vector.y;
							}
							if (vector5.y < 354f && vector5.y > 342f)
							{
								this.pos3.x = vector.x;
							}
						}
						else
						{
							if (vector5.z < 348.7f && vector5.z > 338.7f)
							{
								this.pos3.y = vector.y;
							}
							if (vector5.y < 348.7f && vector5.y > 338.7f)
							{
								this.pos3.x = vector.x;
							}
						}
						this.maid.body0.quaDefEyeR.eulerAngles = new Vector3(this.rotateR.x, this.rotateR.y + this.pos3.x / 10f, this.rotateR.z + this.pos3.y / 10f);
						this.maid.body0.quaDefEyeL.eulerAngles = new Vector3(this.rotateL.x, this.rotateL.y - this.pos3.x / 10f, this.rotateL.z - this.pos3.y / 10f);
					}
				}
				this.mouseIti2 = Input.mousePosition;
			}
		}
	}

	// Token: 0x04000345 RID: 837
	private Vector3 worldPoint;

	// Token: 0x04000346 RID: 838
	private Vector3 off;

	// Token: 0x04000347 RID: 839
	private Vector3 off2;

	// Token: 0x04000348 RID: 840
	public GameObject obj;

	// Token: 0x04000349 RID: 841
	public int ido;

	// Token: 0x0400034A RID: 842
	public bool reset;

	// Token: 0x0400034B RID: 843
	public bool isSelect = false;

	// Token: 0x0400034C RID: 844
	public Transform head;

	// Token: 0x0400034D RID: 845
	public Transform Spine1a;

	// Token: 0x0400034E RID: 846
	public Transform Spine1;

	// Token: 0x0400034F RID: 847
	public Transform Spine0a;

	// Token: 0x04000350 RID: 848
	public Transform Spine;

	// Token: 0x04000351 RID: 849
	public Transform Pelvis;

	// Token: 0x04000352 RID: 850
	public bool isIdo = false;

	// Token: 0x04000353 RID: 851
	public Maid maid;

	// Token: 0x04000354 RID: 852
	public int no;

	// Token: 0x04000355 RID: 853
	public bool shodaiFlg = false;

	// Token: 0x04000356 RID: 854
	private Vector3 rotateL;

	// Token: 0x04000357 RID: 855
	private Vector3 rotateR;

	// Token: 0x04000358 RID: 856
	private Quaternion quaL;

	// Token: 0x04000359 RID: 857
	private Quaternion quaR;

	// Token: 0x0400035A RID: 858
	private Vector3 pos3;

	// Token: 0x0400035B RID: 859
	private Vector3 rotate;

	// Token: 0x0400035C RID: 860
	private Vector3 rotate1;

	// Token: 0x0400035D RID: 861
	private Vector3 rotate2;

	// Token: 0x0400035E RID: 862
	private Vector3 rotate3;

	// Token: 0x0400035F RID: 863
	private Vector3 rotate4;

	// Token: 0x04000360 RID: 864
	private Vector3 rotate5;

	// Token: 0x04000361 RID: 865
	private Vector3 mouseIti;

	// Token: 0x04000362 RID: 866
	private Vector3 mouseIti2;

	// Token: 0x04000363 RID: 867
	public bool isHead = false;

	// Token: 0x04000364 RID: 868
	private float doubleTapTime;

	// Token: 0x04000365 RID: 869
	public bool isStop = false;

	// Token: 0x04000366 RID: 870
	public bool isPlay = false;

	// Token: 0x04000367 RID: 871
	public bool isClick = false;

	// Token: 0x04000368 RID: 872
	public bool isClick2 = false;

	// Token: 0x04000369 RID: 873
	private int idoOld = 0;
}
