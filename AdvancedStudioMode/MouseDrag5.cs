using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class MouseDrag5 : MonoBehaviour
{
	// Token: 0x0600005C RID: 92 RVA: 0x000B6D54 File Offset: 0x000B5D54
	public void OnMouseDown()
	{
		if (this.maid != null)
		{
			if (this.ido == 1)
			{
				this.isClick = true;
			}
			if (this.ido == 2)
			{
				this.isClick = true;
			}
		}
	}

	// Token: 0x04000378 RID: 888
	public Maid maid = null;

	// Token: 0x04000379 RID: 889
	public int no;

	// Token: 0x0400037A RID: 890
	public GameObject obj;

	// Token: 0x0400037B RID: 891
	public int ido;

	// Token: 0x0400037C RID: 892
	public bool isClick = false;
}
