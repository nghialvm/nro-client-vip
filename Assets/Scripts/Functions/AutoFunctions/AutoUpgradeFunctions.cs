using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Functions.AutoFunctions
{
   internal class AutoUpgradeFunctions
{
		/// <summary>
	 public static bool isDapDo;

		// Token: 0x04001281 RID: 4737
		public static Item doDeDap;

		// Token: 0x04001282 RID: 4738
		public static int soSaoCanDap = -1;

		// Token: 0x04001283 RID: 4739
		public static int saoHienTai = -1;

		// Token: 0x04001284 RID: 4740
		public static bool dangBanVang;
		/// </summary>
		public static int thoiVang()
		{
			int num = 0;
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				Item item = global::Char.myCharz().arrItemBag[i];
				if (item != null && item.template.id == 457)
				{
					num += item.quantity;
				}
			}
			return num;
		}
		public static void GotoNpc(int npcID)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId == npcID && global::Math.abs(npc.cx - global::Char.myCharz().cx) >= 50)
				{
					AutoUpgradeFunctions.GotoXY(npc.cx, npc.cy - 1);
					global::Char.myCharz().focusManualTo(npc);
					return;
				}
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000063EE File Offset: 0x000045EE
		public static void GotoXY(int x, int y)
		{
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
		}
		public static void BanVang()
		{
			dangBanVang = true;
			if (TileMap.mapID != 5)
			{
				MapObject.MainObject.StartRunToMapId(5);
				Thread.Sleep(1000);
			}
			while (TileMap.mapID != 5)
			{
				Thread.Sleep(500);
			}
			if (Input.GetKey("q"))
			{
				GameScr.info1.addInfo("Dừng bán vàng", 0);
				dangBanVang = false;
				return;
			}
			while (global::Char.myCharz().xu <= 1500000000L && !Input.GetKey("q"))
			{
				if (thoiVang() <= 0)
				{
					GameScr.info1.addInfo("Hết vàng", 0);
					if (isDapDo)
					{
						isDapDo = false;
						GameScr.info1.addInfo("Đập đồ đã tắt do bạn quá nghèo :v", 0);
					}
					dangBanVang = false;
					return;
				}
				Service.gI().saleItem(1, 1, (short)FindIndexItem(457));
				Thread.Sleep(2500);			
			}
			GameScr.info1.addInfo("Đã bán xong", 0);
			Thread.Sleep(1000);
			dangBanVang = false;
		}

		public static int FindIndexItem(int idItem)
		{
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				if (global::Char.myCharz().arrItemBag[i] != null && (int)global::Char.myCharz().arrItemBag[i].template.id == idItem)
				{
					return global::Char.myCharz().arrItemBag[i].indexUI;
				}
			}
			return -1;
		}
		public static void Update()
        {
			if (AutoUpgradeFunctions.isDapDo && AutoUpgradeFunctions.doDeDap != null)
			{
				AutoUpgradeFunctions.saoHienTai = AutoUpgradeFunctions.soSao(AutoUpgradeFunctions.findItemBagWithIndexUI(AutoUpgradeFunctions.doDeDap.indexUI));
			}
			else
			{
				AutoUpgradeFunctions.saoHienTai = -1;
			}
		}
		public static int soSao(Item item)
		{
			for (int i = 0; i < item.itemOption.Length; i++)
			{
				if (item.itemOption[i].optionTemplate.id == 107)
				{
					return item.itemOption[i].param;
				}
			}
			return 0;
		}
		public static Item findItemBagWithIndexUI(int index)
		{
			foreach (Item item in global::Char.myCharz().arrItemBag)
			{
				if (item != null && item.indexUI == index)
				{
					return item;
				}
			}
			return null;
		}
		public static void AutoDapDo()
		{
			while (isDapDo)
			{
				
				if (TileMap.mapID != 5 && !MapObject.MapService.IsXmapRunning)
				{
					MapObject.MainObject.StartRunToMapId(5);
				}
				while (TileMap.mapID != 5)
				{
					Thread.Sleep(100);
				}
				if (saoHienTai >= soSaoCanDap && doDeDap != null && saoHienTai >= 0 && soSaoCanDap > 0)
				{
					//GameScr.info1.addInfo("Đồ Cần Đập Đã Đạt Số Sao Yêu Cầu", 0);
					soSaoCanDap = -1;
					//isDapDo = false;
					//break;
					//doDeDap = null;
				}
				if (global::Char.myCharz().xu > 200000000L)
				{
					long xu = global::Char.myCharz().xu;
					GotoNpc(21);
					if (doDeDap != null && soSaoCanDap > 0)
					{
						while (!GameCanvas.menu.showMenu)
						{
							Service.gI().combine(1, GameCanvas.panel.vItemCombine);
							Thread.Sleep(500);
						}
						Service.gI().confirmMenu(21, 0);
						GameCanvas.menu.doCloseMenu();
						GameCanvas.panel.currItem = null;
						GameCanvas.panel.chatTField.isShow = false;
					}
				}
				else if (doDeDap != null)
				{
					BanVang();
				}
				Thread.Sleep(100);
			}
		}


	}
}
