using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameComponents;

namespace GameTests
{
    [TestClass]
    public class MazeTests
    {

        Random r = new Random();
        Maze.Directions[] allDirections = {   Maze.Directions.East, 
                                              Maze.Directions.South, 
                                              Maze.Directions.North, 
                                              Maze.Directions.West };

        [TestMethod]
        public void MazeConstructionTest()
        {
            int rows = 5;
            int cols = 5;
            Maze maze = new Maze(rows, cols);
        }

        [TestMethod]
        public void MazeSolvingTest()
        {
            int rows = 2;
            int cols = 5;

            Maze maze = new Maze(rows, cols);
            maze.InitializeMaze();

            while(!maze.MazeSolved())
            {
                maze.MoveActor(allDirections[r.Next(0, allDirections.Length)]);
            }

            Assert.IsTrue(maze.MazeSolved());
        }

        /// <summary>
        /// Test the generation of the move history used
        /// to mark the correct path.
        /// </summary>
        [TestMethod]
        public void TestMoveHistoryGeneration()
        {
            int rows = 5;
            int cols = 5;

            Maze maze = new Maze(rows, cols);
            maze.InitializeMaze();

            bool moved;

            Assert.IsTrue(maze.MoveHistory.Count == 1);

            while(!(moved = maze.MoveActor(allDirections[r.Next(0, allDirections.Length)])))
            {

            }

            Assert.IsTrue(maze.MoveHistory.Count == 2);

        }






    }






}

