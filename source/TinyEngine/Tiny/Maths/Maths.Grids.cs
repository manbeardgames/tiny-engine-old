/* ----------------------------------------------------------------------------
    MIT License

    Copyright (c) 2020 Christopher Whitley

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
---------------------------------------------------------------------------- */

namespace Tiny
{
    public static partial class Maths
    {
        /// <summary>
        ///     Returns a value indictating if the grid cell at the 
        ///     <paramref name="row"/> and <paramref name="column"/> values provided
        ///     is considered an odd cell.  
        /// </summary>
        /// <remarks>
        ///     A "odd" cell is one where the row and column are both even or where the
        ///     row and column are both odd.
        /// </remarks>
        /// <param name="row">
        ///     An <see cref="int"/> value representing the row number of the grid cell.
        /// </param>
        /// <param name="column">
        ///     An <see cref="int"/> value representing the column number of the grid cell.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the grid cell at the row and column given is odd;
        ///     otherwise, <c>false</c
        /// </returns>
        public static bool IsOdd(int row, int column)
        {
            return ((column % 2 == 0 && row % 2 == 0) || (column % 2 != 0 && row % 2 != 0));
        }

        /// <summary>
        ///     Given the <paramref name="row"/> and <paramref name="column"/> number of a
        ///     grid cell, returns the index of the cell within the grid.
        /// </summary>
        /// <param name="row">
        ///     An <see cref="int"/> value representing the row number of the grid cell.
        /// </param>
        /// <param name="column">
        ///     An <see cref="int"/> value representing the column number of the grid cell.
        /// </param>
        /// <param name="totalColumns">
        ///     An <see cref="int"/> value representing the total number of columns in
        ///     the grid.
        /// </param>
        /// <returns>
        ///     An <see cref="int"/> value whos value is the index of the grid cell
        ///     within its grid.
        /// </returns>
        public static int GetGridIndex(int row, int column, int totalColumns)
        {
            return (row * totalColumns) + column;
        }

        /// <summary>
        ///     Given then index of a gird cell within a grid, calculates the <paramref name="row"/>
        ///     and <paramref name="column"/> of the grid cell.
        /// </summary>
        /// <param name="gridindex">
        ///     An <see cref="int"/> value representing the index of the grid cell.
        /// </param>
        /// <param name="totalColumns">
        ///     An <see cref="int"/> value representing the total number of columns in
        ///     the grid.
        /// </param>
        /// <param name="row">
        ///     When this method returns, contains the row number of the grid cell as
        ///     an <see cref="int"/> value.
        /// </param>
        /// <param name="column">
        ///     When this method returns, contains the column number of the grid cell as
        ///     an <see cref="int"/> value.
        /// </param>
        public static void GetRowAndColumn(int gridindex, int totalColumns, out int row, out int column)
        {
            row = gridindex / totalColumns;
            column = gridindex % totalColumns;
        }
    }
}
