using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameComponents
{
    /// <summary>
    /// Represents the actor 
    /// which will be responsible
    /// for moving throughout the maze
    /// </summary>
    public class Actor
    {
        private Cell cell; /// the cell the actor is occupying
        private bool shotRemains = true;
        private int numberOfShells = 3;
        private Maze.Directions shotDirection = Maze.Directions.None;

        /// <summary>
        /// Creates an actor object
        /// which will have a certain
        /// cell within a grid.
        /// </summary>
        /// <param name="position"></param>
        public Actor(Cell position)
        {
            this.cell = position;
            LastDirection = Maze.Directions.None;
        }

        /// <summary>
        /// Returns the game cell
        /// in which the actor exists
        /// </summary>
        public Cell Cell
        {
            get { return cell; }
            set { cell = value; }
        }

        /// <summary>
        /// The last direction the actor moved.
        /// </summary>
        public Maze.Directions LastDirection
        {
            get;
            set;
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
        public Maze.Directions ShotDirection
        {
            get { return shotDirection; }
            set { shotDirection = value; }
        }



    }
}
