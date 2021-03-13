using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class MouseDrag6 : MonoBehaviour
{
	// Token: 0x0600005E RID: 94 RVA: 0x000B6DC0 File Offset: 0x000B5DC0
	public void Update()
	{
		this.doubleTapTime += Time.deltaTime;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x000B6DD8 File Offset: 0x000B5DD8
	public void OnMouseDown()
	{
		if (this.maid != null)
		{
			this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
			this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
			this.off2 = new Vector3(this.obj.transform.position.x - this.maid.transform.position.x, this.obj.transform.position.y - this.maid.transform.position.y, this.obj.transform.position.z - this.maid.transform.position.z);
			this.mouseIti = Input.mousePosition;
			if (this.doubleTapTime < 0.3f && this.isClick && this.ido == this.idoOld)
			{
				if (this.ido == 5)
				{
					if (this.isScale2)
					{
						this.maid.transform.localScale = this.scale2;
					}
					else
					{
						this.maid.transform.localScale = new Vector3(1f, 1f, 1f);
					}
				}
				if (this.ido == 4 || this.ido == 6)
				{
					this.maid.transform.eulerAngles = new Vector3(this.angles.x, this.maid.transform.eulerAngles.y, this.angles.z);
				}
			}
			if (this.doubleTapTime >= 0.3f && this.isClick)
			{
				this.isClick = false;
			}
			this.doubleTapTime = 0f;
			this.rotate = this.maid.transform.localEulerAngles;
			this.size = this.maid.transform.localScale.x;
			this.isSelect = true;
			this.isOn = true;
			if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
			{
				this.isAlt = !this.isAlt;
			}
			this.xList = new List<float>();
			this.yList = new List<float>();
			this.zList = new List<float>();
			if (this.maidArray != null)
			{
				for (int i = 0; i < this.maidArray.Length; i++)
				{
					this.xList.Add(this.maid.transform.position.x - this.maidArray[i].transform.position.x);
					this.yList.Add(this.maid.transform.position.y - this.maidArray[i].transform.position.y);
					this.zList.Add(this.maid.transform.position.z - this.maidArray[i].transform.position.z);
				}
			}
		}
	}

	// Token: 0x06000060 RID: 96 RVA: 0x000B7184 File Offset: 0x000B6184
	public void OnMouseUp()
	{
		if (this.maid != null)
		{
			this.isOn = false;
			if (this.doubleTapTime < 0.3f)
			{
				this.isClick = true;
				this.doubleTapTime = 0f;
				this.idoOld = this.ido;
			}
			if (this.ido == 7)
			{
				this.del = true;
			}
			if (this.ido == 8)
			{
				this.copy = true;
			}
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x000B7214 File Offset: 0x000B6214
	public void OnMouseDrag()
	{
		if (this.maid != null)
		{
			if (this.reset)
			{
				this.reset = false;
				this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
				this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
				this.off2 = new Vector3(this.obj.transform.position.x - this.maid.transform.position.x, this.obj.transform.position.y - this.maid.transform.position.y, this.obj.transform.position.z - this.maid.transform.position.z);
				this.rotate = this.maid.transform.localEulerAngles;
				this.size = this.maid.transform.localScale.x;
				this.mouseIti = Input.mousePosition;
			}
			if (this.mouseIti != Input.mousePosition)
			{
				Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z);
				Vector3 vector = Camera.main.ScreenToWorldPoint(position) + this.off - this.off2;
				this.isIdo = false;
				if (this.ido == 1)
				{
					this.maid.transform.position = new Vector3(vector.x, this.maid.transform.position.y, vector.z);
					if (this.maidArray != null)
					{
						for (int i = 0; i < this.maidArray.Length; i++)
						{
							if (this.mArray[i].isAlt)
							{
								this.maidArray[i].transform.position = new Vector3(vector.x - this.xList[i], this.maidArray[i].transform.position.y, vector.z - this.zList[i]);
							}
						}
					}
					this.isIdo = true;
				}
				if (this.ido == 2)
				{
					this.maid.transform.position = new Vector3(this.maid.transform.position.x, vector.y, this.maid.transform.position.z);
					if (this.maidArray != null)
					{
						for (int i = 0; i < this.maidArray.Length; i++)
						{
							if (this.mArray[i].isAlt)
							{
								this.maidArray[i].transform.position = new Vector3(this.maidArray[i].transform.position.x, vector.y - this.yList[i], this.maidArray[i].transform.position.z);
							}
						}
					}
					this.isIdo = true;
				}
				if (this.ido == 3)
				{
					Vector3 vector2 = Input.mousePosition - this.mouseIti;
					this.maid.transform.eulerAngles = new Vector3(this.maid.transform.eulerAngles.x, this.rotate.y - vector2.x / 2.2f, this.maid.transform.eulerAngles.z);
				}
				if (this.ido == 4)
				{
					Vector3 vector2 = Input.mousePosition - this.mouseIti;
					Transform transform = GameMain.Instance.MainCamera.gameObject.transform;
					Vector3 vector3 = transform.TransformDirection(Vector3.right);
					Vector3 vector4 = transform.TransformDirection(Vector3.forward);
					if (this.mouseIti2 != Input.mousePosition)
					{
						this.maid.transform.localEulerAngles = this.rotate;
						this.maid.transform.RotateAround(this.maid.transform.position, new Vector3(vector3.x, 0f, vector3.z), vector2.y / 4f);
						this.maid.transform.RotateAround(this.maid.transform.position, new Vector3(vector4.x, 0f, vector4.z), -vector2.x / 6f);
					}
					this.isIdo = true;
					this.mouseIti2 = Input.mousePosition;
				}
				if (this.ido == 6)
				{
					Vector3 vector2 = Input.mousePosition - this.mouseIti;
					Transform transform = GameMain.Instance.MainCamera.gameObject.transform;
					Vector3 vector3 = transform.TransformDirection(Vector3.right);
					Vector3 vector4 = transform.TransformDirection(Vector3.forward);
					if (this.mouseIti2 != Input.mousePosition)
					{
						this.maid.transform.localEulerAngles = this.rotate;
						this.maid.transform.localRotation = Quaternion.Euler(this.maid.transform.localEulerAngles) * Quaternion.AngleAxis(-vector2.x / 2.2f, Vector3.up);
					}
					this.isIdo = true;
					this.mouseIti2 = Input.mousePosition;
				}
				if (this.ido == 5)
				{
					Vector3 vector2 = Input.mousePosition - this.mouseIti;
					float num = this.size + vector2.y / 200f;
					if (num < 0.01f)
					{
						num = 0.01f;
					}
					if (this.isScale)
					{
						this.maid.transform.localScale = new Vector3(this.scale.x * num, this.scale.y * num, this.scale.z * num);
					}
					else
					{
						this.maid.transform.localScale = new Vector3(num, num, num);
					}
				}
				if (this.ido == 15)
				{
					Vector3 vector2 = Input.mousePosition - this.mouseIti;
					float num = this.size + vector2.y / 2f;
					if (num < 0.01f)
					{
						num = 0.01f;
					}
					if (num > 150f)
					{
						num = 150f;
					}
					this.maid.transform.localScale = new Vector3(num, num, num);
				}
			}
		}
	}

	// Token: 0x0400037D RID: 893
	private Vector3 worldPoint;

	// Token: 0x0400037E RID: 894
	private Vector3 off;

	// Token: 0x0400037F RID: 895
	private Vector3 off2;

	// Token: 0x04000380 RID: 896
	public GameObject obj;

	// Token: 0x04000381 RID: 897
	public int ido;

	// Token: 0x04000382 RID: 898
	public bool reset;

	// Token: 0x04000383 RID: 899
	public bool isSelect = false;

	// Token: 0x04000384 RID: 900
	public bool isScale = false;

	// Token: 0x04000385 RID: 901
	public bool isScale2 = false;

	// Token: 0x04000386 RID: 902
	public bool isAlt = false;

	// Token: 0x04000387 RID: 903
	public Vector3 angles;

	// Token: 0x04000388 RID: 904
	public Vector3 scale;

	// Token: 0x04000389 RID: 905
	public Vector3 scale2;

	// Token: 0x0400038A RID: 906
	private List<float> xList = new List<float>();

	// Token: 0x0400038B RID: 907
	private List<float> yList = new List<float>();

	// Token: 0x0400038C RID: 908
	private List<float> zList = new List<float>();

	// Token: 0x0400038D RID: 909
	public bool del = false;

	// Token: 0x0400038E RID: 910
	public bool copy = false;

	// Token: 0x0400038F RID: 911
	public GameObject maid = null;

	// Token: 0x04000390 RID: 912
	public GameObject[] maidArray = null;

	// Token: 0x04000391 RID: 913
	public MouseDrag6[] mArray = null;

	// Token: 0x04000392 RID: 914
	public bool isIdo = false;

	// Token: 0x04000393 RID: 915
	private Vector3 rotate;

	// Token: 0x04000394 RID: 916
	private float size;

	// Token: 0x04000395 RID: 917
	private Vector3 mouseIti;

	// Token: 0x04000396 RID: 918
	private Vector3 mouseIti2;

	// Token: 0x04000397 RID: 919
	public bool isClick = false;

	// Token: 0x04000398 RID: 920
	private float doubleTapTime;

	// Token: 0x04000399 RID: 921
	public int count;

	// Token: 0x0400039A RID: 922
	public bool isOn;

	// Token: 0x0400039B RID: 923
	private int idoOld = 0;
}
