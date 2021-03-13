using System;

// Token: 0x02000011 RID: 17
public class CRC32
{
	// Token: 0x06000066 RID: 102 RVA: 0x000B7C3E File Offset: 0x000B6C3E
	public CRC32()
	{
		this.BuildCRC32Table();
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000B7C50 File Offset: 0x000B6C50
	private void BuildCRC32Table()
	{
		this.crcTable = new uint[256];
		for (uint num = 0U; num < 256U; num += 1U)
		{
			uint num2 = num;
			for (int i = 0; i < 8; i++)
			{
				num2 = (uint)(((num2 & 1U) == 0U) ? ((ulong)(num2 >> 1)) : (18446744073402876704UL ^ (ulong)(num2 >> 1)));
			}
			this.crcTable[(int)((UIntPtr)num)] = num2;
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x000B7CBC File Offset: 0x000B6CBC
	public uint Calc(byte[] buf)
	{
		uint num = uint.MaxValue;
		for (int i = 0; i < buf.Length; i++)
		{
			num = (this.crcTable[(int)((UIntPtr)((num ^ (uint)buf[i]) & 255U))] ^ num >> 8);
		}
		return (uint)((ulong)num ^ ulong.MaxValue);
	}

	// Token: 0x040003A3 RID: 931
	private const int TABLE_LENGTH = 256;

	// Token: 0x040003A4 RID: 932
	private uint[] crcTable;
}
