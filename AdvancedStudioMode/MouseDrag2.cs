using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class MouseDrag2 : MonoBehaviour
{
	// Token: 0x0600004F RID: 79 RVA: 0x000B4ED3 File Offset: 0x000B3ED3
	public void Update()
	{
		this.doubleTapTime += Time.deltaTime;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000B4EE8 File Offset: 0x000B3EE8
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
					this.maid.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				if (this.ido == 4 || this.ido == 6)
				{
					this.maid.transform.eulerAngles = new Vector3(0f, this.maid.transform.eulerAngles.y, 0f);
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
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x000B5130 File Offset: 0x000B4130
	public void OnMouseUp()
	{
		if (this.maid != null)
		{
			this.isOn = false;
		}
		if (this.doubleTapTime < 0.3f)
		{
			this.isClick = true;
			this.doubleTapTime = 0f;
			this.idoOld = this.ido;
		}
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000B5190 File Offset: 0x000B4190
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
					this.isIdo = true;
				}
				if (this.ido == 2)
				{
					this.maid.transform.position = new Vector3(this.maid.transform.position.x, vector.y, this.maid.transform.position.z);
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
						this.maid.body0.transform.localRotation = Quaternion.Euler(this.maid.body0.transform.localEulerAngles) * Quaternion.AngleAxis(-vector2.x / 2.2f, Vector3.up);
					}
					this.isIdo = true;
					this.mouseIti2 = Input.mousePosition;
				}
				if (this.ido == 5)
				{
					Vector3 vector2 = Input.mousePosition - this.mouseIti;
					float num = this.size + vector2.y / 200f;
					if (num < 0.1f)
					{
						num = 0.1f;
					}
					this.maid.transform.localScale = new Vector3(num, num, num);
				}
			}
		}
	}

	// Token: 0x04000334 RID: 820
	private Vector3 worldPoint;

	// Token: 0x04000335 RID: 821
	private Vector3 off;

	// Token: 0x04000336 RID: 822
	private Vector3 off2;

	// Token: 0x04000337 RID: 823
	public GameObject obj;

	// Token: 0x04000338 RID: 824
	public int ido;

	// Token: 0x04000339 RID: 825
	public bool reset;

	// Token: 0x0400033A RID: 826
	public bool isSelect = false;

	// Token: 0x0400033B RID: 827
	public Maid maid = null;

	// Token: 0x0400033C RID: 828
	public bool isIdo = false;

	// Token: 0x0400033D RID: 829
	private Vector3 rotate;

	// Token: 0x0400033E RID: 830
	private float size;

	// Token: 0x0400033F RID: 831
	private Vector3 mouseIti;

	// Token: 0x04000340 RID: 832
	private Vector3 mouseIti2;

	// Token: 0x04000341 RID: 833
	public bool isOn;

	// Token: 0x04000342 RID: 834
	public bool isClick = false;

	// Token: 0x04000343 RID: 835
	private float doubleTapTime;

	// Token: 0x04000344 RID: 836
	private int idoOld = 0;
}
