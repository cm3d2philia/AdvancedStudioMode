using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class IK
{
	// Token: 0x06000063 RID: 99 RVA: 0x000B7A60 File Offset: 0x000B6A60
	public void Init(Transform hip, Transform knee, Transform ankle, TBody b)
	{
		this.body = b;
		this.defLEN1 = (hip.position - knee.position).magnitude;
		this.defLEN2 = (ankle.position - knee.position).magnitude;
		this.knee_old = knee.position;
		this.defHipQ = hip.localRotation;
		this.defKneeQ = knee.localRotation;
		this.vechand = Vector3.zero;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000B7AE4 File Offset: 0x000B6AE4
	public void Porc(Transform hip, Transform knee, Transform ankle, Vector3 tgt, Vector3 vechand_offset)
	{
		this.knee_old = this.knee_old * 0.5f + knee.position * 0.5f;
		Vector3 normalized = (this.knee_old - tgt).normalized;
		this.knee_old = tgt + normalized * this.defLEN2;
		Vector3 normalized2 = (this.knee_old - hip.position).normalized;
		this.knee_old = hip.position + normalized2 * this.defLEN1;
		default(Quaternion).SetLookRotation(normalized2);
		hip.transform.rotation = Quaternion.FromToRotation(knee.transform.position - hip.transform.position, this.knee_old - hip.transform.position) * hip.transform.rotation;
		knee.transform.rotation = Quaternion.FromToRotation(ankle.transform.position - knee.transform.position, tgt - knee.transform.position) * knee.transform.rotation;
	}

	// Token: 0x0400039C RID: 924
	private TBody body;

	// Token: 0x0400039D RID: 925
	private float defLEN1;

	// Token: 0x0400039E RID: 926
	private float defLEN2;

	// Token: 0x0400039F RID: 927
	private Vector3 knee_old;

	// Token: 0x040003A0 RID: 928
	private Quaternion defHipQ;

	// Token: 0x040003A1 RID: 929
	private Quaternion defKneeQ;

	// Token: 0x040003A2 RID: 930
	private Vector3 vechand;
}
