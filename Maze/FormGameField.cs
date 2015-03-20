﻿using System;
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
    public partial class FormGameField : Form
    {

        private GameComponents.Maze maze;
        private Renderer renderer;
        public static readonly int NumberOfCells= 10;
        public event MazeSolvedEventHandler Solved;
        public event CannonPrimedEventHandler CannonAimed;

        private static readonly string WinMessage = "You win!";

        public FormGameField()
        {

            maze = new GameComponents.Maze(NumberOfCells, NumberOfCells);
            
            CannonAimed = new CannonPrimedEventHandler(CannonPrimed);
            Solved = new MazeSolvedEventHandler(DisplayWin);

            maze.CannonPrimedEventHandler += CannonAimed;

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
            renderer = new Renderer();
        }

        /// <summary>
        /// Set the event listener.
        /// </summary>
        private void SetMazeSolvedEventListener()
        {
            Solved += new MazeSolvedEventHandler(CannonPrimed);
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
                    actorMoved = maze.MoveActor(GameComponents.Maze.Direction.South);
                    break;
                case "W":
                    actorMoved = maze.MoveActor(GameComponents.Maze.Direction.North);
                    break;
                case "A":
                    actorMoved = maze.MoveActor(GameComponents.Maze.Direction.West);
                    break;
                case "D":
                    actorMoved = maze.MoveActor(GameComponents.Maze.Direction.East);
                    break;

                case " ":
                    actorMoved = maze.BlastWall();
                    break;
                case "6":
                    maze.AimCannon(GameComponents.Maze.Direction.East);
                    break;
                case "4":
                    maze.AimCannon(GameComponents.Maze.Direction.West);
                    break;
                case "8":
                    maze.AimCannon(GameComponents.Maze.Direction.North);
                    break;
                case "2":
                    maze.AimCannon(GameComponents.Maze.Direction.South);
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
                Renderer.DrawWin(e.Graphics, WinMessage);
            }
            else
            {
               Renderer.DisplayCannonStatus(e.Graphics, maze.Actor);
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
