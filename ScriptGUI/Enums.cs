using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLumberjack
{
    public enum LumberItems : int
    {
        Fungi = 0x3191, 
        Bark = 0x318f, 
        Switch = 0x2F5F, 
        Plant = 0x3190, 
        Amber = 0x3199,
             
    }
    public enum Lumber : int
    {
        Logs = 0x1BDD,
        Boards = 0x1BD7
    }
    public enum LumberColor : int
    {
        
        Reg = 0x0,
        Oak = 0x7DA,
        Ash = 0x4A7,
        Yew= 0x4A8,
        HeartWood = 0x4A9,
        Bloodwood = 0x4AA,
        Frostwood = 0x47F
    }
}
