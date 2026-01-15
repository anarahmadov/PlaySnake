using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaySnake
{
    internal class Snake
    {
        public int Length { get; set; }
        public int Score { get; set; }

        override public string ToString()
        {
            string snakeString = "";
            for (int i = 0; i < Length; i++)
            {
                snakeString += "=";
            }
            return snakeString;
        }
    }
}
