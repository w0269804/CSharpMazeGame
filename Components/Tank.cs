using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameComponents
{
    public class Tank : Actor
    {
        private int numberOfShells = 3;
        private Maze.Direction shotDirection = Maze.Direction.None; // the direction the cannon is aiming

        /// <summary>
        /// Initialize an actor on a given co-ordinate on
        /// the maze.
        /// </summary>
        /// <param name="cell"></param>
          /// <summary>
        /// Creates an actor object
        /// which will have a certain
        /// cell within a grid.
        /// </summary>
        /// <param name="position"></param>
        public Tank(Cell position)
        {
            this.Cell = position;
        }

        /// <summary>
        /// The number of shells the tank 
        /// remaining.
        /// </summary>
        public int NumberOfShells
        {
            get { return numberOfShells; }
            set { numberOfShells = value; }
        }

        /// <summary>
        /// The direction in which the cannon
        /// is aimed.
        /// </summary>
        public Maze.Direction ShotDirection
        {
            get { return shotDirection; }
            set { shotDirection = value; }
        }
    }
}
