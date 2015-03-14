using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameComponents
{
    /// <summary>
    /// Represents a cell in a maze
    /// with four walls which will 
    /// either be passable or impassable
    /// </summary>
    public class Cell
    {
        protected bool leftWall = true, rightWall = true, topWall = true, bottomWall = true;
        protected int row, col;
        protected bool visited;


        /// <summary>
        /// Copy contructure.
        /// </summary>
        /// <param name="cell">The cell to create a copy of.</param>
        public Cell (Cell cell)
        {
            this.leftWall = cell.LeftWall;
            this.RightWall = cell.RightWall;
            this.TopWall = cell.TopWall;
            this.Row = cell.Row;
            this.Col = cell.Col;
            this.BottomWall = cell.BottomWall;
        }

        /// <summary>
        /// Creates a cell with the 
        /// specified row and column
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }

        /// <summary>
        /// The column of the cell.
        /// </summary>
        public int Col
        {
            get { return col; }
            set { col = value; }
        }

        /// <summary>
        /// Returns the row of the cell.
        /// </summary>
        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        /// <summary>
        /// Whether the bottom of the cell
        /// is passable or not.
        /// </summary>
        public bool BottomWall
        {
            get { return bottomWall; }
            set { bottomWall = value; }
        }

        /// <summary>
        /// Whether the top of the cell
        /// is passable or not.
        /// </summary>
        public bool TopWall
        {
            get { return topWall; }
            set { topWall = value; }
        }

        /// <summary>
        /// Whether the right of the cell
        /// is passable or not.
        /// </summary>
        public bool RightWall
        {
            get { return rightWall; }
            set { rightWall = value; }
        }

        /// <summary>
        /// Whether the left of the cell
        /// is passable or not.
        /// </summary>
        public bool LeftWall
        {
            get { return leftWall; }
            set { leftWall = value; }
        }


        /// <summary>
        /// Indicates whether it has been visited
        /// </summary>
        public bool Visited
        {
            get { return visited; }
            set { visited = value; }
        }


        public bool BottomWalll { get; set; }
    }
}
