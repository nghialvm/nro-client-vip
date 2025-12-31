using Assets.src.e;
using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

namespace KhanhDTK
{
    public class Bag : IActionListener
    {
        /* AUTHOR: KhanhDTK */
        private enum TypePaint
        {
            typeBody,
            typeBag,
            typeBox
        }
        private static Bag instance;
        public bool isShow = false;
        private TypePaint typePaint;
        private Image bgSize;
        private Image btnItem;
        private Image[] imgBox = new Image[5];
        private Image[] indexBag = new Image[2];
        private int currIndex = 0, currTab = 0;
        private Scroll scrollBag, scrollInfo;
        private int numCol = 5, numRow = 5;
        private int X, Y, W, H;
        private Cell[] cellsBag;
        private Item itemDetails;
        private bool isBody = true, isItemInfo;
        private int selected;
        private Tab[] tabs;
        private Image imgX;
        private Image[] imgTab = new Image[2];
        private int numCol2, numRow2;
        public static Bag gI()
        {
            if (instance == null)
            {
                instance = new Bag();
            }
            return instance;
        }
        private class Cell
        {
            public int x;
            public int y;
            public Cell(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public void paint(mGraphics g)
            {
                g.drawImage(Bag.gI().btnItem, x, y);
            }
        }
        public Bag()
        {
            if (bgSize == null)
            {
                bgSize = GameCanvas.loadImage("/bag/bgSize.png");
            }
            X = GameCanvas.w / 2 - bgSize.getWidth() / 2;
            Y = GameCanvas.h / 2 - bgSize.getHeight() / 2;
            W = bgSize.getWidth();
            H = bgSize.getHeight();
            for (int i = 0; i < imgBox.Length; i++)
            {
                if (imgBox[i] == null)
                {
                    imgBox[i] = GameCanvas.loadImage("/bag/textbox.png");
                }
            }
            btnItem = GameCanvas.loadImage("/bag/btnItem.png");
            for (int i = 0; i < indexBag.Length; i++)
            {
                if (indexBag[i] == null)
                {
                    indexBag[i] = GameCanvas.loadImage($"/bag/index{i}.png");
                }
            }
            InitScroll();
            tabs = new Tab[]
            {
                new Tab("Lấy Ra", this, 1 , null),
                new Tab("Dùng", this, 2, null),
                new Tab("Vứt Ra", this, 3, null),
                new Tab("Cất Vào", this, 4, null),
                new Tab("Cho Đệ", this, 5, null)
            };
            imgX = GameCanvas.loadImage("/mainImage/myTexture2dbtx.png");
            for (int i = 0; i < imgTab.Length; i++)
            {
                if (imgTab[i] == null)
                {
                    imgTab[i] = GameCanvas.loadImage($"/bag/tab{i}.png");
                }
            }
        }
        public void update()
        {
            if (!isShow) return;

            updateScroll(scrollBag);
            updateScroll(scrollInfo);
        }
        public void updateScroll(Scroll sc)
        {
            if (sc != null && sc.cmyLim > 0)
            {
                ScrollResult s = sc.updateKey();
                if (sc.cmy < 0 && !s.isDowning)
                {
                    sc.cmy -= sc.cmy / 2;
                }
                else if (sc.cmy > sc.cmyLim && !s.isDowning)
                {
                    sc.cmy -= (sc.cmy - sc.cmyLim + 6) / 2;
                }
                if (s.isDowning) GameCanvas.isPointerMove = false;
                GameCanvas.clearKeyHold();
            }
        }
        public void updateKey()
        {
            if (!isShow) return;
            if (scrollBag != null)
            {
                scrollBag.updateKey();
            }
            if (scrollInfo != null)
            {
                scrollInfo.updateKey();
            }
        }
        private void InitScroll()
        {
            int x1 = X + 1, y1 = Y, w1 = W - 2, h1 = H - 2;
            int x2 = x1 + 2, y2 = y1 + 2, w2 = w1 - 4, h2 = h1 - 1;
            int x3 = x2 + 2, y3 = y2, w3 = w2 - 4, h3 = h2 - 4;
            int x4 = x3 + 3, y4 = y3 + 2, w4 = w3 - 4, h4 = h3 - 4;
            int x5 = x4 + 4, y5 = y4 + 25, w5 = w4 - 8, h5 = h4 - 30;
            int x6 = x5 + 1, y6 = y5 + 1, w6 = w5 - 2, h6 = h5 - 2;
            int x7 = x6 + 4, y7 = y6 + 8, w7 = w6 / 2 + 2, h7 = h6 - 16;
            int x10 = x7 + w7 + 5;
            int y10 = y7 + 20;
            int w10 = w7 - 16;
            int h10 = h7 - 20;
            GameCanvas.clearAllPointerEvent();
            Item[] items = currIndex == 0 ? Char.myCharz().arrItemBag : Char.myCharz().arrItemBox;
            numCol2 = 6;
            numRow2 = UnityEngine.Mathf.Max(6, UnityEngine.Mathf.CeilToInt(items.Length / 6f));
            scrollBag = new Scroll();
            scrollBag.setStyle(numCol2 * numRow2, btnItem.getWidth() + 5, x10, y10 + btnItem.getHeight(), (btnItem.getWidth() + 5) * 6, h10, true, 6);
        }
        private void InitScrollInfo()
        {
            int x1 = X + 1, y1 = Y, w1 = W - 2, h1 = H - 2;
            int x2 = x1 + 2, y2 = y1 + 2, w2 = w1 - 4, h2 = h1 - 1;
            int x3 = x2 + 2, y3 = y2, w3 = w2 - 4, h3 = h2 - 4;
            int x4 = x3 + 3, y4 = y3 + 2, w4 = w3 - 4, h4 = h3 - 4;
            int x5 = x4 + 4, y5 = y4 + 25, w5 = w4 - 8, h5 = h4 - 30;
            int x6 = x5 + 1, y6 = y5 + 1, w6 = w5 - 2, h6 = h5 - 2;
            int x7 = x6 + 4, y7 = y6 + 8, w7 = w6 / 2 + 2, h7 = h6 - 16;
            int x10 = x7 + w7 + 5;
            int y10 = y7 + 20;
            int w10 = w7 - 16;
            int h10 = h7 - 20;
            GameCanvas.clearAllPointerEvent();
            scrollInfo = new Scroll();
            scrollInfo.clear();
            if (itemDetails != null && itemDetails.itemOption != null)
                scrollInfo.setStyle(itemDetails.itemOption.Length, mFont.tahoma_7b_green.getHeight() + itemDetails.itemOption.Length, x7 + 5, y7 + 40, w7 - 10, h7 - 80, true, 1);

        }
        public void Paint(mGraphics g)
        {
            if (!isShow) return;
            g.setColor(15839327);
            // viền ngoài 
            g.fillRect(X, Y, W, H, 10);
            int x1 = X + 1, y1 = Y, w1 = W - 2, h1 = H - 2;
            g.setColor(2233091, 1f);
            // viền ngoài 
            g.fillRect(x1, y1, w1, h1, 10);
            int x2 = x1 + 2, y2 = y1 + 2, w2 = w1 - 4, h2 = h1 - 1;
            g.setColor(3678729);
             // viền ngoài 
            g.fillRect(x2, y2, w2, h2, 8);
            int x3 = x2 + 2, y3 = y2, w3 = w2 - 4, h3 = h2 - 4;
            g.setColor(7228184, 2f);
             // viền ngoài 
            g.fillRect(x3, y3, w3, h3, 4);
            int x4 = x3 + 3, y4 = y3 + 2, w4 = w3 - 4, h4 = h3 - 4;
            g.setColor(11562007, 2f);
             // viền ngoài 
            g.fillRect(x4, y4, w4, h4, 4);
            mFont.tahoma_7b_green.drawStringBd(g, "-----YTB KhanhDTK-----", GameCanvas.w / 2 + x4 + 10, y4 + 2, mFont.LEFT, mFont.tahoma_7);
            g.drawImage(imgX, GameCanvas.w - x4 - 10 - imgX.getWidth(), y4, 0);
            if (GameCanvas.isPointerHoldIn(GameCanvas.w - x4 - 10 - imgX.getWidth(), y4, imgX.getWidth(), imgX.getHeight()))
            {
                if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick)
                {
                    isShow = false;
                    GameCanvas.clearAllPointerEvent();
                }
            }
            g.drawImage((currTab == 0) ? imgTab[1] : imgTab[0], x4 + imgTab[0].getWidth() / 2 + 5, y4 + imgTab[0].getHeight() / 2 + 5, mGraphics.VCENTER | mGraphics.HCENTER);
            mFont.tahoma_7_yellow.drawStringBd(g, "Bản Thân", x4 + imgTab[0].getWidth() / 2 + 5, y4 + imgTab[0].getHeight() / 2, mGraphics.VCENTER | mGraphics.HCENTER, mFont.tahoma_7b_dark);
            g.drawImage((currTab == 1) ? imgTab[1] : imgTab[0], x4 + imgTab[0].getWidth() / 2 + 50, y4 + imgTab[0].getHeight() / 2 + 5, mGraphics.VCENTER | mGraphics.HCENTER);
            mFont.tahoma_7_yellow.drawStringBd(g, "Đệ tử", x4 + imgTab[0].getWidth() / 2 + 50, y4 + imgTab[0].getHeight() / 2, mGraphics.VCENTER | mGraphics.HCENTER, mFont.tahoma_7b_dark);

            int x5 = x4 + 4, y5 = y4 + 25, w5 = w4 - 8, h5 = h4 - 30;
            g.setColor(0, 1f);
            // viền ngoài kế 2 nút bản thân và đệ tử
            g.fillRect(x5, y5, w5, h5, 4);
            int x6 = x5 + 1, y6 = y5 + 1, w6 = w5 - 2, h6 = h5 - 2;
            g.setColor(8545082, 3f);
            // viền ngoài kế tiếp
            g.fillRect(x6, y6, w6, h6, 4);
            int x7 = x6 + 4, y7 = y6 + 8, w7 = w6 / 2 + 2, h7 = h6 - 16;
            g.setColor(6116693);
            // nền  nhân vật 
            if (isBody)
            {
                g.fillRect(x7, y7, w7, h7, 4);
                g.drawImage(TileMap.bong, x7 + w7 / 2, y7 + h7 / 2, mGraphics.VCENTER | mGraphics.HCENTER);
                int cx = x7 + w7 / 2;
                int cy = y7 + h7 / 2 - TileMap.bong.getHeight() / 2;
                #region EFFECT & MYCHARZ
                Char.myCharz().paintCharBody(g, cx, cy + 2, 1, Char.myCharz().cf, true);
                for (int i = 0; i < Char.myCharz().vEffChar.size(); i++)
                {
                    Effect effect = (Effect)Char.myCharz().vEffChar.elementAt(i);
                    if (effect.layer == 1)
                    {
                        bool flag = true;
                        if (effect.isStand == 0)
                        {
                            flag = ((Char.myCharz().statusMe == 1 || Char.myCharz().statusMe == 6) ? true : false);
                        }
                        if (flag)
                        {
                            effect.x = cx;
                            effect.y = cy + 20;
                            effect.paint(g);
                        }
                    }
                }
                #endregion
                #region BODY
                int x8 = x7 + 1;
                int y8 = y7 + h7 / 2 + 35;
                int w8 = w7 - 2;
                int h8 = h7 / 4 + 15;
                var c = Char.myCharz();
                g.setColor(6941494,1f);
                // thanh thông tin ki sức mạnh 
                g.fillRect(x8, y8, w8, h8, 4);
                //Power
                mFont.bigNumber_While.drawString(g, "Pow: ", x8 + 7, y8 + 4, mFont.LEFT);
                g.drawImage(imgBox[0], x8 + mFont.tahoma_7b_white.getWidth("PW: ") / 2 + imgBox[0].getWidth() / 2 + 20, y8 + 10, mGraphics.VCENTER | mGraphics.HCENTER);
                g.setColor(2486262);
                mFont.tahoma_7b_white.drawStringBd(g, Res.formatNumber(c.cPower), x8 + mFont.tahoma_7b_white.getWidth("PW: ") / 2 + imgBox[0].getWidth() / 2 + 22, y8 + 5, mGraphics.VCENTER | mGraphics.HCENTER, mFont.tahoma_7b_dark);
                //Guild
                int xBox1 = x8 + mFont.tahoma_7_yellow.getWidth("Guild: ") / 2 + imgBox[0].getWidth() / 2 + 15;
                int yBox1 = y8 + 15 + imgBox[0].getHeight();
                mFont.tahoma_7_yellow.drawStringBd(g, "Guild: ", x8 + mFont.tahoma_7_yellow.getWidth("Guild: ") + 2, y8 + 20, mFont.RIGHT, mFont.tahoma_7b_dark);
                g.drawImage(imgBox[0], x8 + mFont.tahoma_7_yellow.getWidth("Guild: ") / 2 + imgBox[0].getWidth() / 2 + 15, y8 + 15 + imgBox[0].getHeight(), mGraphics.VCENTER | mGraphics.HCENTER);
                mFont.tahoma_7_white.drawStringBd(g, (c.clan != null) ? c.clan.name : "Chưa gia nhập", xBox1 + 2, yBox1 - 6, mGraphics.VCENTER | mGraphics.HCENTER, mFont.tahoma_7_grey);
                //Stamina
                mFont.tahoma_7_yellow.drawStringBd(g, "PS: ", x8 + mFont.tahoma_7_yellow.getWidth("Guild: ") + 2, y8 + 35, mFont.RIGHT, mFont.tahoma_7b_dark);
                g.drawImage(imgBox[0], x8 + mFont.tahoma_7_yellow.getWidth("Guild: ") / 2 + imgBox[0].getWidth() / 2 + 15, y8 + 30 + imgBox[0].getHeight(), mGraphics.VCENTER | mGraphics.HCENTER);
                int xBox2 = x8 + mFont.tahoma_7_yellow.getWidth("Guild: ") / 2 + imgBox[0].getWidth() / 2 + 15;
                int yBox2 = y8 + 30 + imgBox[0].getHeight();
                int clipS = c.cStamina * imgBox[0].getWidth() / c.cMaxStamina;
                g.setColor(6941494);
                g.fillRect(xBox2 + 1 - imgBox[0].getWidth() / 2, yBox2 + 1 - imgBox[0].getHeight() / 2, clipS - 3, imgBox[0].getHeight() - 2);
                mFont.tahoma_7_white.drawStringBd(g, NinjaUtil.getMoneys(c.cMaxStamina), xBox2 + 1 - imgBox[0].getWidth() / 2 + 6, yBox2 + 1 - imgBox[0].getHeight() / 2 - 2, mFont.LEFT, mFont.tahoma_7b_dark);
                //HP
                int x9 = x8 + w8 / 2 + 20;
                mFont.tahoma_7_yellow.drawStringBd(g, "HP", x9, y8 + 5, mFont.RIGHT, mFont.tahoma_7b_dark);
                g.drawImage(imgBox[0], x9 + imgBox[0].getWidth() / 2 + 5, y8 + 5, mFont.RIGHT);
                int clipHP = (int)(c.cHP * imgBox[0].getWidth() / c.cHPFull);
                g.setColor(16713995);
                g.fillRect(x9 + 6, y8 + 6, clipHP - 2, imgBox[0].getHeight() - 2);
                mFont.tahoma_7_white.drawStringBd(g, NinjaUtil.getMoneys(c.cHP), x9 + 7, y8 + 4, mFont.LEFT, mFont.tahoma_7b_dark);
                //KI
                mFont.tahoma_7_yellow.drawStringBd(g, "MP", x9, y8 + 20, mFont.RIGHT, mFont.tahoma_7b_dark);
                g.drawImage(imgBox[0], x9 + imgBox[0].getWidth() / 2 + 5, y8 + 10 + imgBox[0].getHeight(), mFont.RIGHT);
                int clipKI = (int)(c.cMP * imgBox[0].getWidth() / c.cMPFull);
                g.setColor(2486262);
                g.fillRect(x9 + 6, y8 + imgBox[0].getHeight() + 11, clipKI - 2, imgBox[0].getHeight() - 2);
                mFont.tahoma_7_white.drawStringBd(g, NinjaUtil.getMoneys(c.cMP), x9 + 7, y8 + 20, mFont.LEFT, mFont.tahoma_7b_dark);
                //DMG
                mFont.tahoma_7_yellow.drawStringBd(g, "DMG", x9, y8 + 35, mFont.RIGHT, mFont.tahoma_7b_dark);
                g.drawImage(imgBox[0], x9 + imgBox[0].getWidth() / 2 + 5, y8 + 15 + imgBox[0].getHeight() * 2, mFont.RIGHT);
                g.setColor(6941494);
                // chung màu hành trang
                g.fillRect(x9 + 6, y8 + 16 + imgBox[0].getHeight() * 2, imgBox[0].getWidth() - 2, imgBox[0].getHeight() - 2);
                mFont.tahoma_7_white.drawStringBd(g, NinjaUtil.getMoneys(c.cDamFull), x9 + 7, y8 + 35, mFont.LEFT, mFont.tahoma_7b_dark);
                #endregion
                #region ITEMBODY
                for (int row = 0; row < numRow; row++)
                {
                    for (int col = 0; col < numCol; col++)
                    {
                        int x = x7 + 4 + col * (btnItem.getWidth() + 17);
                        int y = y7 + 4 + row * (btnItem.getHeight() + 2);
                        if (row < 5 && (col == 0 || col == numCol - 1))
                        {
                            g.drawImage(btnItem, x, y, 0);
                            Item item = null;
                            int index = -1;
                            if (col == 0 && row <= 5)
                            {
                                if (Char.myCharz().arrItemBody != null && row < Char.myCharz().arrItemBody.Length && Char.myCharz().arrItemBody[row] != null)
                                {
                                    item = Char.myCharz().arrItemBody[row];
                                }
                                index = row;
                            }
                            else if (col == numCol - 1 && row <= 5)
                            {
                                if (Char.myCharz().arrItemBody != null && row + 5 < Char.myCharz().arrItemBody.Length && Char.myCharz().arrItemBody[row + 5] != null)
                                {
                                    item = Char.myCharz().arrItemBody[row + 5];
                                }
                                index = row + 5;
                            }
                            if (item != null)
                            {
                                SmallImage.drawSmallImage(g, item.template.iconID, x + btnItem.getWidth() / 2, y + btnItem.getHeight() / 2, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                                g.reset();
                                if (GameCanvas.isPointerHoldIn(x, y, btnItem.getWidth(), btnItem.getHeight()) && (GameCanvas.isPointerClick || GameCanvas.isPointerJustDown))
                                {
                                    if (GameCanvas.isPointerJustRelease)
                                    {
                                        isBody = false;
                                        itemDetails = item;
                                        isItemInfo = true;
                                        typePaint = TypePaint.typeBody;
                                        selected = (sbyte)index;
                                        InitScrollInfo();
                                        GameCanvas.clearAllPointerEvent();
                                    }
                                }
                            }
                        }
                        else if (row >= 4)
                        {
                            g.drawImage(btnItem, x, y);
                            int index = (row - 4) * numCol + (col + 8);
                            Item item = null;
                            if (Char.myCharz().arrItemBody != null && index < Char.myCharz().arrItemBody.Length && Char.myCharz().arrItemBody[index] != null)
                            {
                                item = Char.myCharz().arrItemBody[index];
                            }
                            if (item != null)
                            {
                                SmallImage.drawSmallImage(g, item.template.iconID, x + btnItem.getWidth() / 2, y + btnItem.getHeight() / 2, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                                if (GameCanvas.isPointerHoldIn(x, y, btnItem.getWidth(), btnItem.getHeight()) && (GameCanvas.isPointerClick || GameCanvas.isPointerJustDown))
                                {
                                    if (GameCanvas.isPointerJustRelease)
                                    {
                                        isBody = false;
                                        itemDetails = item;
                                        isItemInfo = true;
                                        typePaint = TypePaint.typeBody;
                                        selected = (sbyte)index;
                                        GameCanvas.clearAllPointerEvent();
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            #region ITEMBAG
            int x10 = x7 + w7 + 5;
            int y10 = y7 + 20;
            int w10 = w7 - 16;
            int h10 = h7 - 20;
            g.setColor(6116693); 
            g.fillRect(x10, y10, w10, h10, 4);
            g.drawImage((currIndex == 0) ? indexBag[1] : indexBag[0], x10 + w10 / 2 - indexBag[0].getWidth(), y10 - indexBag[0].getHeight() - 5);
            mFont.tahoma_7_yellow.drawStringBd(g, "Túi Đồ", x10 + w10 / 2 - indexBag[0].getWidth() + 7, y10 - indexBag[0].getHeight(), 0, mFont.tahoma_7b_dark);
            g.drawImage((currIndex == 1) ? indexBag[1] : indexBag[0], x10 + w10 / 2 + 5, y10 - indexBag[0].getHeight() - 5);
            mFont.tahoma_7_yellow.drawStringBd(g, "Rương Đồ", x10 + w10 / 2 + 12, y10 - indexBag[0].getHeight(), 0, mFont.tahoma_7b_dark);
            if (GameCanvas.isPointerHoldIn(x10 + w10 / 2 - indexBag[0].getWidth(), y10 - indexBag[0].getHeight() - 5, indexBag[0].getWidth(), indexBag[0].getHeight()))
            {
                if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick)
                {
                    currIndex = 0;
                    GameCanvas.clearAllPointerEvent();
                }
            }
            if (GameCanvas.isPointerHoldIn(x10 + w10 / 2 + 5, y10 - indexBag[0].getHeight() - 5, indexBag[0].getWidth(), indexBag[0].getHeight()))
            {
                if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick)
                {
                    currIndex = 1;
                    GameCanvas.clearAllPointerEvent();
                }
            }
            if (cellsBag == null || cellsBag.Length != (numCol2 * numRow2))
                cellsBag = new Cell[(numCol2 * numRow2)];
            g.setClip(x10, y10, w10, h10);
            g.translate(0, -scrollBag.cmy);
            for (int row = 0; row < numRow2; row++)
            {
                for (int col = 0; col < numCol2; col++)
                {
                    int x = x10 + 6 + (col * (btnItem.getWidth() + 5));
                    int y = y10 + 5 + (row * (btnItem.getHeight() + 5));
                    int index = row * numCol2 + col;
                    if (cellsBag[index] == null)
                    {
                        cellsBag[index] = new Cell(x, y);
                    }
                    cellsBag[index].paint(g);
                    Item[] items = (currIndex == 0) ? Char.myCharz().arrItemBag : Char.myCharz().arrItemBox;
                    if (items != null && index < items.Length && items[index] != null)
                    {
                        SmallImage.drawSmallImage(g, items[index].template.iconID, x + btnItem.getWidth() / 2, y + btnItem.getHeight() / 2, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                        if (GameCanvas.isPointerHoldIn(cellsBag[index].x, cellsBag[index].y - scrollBag.cmy, btnItem.getWidth(), btnItem.getHeight()))
                        {
                            if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick)
                            {
                                isBody = false;
                                itemDetails = items[index];
                                isItemInfo = true;
                                typePaint = (currIndex == 0) ? TypePaint.typeBag : TypePaint.typeBox;
                                selected = (sbyte)index;
                                InitScrollInfo();
                                GameCanvas.clearAllPointerEvent();
                            }

                        }
                    }
                }
            }
            g.translate(0, scrollBag.cmy);
            g.reset();
            paintItemDetails(g, x7, y7, w7, h7);
            #endregion
        }
        private void paintItemDetails(mGraphics g, int x, int y, int w, int h)
        {
            try
            {
                if (isItemInfo && itemDetails != null)
                {
                    g.setColor(6116693);
                    g.fillRect(x, y, w, h, 10);
                    SmallImage.drawSmallImage(g, itemDetails.template.iconID, x + 2, y + 3, 0, 0);
                    mFont.tahoma_7b_white.drawStringBd(g, itemDetails.template.name, x + 25, y + 10, 0, mFont.tahoma_7);
                    g.setColor(UnityEngine.Color.white);
                    g.fillRect(x + 5, y + 35, w - 10, 2, 10);
                    GameCanvas.panel.addItemDetail(itemDetails);
                    var cp = GameCanvas.panel.cp;
                    if (cp.maxStarSlot > 0)
                    {
                        for (int j = 0; j < cp.maxStarSlot; j++)
                        {
                            g.drawImage(Panel.imgMaxStar, x + 40 - cp.maxStarSlot * 20 / 2 + j * 20 + Panel.imgMaxStar.getWidth(), y + 23);
                        }
                    }
                    if (cp.starSlot > 0)
                    {
                        for (int k = 0; k < cp.starSlot; k++)
                        {
                            g.drawImage(Panel.imgStar, x + 40 - cp.maxStarSlot * 20 / 2 + k * 20 + Panel.imgStar.getWidth(), y + 23);
                        }
                    }
                    g.setClip(x + 5, y + 39, w - 10, h - 80);
                    g.setColor(0);
                    g.fillRect(x + w - 6, y + 40, 4, h - 80, 10);
                    g.setColor(UnityEngine.Color.white);
                    g.fillRect(x + w - 6, y + 41 + scrollInfo.cmy, 2, 20, 10);
                    g.translate(scrollInfo.cmx, -scrollInfo.cmy);
                    if (itemDetails.quantity > 0)
                    {
                        mFont.tahoma_7b_green.drawStringBd(g, $"Số Lượng: {itemDetails.quantity}", x + 6, y + 41, 0, mFont.tahoma_7);
                    }
                    if (itemDetails.itemOption.Length > 0)
                    {
                        string text = string.Empty;
                        int yPaint = y + 51;
                        for (int i = 0; i < itemDetails.itemOption.Length; i++)
                        {
                            if (itemDetails.itemOption[i].optionTemplate.id != 107 && itemDetails.itemOption[i].optionTemplate.id != 102)
                            {
                                mFont.tahoma_7b_yellow.drawString(g, itemDetails.itemOption[i].getOptionString(), x + 6, yPaint, 0, mFont.tahoma_7);
                                yPaint += 12;
                            }
                        }
                    }
                    g.reset();
                    switch (typePaint)
                    {
                        case TypePaint.typeBody:
                        case TypePaint.typeBox:
                            if (tabs[0] != null)
                            {
                                tabs[0].x = x + 5;
                                tabs[0].y = h - 10;
                                tabs[0].paint(g);
                                if (tabs[0].Pressed())
                                {
                                    tabs[0].actionPerform();
                                }
                            }
                            break;
                        case TypePaint.typeBag:
                            if (tabs[1] != null)
                            {
                                tabs[1].x = x + 5;
                                tabs[1].y = h - 10;
                                tabs[1].paint(g);
                                if (tabs[1].Pressed())
                                {
                                    tabs[1].actionPerform();
                                }
                            }
                            if (tabs[2] != null)
                            {
                                tabs[2].x = x + 15 + tabs[1].tabs[0].getWidth() * 2;
                                tabs[2].y = h - 10;
                                tabs[2].paint(g);
                                if (tabs[2].Pressed())
                                {
                                    tabs[2].actionPerform();
                                }
                            }
                            if (tabs[3] != null)
                            {
                                tabs[3].x = x + 10 + tabs[1].tabs[0].getWidth();
                                tabs[3].y = h - 10;
                                tabs[3].paint(g);
                                if (tabs[3].Pressed())
                                {
                                    tabs[3].actionPerform();
                                }
                            }
                            if (tabs[4] != null)
                            {
                                tabs[4].x = x + 5;
                                tabs[4].y = h - 5 + tabs[4].tabs[0].getHeight();
                                tabs[4].paint(g);
                                if (tabs[4].Pressed())
                                {
                                    tabs[4].actionPerform();
                                }
                            }
                            break;
                    }

                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        public void perform(int idAction, object p)
        {
            switch (idAction)
            {
                case 1:
                    if (GameScr.gI().isBagFull())
                    {
                        GameScr.info1.addInfo("|2|Hành Trang Đã Đầy!", 0);
                    }
                    else
                    {
                        if (typePaint == TypePaint.typeBody)
                        {
                            Service.gI().getItem(Panel.BODY_BAG, (sbyte)selected);
                        }
                        else
                        {
                            Service.gI().getItem(Panel.BOX_BAG, (sbyte)selected);
                        }
                    }
                    isItemInfo = false;
                    isBody = true;
                    break;
                case 2:
                    if (!itemDetails.isTypeBody())
                    {
                        Service.gI().useItem(0, 1, (sbyte)itemDetails.indexUI, -1);
                    }
                    else
                    {
                        Service.gI().getItem(Panel.BAG_BODY, (sbyte)selected);
                    }
                    isItemInfo = false;
                    isBody = true;
                    break;
                case 3:
                    Service.gI().useItem(2, 1, (sbyte)itemDetails.indexUI, -1);
                    isItemInfo = false;
                    isBody = true;
                    break;
                case 4:
                    if (GameScr.gI().isBoxFull())
                    {
                        GameScr.info1.addInfo("|2|Rương Đồ Đã Đầy!", 0);
                    }
                    else
                    {
                        Service.gI().getItem(Panel.BAG_BOX, (sbyte)selected);
                    }
                    isItemInfo = false;
                    isBody = true;
                    break;
                case 5:
                    Service.gI().getItem(Panel.BAG_PET, (sbyte)selected);
                    isItemInfo = false;
                    isBody = true;
                    break;
                case 1000:
                    GameCanvas.panel.setTypeMain();
                    GameCanvas.panel.show();
                    break;
                case 999:
                    isShow = true;
                    break;
            }
        }
    }
}