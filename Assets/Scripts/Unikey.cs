using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x02000089 RID: 137
internal class Unikey
{
	// Token: 0x06000704 RID: 1796 RVA: 0x0006E964 File Offset: 0x0006CB64
	public static char[] getDau(int source, char befor)
	{
		if (source != 97)
		{
			if (source == 119)
			{
				int num = Unikey.chuW.IndexOf(befor);
				bool flag = num == -1;
				if (flag)
				{
					return new char[]
					{
						befor,
						(char)source
					};
				}
				bool flag2 = num >= 3;
				if (flag2)
				{
					return new char[]
					{
						Unikey.chuW[num - 3],
						(char)source
					};
				}
				return new char[]
				{
					Unikey.chuW[num + 3]
				};
			}
		}
		else
		{
			bool flag3 = befor == 'a';
			if (flag3)
			{
				return new char[]
				{
					'â'
				};
			}
		}
		bool flag4 = source == 111 && befor == 'o';
		char[] result;
		if (flag4)
		{
			result = new char[]
			{
				'ô'
			};
		}
		else
		{
			bool flag5 = source == 97 && befor == 'â';
			if (flag5)
			{
				result = new char[]
				{
					'a',
					'a'
				};
			}
			else
			{
				bool flag6 = source == 111 && befor == 'ô';
				if (flag6)
				{
					result = new char[]
					{
						'o',
						'o'
					};
				}
				else
				{
					bool flag7 = source == 101 && befor == 'e';
					if (flag7)
					{
						result = new char[]
						{
							'ê'
						};
					}
					else
					{
						bool flag8 = source == 101 && befor == 'ê';
						if (flag8)
						{
							result = new char[]
							{
								'e',
								'e'
							};
						}
						else
						{
							bool flag9 = source == 100 && befor == 'd';
							if (flag9)
							{
								result = new char[]
								{
									'đ'
								};
							}
							else
							{
								bool flag10 = source == 100 && befor == 'đ';
								if (flag10)
								{
									result = new char[]
									{
										'd',
										'd'
									};
								}
								else
								{
									bool flag11 = source == 122;
									if (flag11)
									{
										foreach (string text in Unikey.laydau.Values)
										{
											bool flag12 = text.IndexOf(befor) != -1;
											if (flag12)
											{
												return new char[]
												{
													Unikey.nguyenam[text.IndexOf(befor)]
												};
											}
										}
										bool flag13 = Unikey.nguyenamdau.ContainsKey(befor);
										if (flag13)
										{
											return new char[]
											{
												Unikey.nguyenamdau[befor]
											};
										}
									}
									bool flag14 = Unikey.laydau.ContainsKey(source);
									if (flag14)
									{
										string text2 = Unikey.laydau[source];
										bool flag15 = Unikey.nguyenam.IndexOf(befor) != -1;
										if (flag15)
										{
											int pos = Unikey.getPos(Unikey.nguyenam, befor);
											return new char[]
											{
												Unikey.getChar(text2, pos)
											};
										}
										bool flag16 = text2.IndexOf(befor) != -1;
										if (flag16)
										{
											int index = text2.IndexOf(befor);
											return new char[]
											{
												Unikey.nguyenam[index],
												(char)source
											};
										}
									}
									result = new char[]
									{
										befor,
										(char)source
									};
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0006ECC8 File Offset: 0x0006CEC8
	private static int getPos(string s, char c)
	{
		for (int i = 0; i < s.Length; i++)
		{
			bool flag = s[i] == c;
			if (flag)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0006ED08 File Offset: 0x0006CF08
	private static char getChar(string s, int pos)
	{
		return s[pos];
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0006ED24 File Offset: 0x0006CF24
	private static int laysonguyenam(string s)
	{
		int num = -1;
		for (int i = 0; i < s.Length; i++)
		{
			bool flag = Unikey.nguyenam.Contains(s[i]);
			if (flag)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0006ED70 File Offset: 0x0006CF70
	private static int layvitriphuam(string s)
	{
		string text = Unikey.phuam + " " + Unikey.tohopphuam;
		string[] array = text.Split(new char[]
		{
			' '
		});
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = s.EndsWith(array[i]);
			if (flag)
			{
				return s.LastIndexOf(array[i]);
			}
		}
		return -1;
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x0006EDE0 File Offset: 0x0006CFE0
	private static int layvitringuyenam(string s)
	{
		for (int i = 0; i < s.Length; i++)
		{
			bool flag = Unikey.nguyenam.Contains(s[i]);
			if (flag)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x0006EE24 File Offset: 0x0006D024
	private static int laynguyenammangdauphu(string s)
	{
		List<char> list = new List<char>
		{
			'â',
			'ă',
			'ê',
			'ô',
			'ư',
			'ơ',
			'Â',
			'Ă',
			'Ê',
			'Ô',
			'Ư',
			'Ơ'
		};
		for (int i = 0; i < s.Length; i++)
		{
			bool flag = list.Contains(s[i]);
			if (flag)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0006EEFC File Offset: 0x0006D0FC
	private static string getDau(char source, char befor)
	{
		bool flag = Unikey.laydau.ContainsKey((int)source);
		if (flag)
		{
			string text = Unikey.laydau[(int)source];
			bool flag2 = Unikey.nguyenam.IndexOf(befor) != -1;
			if (flag2)
			{
				int pos = Unikey.getPos(Unikey.nguyenam, befor);
				return Unikey.getChar(text, pos).ToString();
			}
			bool flag3 = text.IndexOf(befor) != -1;
			if (flag3)
			{
				int index = text.IndexOf(befor);
				return Unikey.nguyenam[index].ToString() + source.ToString();
			}
		}
		return source.ToString() + befor.ToString();
	}

	// Token: 0x040009AF RID: 2479
	private static string nguyenam = "aăâeêioôơuưy";

	// Token: 0x040009B0 RID: 2480
	private static string chuW = "auoăươ";

	// Token: 0x040009B1 RID: 2481
	private static string phuam = "b c d đ g h k l m n p q r s t v x";

	// Token: 0x040009B2 RID: 2482
	private static string tohopphuam = "ch gh kh ng ngh nh ph th tr gi qu";

	// Token: 0x040009B3 RID: 2483
	private static string newText = "";

	// Token: 0x040009B4 RID: 2484
	private static string lastText = "";

	// Token: 0x040009B5 RID: 2485
	private static int thanhIndex = -1;

	// Token: 0x040009B6 RID: 2486
	public static int lastSpace = 0;

	// Token: 0x040009B7 RID: 2487
	public static string text = "";

	// Token: 0x040009B8 RID: 2488
	public static int caretPos = 0;

	// Token: 0x040009B9 RID: 2489
	private static Dictionary<char, char> nguyenamdau = new Dictionary<char, char>
	{
		{
			'ă',
			'a'
		},
		{
			'â',
			'a'
		},
		{
			'ê',
			'e'
		},
		{
			'ô',
			'o'
		},
		{
			'ơ',
			'o'
		},
		{
			'ư',
			'u'
		}
	};

	// Token: 0x040009BA RID: 2490
	private static Dictionary<int, string> laydau = new Dictionary<int, string>
	{
		{
			102,
			"àằầèềìòồờùừỳ"
		},
		{
			115,
			"áắấéếíóốớúứý"
		},
		{
			106,
			"ạặậẹệịọộợụựỵ"
		},
		{
			120,
			"ãẵẫẽễĩõỗỡũữỹ"
		}
	};
}
