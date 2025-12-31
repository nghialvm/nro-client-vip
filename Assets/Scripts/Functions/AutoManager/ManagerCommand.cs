using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions.AutoManager
{
   internal abstract class ManagerCommand 
    {
        private static int selected;
        public static void paint(mGraphics g)
        {
                //selected = (!GameCanvas.isTouch) ? 1:0;
                g.drawImage((GameCanvas.isTouch) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, 10, 200);
                
            
            mFont.tahoma_7b_white.drawStringBd(g, "Auto Manager", 19, 205,mFont.LEFT,mFont.tahoma_7b_dark);
        }
    }
}
