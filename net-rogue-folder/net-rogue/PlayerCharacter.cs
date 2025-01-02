using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace net_rogue
{
    internal class PlayerCharacter
    {
        public string Name;
        public Species species;
        public Role role;

        public char image;
        public ConsoleColor drawColor;

        public Vector2 position;



        public void move(int moveX, int moveY, Map CurrentMap  )
        {
           
            position.X += moveX;
            position.Y += moveY;
            // Prevent  from going outside screen
            if (position.X < 0)
            {
                position.X = 0;
            }
            else if (position.X > Console.WindowWidth - 1)
            {
                position.X = Console.WindowWidth - 1;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            else if (position.Y > Console.WindowHeight - 1)
            {
                position.Y = Console.WindowHeight - 1;
            }

            if (CurrentMap.mapTiles[(int)(position.X + (position.Y * CurrentMap.mapWidth))] == 2)  //stops player from moving into tiles with impassable id types
            {
                position.X -= moveX;
                position.Y -= moveY;
            }
           

            Draw();

        }

        public void Draw()
        {
            // Draw the player
            Console.SetCursorPosition((int)position.X, (int)position.Y);
            Console.Write("@");
        }
    }

    public enum Species
    {
        Duck,
        Mongoose,
        Elf
    }

    public enum Role
    {
        Cook,
        Smith,
        Rogue
    }
}
