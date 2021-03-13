using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class MouseDrag4 : MonoBehaviour
{
	// Token: 0x06000059 RID: 89 RVA: 0x000B6910 File Offset: 0x000B5910
	public void OnMouseDown()
	{
		if (this.maid != null)
		{
			this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
			this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
			this.off2 = new Vector3(this.obj.transform.position.x - this.HandL.position.x, this.obj.transform.position.y - this.HandL.position.y, this.obj.transform.position.z - this.HandL.position.z);
			this.mouseIti = Input.mousePosition;
			this.isSelect = true;
			this.rotate = this.HandL.localEulerAngles;
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x000B6A38 File Offset: 0x000B5A38
	public void OnMouseDrag()
	{
		if (this.maid != null)
		{
			if (this.reset)
			{
				this.reset = false;
				this.worldPoint = Camera.main.WorldToScreenPoint(base.transform.position);
				this.off = base.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z));
				this.off2 = new Vector3(this.obj.transform.position.x - this.HandL.position.x, this.obj.transform.position.y - this.HandL.position.y, this.obj.transform.position.z - this.HandL.position.z);
				this.rotate = this.HandL.localEulerAngles;
				this.mouseIti = Input.mousePosition;
			}
			if (this.mouseIti != Input.mousePosition)
			{
				Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.worldPoint.z);
				Vector3 vector = Camera.main.ScreenToWorldPoint(position) + this.off - this.off2;
				Vector3 vector2 = Input.mousePosition - this.mouseIti;
				Transform transform = GameMain.Instance.MainCamera.gameObject.transform;
				Vector3 vector3 = transform.TransformDirection(Vector3.right);
				Vector3 vector4 = transform.TransformDirection(Vector3.forward);
				if (this.mouseIti2 != Input.mousePosition)
				{
					this.HandL.localEulerAngles = this.rotate;
					if (this.ido == 1)
					{
						this.HandL.RotateAround(this.HandL.position, new Vector3(vector3.x, 0f, vector3.z), vector2.y / 1f);
						this.HandL.RotateAround(this.HandL.position, new Vector3(vector4.x, 0f, vector4.z), -vector2.x / 1.5f);
					}
					if (this.ido == 2)
					{
						this.HandL.localRotation = Quaternion.Euler(this.HandL.localEulerAngles) * Quaternion.AngleAxis(vector2.x / 1.5f, Vector3.right);
					}
				}
			}
			this.mouseIti2 = Input.mousePosition;
		}
	}

	// Token: 0x0400036A RID: 874
	private Vector3 worldPoint;

	// Token: 0x0400036B RID: 875
	private Vector3 off;

	// Token: 0x0400036C RID: 876
	private Vector3 off2;

	// Token: 0x0400036D RID: 877
	public Maid maid = null;

	// Token: 0x0400036E RID: 878
	public Transform HandL;

	// Token: 0x0400036F RID: 879
	public bool isStop = false;

	// Token: 0x04000370 RID: 880
	public bool isPlay = false;

	// Token: 0x04000371 RID: 881
	public bool isSelect = false;

	// Token: 0x04000372 RID: 882
	private Vector3 mouseIti;

	// Token: 0x04000373 RID: 883
	public GameObject obj;

	// Token: 0x04000374 RID: 884
	public int ido;

	// Token: 0x04000375 RID: 885
	public bool reset;

	// Token: 0x04000376 RID: 886
	private Vector3 rotate;

	// Token: 0x04000377 RID: 887
	private Vector3 mouseIti2;
}
