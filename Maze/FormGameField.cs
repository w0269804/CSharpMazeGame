using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GameComponents;

namespace Maze
{
    public partial class frmGameField : Form
    {

        private GameComponents.Maze maze;
        private Renderer renderer;
        public static readonly int NumberOfCells= 10;
        public event MazeSolvedHandler Solved;
        public event CannonPrimedHandler CannonAimed;

        private static readonly string WinMessage = "You win!";

        public frmGameField()
        {

            maze = new GameComponents.Maze(NumberOfCells, NumberOfCells);
            
            CannonAimed = new CannonPrimedHandler(CannonPrimed);
            Solved = new MazeSolvedHandler(DisplayWin);

            maze.cannonPrimed += CannonAimed;

            InitializeComponent();
            CreateMaze();
            InitializeRenderer();
            SetMazeSolvedEventListener();
            SetFormSize();
        }

        private void SetFormSize()
        {
            this.Height = (NumberOfCells + 1) * Renderer.CellWallHeight + Renderer.EmptyTopSpaceHeight;
            this.Width = (NumberOfCells + 1) * Renderer.CellWallWidth;
        }

        /// <summary>
        /// Initialize the graphics renderer.
        /// </summary>
        private void InitializeRenderer()
        {
            renderer = new Renderer(NumberOfCells, NumberOfCells);
        }

        /// <summary>
        /// Set the event listener.
        /// </summary>
        private void SetMazeSolvedEventListener()
        {
            Solved += new MazeSolvedHandler(CannonPrimed);
        }

        /// <summary>
        /// Creates the maze.
        /// </summary>
        private void CreateMaze()
        {
            maze.InitializeMaze();
        }

        /// <summary>
        /// Handles pressing keys, will move 
        /// the actor throughout the maze.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGameField_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool actorMoved = false;

            switch(e.KeyChar.ToString().ToUpper())
            {
                case "S":
                    actorMoved = maze.MoveActor(GameComponents.Maze.Directions.South);
                    break;
                case "W":
                    actorMoved = maze.MoveActor(GameComponents.Maze.Directions.North);
                    break;
                case "A":
                    actorMoved = maze.MoveActor(GameComponents.Maze.Directions.West);
                    break;
                case "D":
                    actorMoved = maze.MoveActor(GameComponents.Maze.Directions.East);
                    break;

                case " ":
                    actorMoved = maze.BlastWall();
                    break;
                case "6":
                    maze.AimCannon(GameComponents.Maze.Directions.East);
                    break;
                case "4":
                    maze.AimCannon(GameComponents.Maze.Directions.West);
                    break;
                case "8":
                    maze.AimCannon(GameComponents.Maze.Directions.North);
                    break;
                case "2":
                    maze.AimCannon(GameComponents.Maze.Directions.South);
                    break;

                case "R":
                    CreateMaze();
                    actorMoved = true;
                    break;
            }

            if(actorMoved)           
            Invalidate();

            if(maze.MazeSolved())
            {
                if(Solved != null)
                {
                    Solved();
                }

                CreateMaze();
                Invalidate();
            }
            

        }

        /// <summary>
        /// Event listener for window painting. Fires
        /// when changes to the painted area occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGameField_Paint(object sender, PaintEventArgs e)
        {
            renderer.DrawMoveHistory(e.Graphics, maze.MoveHistory);
            renderer.DrawMaze(e.Graphics, maze.GetMaze());
            renderer.DrawActor(e.Graphics, maze.Actor);
            renderer.DrawEndPosition(e.Graphics, maze.EndPosition);
   


            if(maze.MazeSolved())
            {
                renderer.DrawWin(e.Graphics, WinMessage);
            }
            else
            {
                renderer.DisplayCannonStatus(e.Graphics, maze.Actor);
            }

        }


        private void CannonPrimed()
        {
            Refresh();
        }

        private void DisplayWin()
        {
            Refresh();
            Thread.Sleep(2000);
        }
    }
}
