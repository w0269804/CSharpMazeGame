using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameComponents;
using System.Drawing;

namespace GameComponents
{

    public class Maze
    {
        private Actor actor;
        private Cell[,] maze;
        private Cell goal; 
        private int rows;
        private int cols;
        private Random random;
        private List<Cell> moveHistory;

        public event CannonPrimedHandler cannonPrimed;
        private MazeGenerator mazeGenerator;

        /// <summary>
        /// The possible directions to travel.
        /// </summary>
        public enum Directions
        {
            North, South, West, East, None
        }

        /// <summary>
        /// Creates a maze with the specified dimensions
        /// </summary>
        /// <param name="rows">The number of rows of the maze.</param>
        /// <param name="cols"></param>
        public Maze(int rows, int cols)
        {
            random = new Random();
            mazeGenerator = new MazeGenerator(rows, cols);

            this.rows = rows;
            this.cols = cols;

        }

        /// <summary>
        /// Creates the maze for the game.
        /// </summary>
        public void InitializeMaze()
        {
            mazeGenerator = new MazeGenerator(rows, cols);
            maze = mazeGenerator.GenerateMaze();
            InitializeActor();
            SetEndPosition();
        }

        /// <summary>
        /// Returns whether the actor
        /// occupies the cell designated
        /// as the end cell.
        /// </summary>
        /// <returns></returns>
        public bool MazeSolved()
        {
            return actor.Cell.Equals(goal);
        }

        /// <summary>
        /// Sets the end position of the maze. Will continually
        /// find a random cell until the end position is not equal
        /// with the start position.
        /// </summary>
        private void SetEndPosition()
        {
            do          
                goal = maze[random.Next(rows), random.Next(cols)];
            while (goal == actor.Cell);
        }

        /// <summary>
        /// Set the actor
        /// </summary>
        private void InitializeActor()
        {
            actor = new Actor(maze[random.Next(0, cols - 1),random.Next(0, rows - 1)]);
            moveHistory = new List<Cell>();
            moveHistory.Add(actor.Cell);
        }


        public void AimCannon(Directions direction)
        {
            if(actor.NumberOfShells > 0)
            {
                actor.ShotDirection = direction;

                if (cannonPrimed != null)
                    cannonPrimed();
            }
   
        }

        /// <summary>
        /// Returns the actor.
        /// </summary>
        public Actor Actor
        {
            get { return actor; }
            set { actor = value; }
        }

        /// <summary>
        /// Fire the tank's cannon to destroy
        /// the wall in the direction the cannon
        /// is aimed towards.
        /// </summary>
        /// <returns></returns>
        public bool BlastWall()
        {
            if (actor.NumberOfShells > 0 && actor.ShotDirection != Directions.None)
            {
                DestroyWall(actor.Cell, actor.ShotDirection);
                actor.ShotDirection = Directions.None;
                actor.NumberOfShells--;
            }

            return true;
        }

        /// <summary>
        /// Destroys a target wall
        /// </summary>
        /// <param name="sourceCell"></param>
        /// <param name="direction"></param>
        private void DestroyWall(Cell sourceCell, Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    DestroyNorthWall(sourceCell);
                    break;             
                case Directions.South:
                    DestroySouthWall(sourceCell);
                    break;
                case Directions.West:
                    DestroyWestWall(sourceCell);
                    break; 
                case Directions.East:
                    DestroyEastWall(sourceCell);
                    break;
                case Directions.None:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Destroy the wall south of the actor's
        /// current cell.
        /// </summary>
        /// <param name="sourceCell"></param>
        private void DestroyNorthWall(Cell sourceCell)
        {
            maze[sourceCell.Row, sourceCell.Col].TopWall = false;
            if (sourceCell.Row > 0)
                maze[sourceCell.Row - 1, sourceCell.Col].BottomWall = false;
        }

        /// <summary>
        /// Destroy the wall north of the actor's
        /// current cell.
        /// </summary>
        /// <param name="sourceCell"></param>
        private void DestroySouthWall(Cell sourceCell)
        {
            maze[sourceCell.Row, sourceCell.Col].BottomWall = false;
            if (sourceCell.Row < rows - 1)
                maze[sourceCell.Row + 1, sourceCell.Col].TopWall = false;
        }

        /// <summary>
        /// Destroy the wall west of the actor's
        /// current cell.
        /// </summary>
        /// <param name="sourceCell"></param>
        private void DestroyWestWall(Cell sourceCell)
        {
            maze[sourceCell.Row, sourceCell.Col].LeftWall = false;
            if (sourceCell.Col > 0)
                maze[sourceCell.Row, sourceCell.Col - 1].RightWall = false;
        }

        /// <summary>
        /// Destroy the wall east of the 
        /// actor's cell.
        /// </summary>
        /// <param name="sourceCell"></param>
        private void DestroyEastWall(Cell sourceCell)
        {
            maze[sourceCell.Row, sourceCell.Col].RightWall = false;
            if (sourceCell.Col < cols - 1)
                maze[sourceCell.Row, sourceCell.Col + 1].LeftWall = false;
        }

        /// <summary>
        /// Attempt to move the actor in the supplied
        /// direction.
        /// </summary>
        /// <param name="direction">The direction to move.</param>
        /// <returns>Whether the actor has moved</returns>
        public bool MoveActor(Directions direction)
        {
            bool moved;

            if (moved = CanMove(actor.Cell.Row, actor.Cell.Col, direction))
            {
                Cell temp = Actor.Cell; /// the current cell before the move
 
                switch (direction)
                {
                    case Directions.North:
                        actor.Cell = maze[actor.Cell.Row - 1, actor.Cell.Col];
                        break;
                    case Directions.South:
                        actor.Cell = maze[actor.Cell.Row + 1, actor.Cell.Col];
                        break;
                    case Directions.West:
                        actor.Cell = maze[actor.Cell.Row, actor.Cell.Col - 1];
                        break;
                    case Directions.East:
                        actor.Cell = maze[actor.Cell.Row, actor.Cell.Col + 1];
                        break;
                    default:
                        break;
                }


                actor.LastDirection = direction;

                if (moveHistory.Contains(actor.Cell))
                {
                    int firstTempPosition = moveHistory.IndexOf(moveHistory.First(number => number == actor.Cell));
                    int lastPosition = moveHistory.Count - moveHistory.IndexOf(moveHistory.Last(number => number == actor.Cell));
                    moveHistory.RemoveRange(firstTempPosition, lastPosition);
                    moveHistory.Add(actor.Cell);              
                }
                  

                else
                    moveHistory.Add(actor.Cell);
                
            }

            return moved;
        }

        /// <summary>
        /// Marks all cell positions within
        /// the array of cells to visisted.
        /// Used to clear the visisted status
        /// after generating the maze pattern.
        /// </summary>
        private void ClearVisisted(Cell[,] maze)
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    maze[i, j].Visited = false;
        }



        /// <summary>
        /// Checks to see if the actor can move to
        /// the given row or column by checking if 
        /// it is a valid cell and if the wall configuration
        /// will allow the actor to travel there.
        /// </summary>
        /// <param name="row">The destination row.</param>
        /// <param name="col">The destination column.</param>
        /// <param name="direction">The direction of the destination from source.</param>
        /// <returns></returns>
        private bool CanMove(int row, int col, Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    return (row > 0 && !maze[row - 1, col].BottomWall);
                case Directions.South:
                    return (row < rows - 1 && !maze[row + 1, col].TopWall);
                case Directions.East:
                    return (col < cols - 1 && !maze[row, col + 1].LeftWall);
                case Directions.West:
                    return (col > 0 && !maze[row, col - 1].RightWall);
                default:
                    return false;
            }
        }




        /// <summary>
        /// Returns the array of cells.
        /// </summary>
        /// <returns></returns>
        public Cell[,] GetMaze()
        {
            return maze;
        }

        /// <summary>
        /// This cell represents
        /// the target cell for 
        /// the actor to reach.
        /// </summary>
        public Cell EndPosition
        {
            get { return goal; }
            set { goal = value; }
        }

        /// <summary>
        /// Returns the history of actor moves.
        /// </summary>
        public List<Cell> MoveHistory
        {
            get { return moveHistory; }
        }
    }
}
