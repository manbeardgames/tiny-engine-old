using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tiny
{
    /// <summary>
    ///     Defines values tha describe the action to take when adding a
    ///     new value to the grid if the grid cell being added to already
    ///     contains a value.
    /// </summary>
    public enum OverwriteBehavior
    {
        /// <summary>
        ///     Describes that adding a value is invalid and an exception should be thrown
        /// </summary>
        Invalid,

        /// <summary>
        ///     Describes that the older value should be used instead of the new value given.
        /// </summary>
        TakeOlder,

        /// <summary>
        ///     Describes that the new value should be used to overwrite the old value.
        /// </summary>
        TakeNewer,
    }


    /// <summary>
    ///     Represents a grid of values.
    /// </summary>
    /// <typeparam name="T">
    ///     Defines the type for the values that will be stored in each grid cell.
    /// </typeparam>
    public class Grid<T>
    {


        //  The values that make up this grid.
        private T[,] _values;

        //  The empty value to use for grid cells that are empty.
        private readonly T _emptyValue;

        //  A collection of point values that describe the column and row of empty cells.
        private List<Point> _emptyCells;

        //  A collectin of point values that describe the column and row of filled cells.
        private List<Point> _filledCells;

        /// <summary>
        ///     Gets the value that is used as the empty cell value.
        /// </summary>
        public T EmptyValue => _emptyValue;

        /// <summary>
        ///     Gets or Sets the value of the grid cell at the
        ///     <paramref name="column"/> and <paramref name="row"/> values given.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     A <see cref="ArgumentOutOfRangeException"/> exception will be thrown
        ///     if either the column or row values given are outside the bounds of
        ///     this grid.
        /// </exception>
        /// <exception cref="Exception">
        ///     A <see cref="Exception"/> exception will be thrown if a value already
        ///     exists within the grid cell.
        /// </exception>
        /// <param name="column">
        ///     A <see cref="int"/> value that represents the column of the grid cell
        ///     to add the value to.
        /// </param>
        /// <param name="row">
        ///     A <see cref="int"/> value that represents the row of the grid cell
        ///     to add the value to.
        /// </param>
        /// <returns>
        ///     The value of the grid cell as type <see cref="{T}"/>
        /// </returns>
        public T this[int column, int row]
        {
            get
            {
                if (!Maths.IsInRange(column, 0, Size.X))
                {
                    throw new ArgumentOutOfRangeException(nameof(column), $"The column value given is outside the bounds of this grid. Value given was: {column}.  Expected a value greater than or equal to zero and less than {Size.X}");
                }

                if (!Maths.IsInRange(row, 0, Size.Y))
                {
                    throw new ArgumentOutOfRangeException(nameof(row), $"The row value given is outside the bounds of this grid. Value given was: {row}.  Expected a value greater than or equal to zero and less than {Size.Y}");
                }

                return _values[row, column];
            }

            set
            {
                Add(value, column, row);
            }
        }

        /// <summary>
        ///     Gets or Sets the value of the grid cell at the
        ///     <paramref name="index"/> values provided.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     A <see cref="ArgumentOutOfRangeException"/> exception will be thrown
        ///     if the index  given is outside the bounds of this grid.
        /// </exception>
        /// <exception cref="Exception">
        ///     A <see cref="Exception"/> exception will be thrown if a value already
        ///     exists within the grid cell.
        /// </exception>
        /// <param name="index">
        ///     A <see cref="int"/> value that represents the index of the grid cell
        ///     to add the value to.
        /// </param>
        /// <returns>
        ///     The value of the grid cell as type <see cref="{T}"/>
        /// </returns>
        public T this[int index]
        {
            get
            {
                if (!Maths.IsInRange(index, 0, CellCount))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), $"The index value given is outside the bounds of this grid. Value given was: {index}");
                }

                Maths.GetColumnAndRow(index, Size.X, out int column, out int row);
                return _values[row, column];
            }

            set
            {
                Add(value, index);
            }
        }

        /// <summary>
        ///     Gets or Sets the value of the grid cell at the
        ///     <paramref name="cell"/> values given.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     A <see cref="ArgumentOutOfRangeException"/> exception will be thrown
        ///     the <paramref name="cell"/> given is outside the bounds of
        ///     this grid.
        /// </exception>
        /// <exception cref="Exception">
        ///     A <see cref="Exception"/> exception will be thrown if a value already
        ///     exists within the grid cell.
        /// </exception>
        /// <param name="cell">
        ///     A <see cref="Point"/> value that whos <see cref="Point.X"/> and <see cref="Point.Y"/>
        ///     defines the column and row of a grid cell.
        /// </param>
        /// <returns>
        ///     The value of the grid cell as type <see cref="{T}"/>
        /// </returns>
        public T this[Point cell]
        {
            get
            {
                if (!Maths.IsInRange(cell.X, 0, Size.X))
                {
                    throw new ArgumentOutOfRangeException(nameof(cell.X), $"The column value given is outside the bounds of this grid. Value given was: {cell.X}.  Expected a value greater than or equal to zero and less than {Size.X}");
                }

                if (!Maths.IsInRange(cell.Y, 0, Size.Y))
                {
                    throw new ArgumentOutOfRangeException(nameof(cell.Y), $"The row value given is outside the bounds of this grid. Value given was: {cell.Y}.  Expected a value greater than or equal to zero and less than {Size.Y}");
                }

                return _values[cell.Y, cell.X];
            }

            set
            {
                Add(value, cell);
            }
        }

        /// <summary>
        ///     Gets a <see cref="Point"/> value whos <see cref="Point.X"/> and
        ///     <see cref="Point.Y"/> define the total count of columns and the
        ///     total count of rows respectivly in this grid.
        /// </summary>
        public Point Size { get; private set; }

        /// <summary>
        ///     Gets a <see cref="int"/> value that defines the total number of
        ///     grid cells in this grid.
        /// </summary>
        public int CellCount { get; private set; }

        public IReadOnlyList<Point> EmptyCells { get; private set; }

        public IReadOnlyList<Point> FilledCells { get; private set; }

        /// <summary>
        ///     Creates a new <see cref="Grid{T}"/> instance.
        /// </summary>
        /// <param name="columns">
        ///     A <see cref="int"/> value that defines the total number of
        ///     columns to create for the grid.
        /// </param>
        /// <param name="rows">
        ///     A <see cref="int"/> value that defines the total number of
        ///     rows to create for the grid.
        /// </param>
        /// <param name="emptyValue">
        ///     A value that defines the value of an empty grid cell.
        /// </param>
        public Grid(int columns, int rows, T emptyValue)
        {
            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows), $"The column count must be a value greater than 0.  Value given was {columns}");
            }

            if (rows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows), $"The row count must be a value greater than 0. Value given was {rows}");
            }

            Size = new Point(columns, rows);
            _values = new T[rows, columns];
            _emptyValue = emptyValue;

            _emptyCells = new List<Point>();
            _filledCells = new List<Point>();

            EmptyCells = _emptyCells.AsReadOnly();
            FilledCells = _filledCells.AsReadOnly();

            Clear();
        }

        /// <summary>
        ///     Adds the provided <paramref name="value"/> to the grid cell at the
        ///     <paramref name="column"/> and <paramref name="row"/> values given.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     A <see cref="ArgumentOutOfRangeException"/> exception will be thrown
        ///     if either the column or row values given are outside the bounds of
        ///     this grid.
        /// </exception>
        /// <exception cref="Exception">
        ///     A <see cref="Exception"/> exception will be thrown if a value already
        ///     exists within the grid cell.
        /// </exception>
        /// <param name="value">
        ///     The <see cref="{T}"/> value to add.
        /// </param>
        /// <param name="column">
        ///     A <see cref="int"/> value that represents the column of the grid cell
        ///     to add the value to.
        /// </param>
        /// <param name="row">
        ///     A <see cref="int"/> value that represents the row of the grid cell
        ///     to add the value to.
        /// </param>
        public void Add(T value, int column, int row)
        {
            Add(value, new Point(column, row), OverwriteBehavior.Invalid);
        }

        /// <summary>
        ///     Adds the provided <paramref name="value"/> to the grid cell at the
        ///     <paramref name="index"/> given.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     A <see cref="ArgumentOutOfRangeException"/> exception will be thrown
        ///     if either the grid index provided is outside the bounds of this grid.
        /// </exception>
        /// <exception cref="Exception">
        ///     A <see cref="Exception"/> exception will be thrown if a value already
        ///     exists within the grid cell.
        /// </exception>
        /// <param name="value">
        ///     The <see cref="{T}"/> value to add.
        /// </param>
        /// <param name="index">
        ///     A <see cref="int"/> value that represents the index of the grid cell
        ///     to add the value to.
        /// </param>
        public void Add(T value, int index)
        {
            Maths.GetColumnAndRow(index, Size.X, out int column, out int row);
            Add(value, new Point(column, row), OverwriteBehavior.Invalid);
        }

        /// <summary>
        ///     Adds the provided <paramref name="value"/> to the <paramref name="cell"/> given.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="{T}"/> value to add.
        /// </param>
        /// <param name="cell">
        ///     A <see cref="Point"/> value that whos <see cref="Point.X"/> and <see cref="Point.Y"/>
        ///     defines the column and row of a grid cell.
        /// </param>
        public void Add(T value, Point cell)
        {
            Add(value, cell, OverwriteBehavior.Invalid);
        }

        /// <summary>
        ///     Adds the given <paramref name="value"/> to the grid cell at the
        ///     <paramref name="column"/> and <paramref name="row"/> values.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     A <see cref="ArgumentOutOfRangeException"/> exception will be thrown
        ///     if either the column or row values given are outside the bounds of
        ///     this grid.
        /// </exception>
        /// <exception cref="Exception">
        ///     A <see cref="Exception"/> exception will be thrown if a value already
        ///     exists within the grid cell and the <paramref name="behavior"/> value
        ///     is <see cref="OverwriteBehavior.Invalid"/>.
        /// </exception>
        /// <param name="value">
        ///     The <see cref="{T}"/> value to add.
        /// </param>
        /// <param name="column">
        ///     A <see cref="int"/> value that represents the column of the grid cell
        ///     to add the value to.
        /// </param>
        /// <param name="row">
        ///     A <see cref="int"/> value that represents the row of the grid cell
        ///     to add the value to.
        /// </param>
        /// <param name="behavior">
        ///     A <see cref="OverwriteBehavior"/> value that defines the behavior to take
        ///     if the grid cell already contains a value. If <see cref="OverwriteBehavior.Invalid"/>
        ///     is given, an exception will be thrown.
        /// </param>
        public void Add(T value, int column, int row, OverwriteBehavior behavior)
        {
            Add(value, new Point(column, row), behavior);
        }

        /// <summary>
        ///     Adds the given <paramref name="value"/> to the grid cell at the
        ///     <paramref name="column"/> and <paramref name="row"/> values.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     A <see cref="ArgumentOutOfRangeException"/> exception will be thrown
        ///     if either the grid index provided is outside the bounds of this grid.
        /// </exception>
        /// <exception cref="Exception">
        ///     A <see cref="Exception"/> exception will be thrown if a value already
        ///     exists within the grid cell and the <paramref name="behavior"/> value
        ///     is <see cref="OverwriteBehavior.Invalid"/>.
        /// </exception>
        /// <param name="value">
        ///     The <see cref="{T}"/> value to add.
        /// </param>
        /// <param name="index">
        ///     A <see cref="int"/> value that represents the index of the grid cell
        ///     to add the value to.
        /// </param>
        /// <param name="behavior">
        ///     A <see cref="OverwriteBehavior"/> value that defines the behavior to take
        ///     if the grid cell already contains a value. If <see cref="OverwriteBehavior.Invalid"/>
        ///     is given, an exception will be thrown.
        /// </param>
        public void Add(T value, int index, OverwriteBehavior behavior)
        {
            Maths.GetColumnAndRow(index, Size.X, out int column, out int row);
            Add(value, new Point(column, row), behavior);
        }

        /// <summary>
        ///     Adds the given value to the grid cell at the <paramref name="cell"/> value
        ///     provided.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="{T}"/> value to add.
        /// </param>
        /// <param name="cell">
        ///     A <see cref="Point"/> value that whos <see cref="Point.X"/> and <see cref="Point.Y"/>
        ///     defines the column and row of a grid cell.
        /// </param>
        /// <param name="behavior">
        ///     A <see cref="OverwriteBehavior"/> value that defines the behavior to take
        ///     if the grid cell already contains a value. If <see cref="OverwriteBehavior.Invalid"/>
        ///     is given, an exception will be thrown.
        /// </param>
        public void Add(T value, Point cell, OverwriteBehavior behavior)
        {
            if (!Maths.IsInRange(cell.X, 0, Size.X))
            {
                throw new ArgumentOutOfRangeException(nameof(cell), $"The column value given is outside the bounds of this grid. Value given was: {cell.X}.  Expected a value greater than or equal to zero and less than {Size.X}");
            }

            if (!Maths.IsInRange(cell.Y, 0, Size.Y))
            {
                throw new ArgumentOutOfRangeException(nameof(cell), $"The row value given is outside the bounds of this grid. Value given was :{cell.Y}.  Expected a value greater than or equal to zero and less than {Size.Y}");
            }

            if (IsEmpty(cell))
            {
                _values[cell.Y, cell.X] = value;
            }
            else
            {
                if (behavior == OverwriteBehavior.Invalid)
                {
                    throw new Exception($"Unable to add a value to a grid cell that is not empty");
                }
                else if (behavior == OverwriteBehavior.TakeNewer)
                {
                    _values[cell.Y, cell.X] = value;
                    _filledCells.Add(cell);
                    _emptyCells.Remove(cell);
                }
            }
        }

        /// <summary>
        ///     Removes any value in the grid cell at the <paramref name="column"/> and
        ///     <paramref name="row"/> values provided by setting its value to the
        ///     default value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     A <see cref="ArgumentOutOfRangeException"/> exception will be thrown
        ///     if either the column or row values given are outside the bounds of
        ///     this grid.
        /// </exception>
        /// <param name="column">
        ///     A <see cref="int"/> value that represents the column of the grid cell
        ///     to add the value to.
        /// </param>
        /// <param name="row">
        ///     A <see cref="int"/> value that represents the row of the grid cell
        ///     to add the value to.
        /// </param>
        public void Remove(int column, int row)
        {
            Remove(new Point(column, row));
        }

        /// <summary>
        ///     Removes any value in the grid cell at the <paramref name="index"/>
        ///     value provided by setting its value to the default value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     A <see cref="ArgumentOutOfRangeException"/> exception will be thrown
        ///     if the index given is outside the bounds of this grid.
        /// </exception>
        /// <param name="index">
        ///     A <see cref="int"/> value that represents the index of the grid cell
        ///     to add the value to.
        /// </param>
        public void Remove(int index)
        {
            Maths.GetColumnAndRow(index, Size.X, out int column, out int row);
            Remove(new Point(column, row));
        }

        /// <summary>
        ///     Removes any value in the grid cell at the <paramref name="cell"/> value provided
        ///     by setting its value to the <see cref="EmptyValue"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     A <see cref="ArgumentOutOfRangeException"/> exception will be thrown
        ///     if either the column or row values given are outside the bounds of
        ///     this grid.
        /// </exception>
        /// <param name="cell">
        ///     A <see cref="Point"/> value that whos <see cref="Point.X"/> and <see cref="Point.Y"/>
        ///     defines the column and row of a grid cell.
        /// </param>
        public void Remove(Point cell)
        {
            if (!Maths.IsInRange(cell.X, 0, Size.X))
            {
                throw new ArgumentOutOfRangeException(nameof(cell), $"The column value given is outside the bounds of this grid. Value given was: {cell.X}.  Expected a value greater than or equal to zero and less than {Size.X}");
            }

            if (!Maths.IsInRange(cell.Y, 0, Size.Y))
            {
                throw new ArgumentOutOfRangeException(nameof(cell), $"The row value given is outside the bounds of this grid. Value given was :{cell.Y}.  Expected a value greater than or equal to zero and less than {Size.Y}");
            }

            _values[cell.Y, cell.X] = _emptyValue;
            _emptyCells.Add(cell);
            _filledCells.Remove(cell);

        }

        /// <summary>
        ///     Clears the grid of all values by setting all grid cells equal to the
        ///     default value.
        /// </summary>
        public void Clear()
        {
            _filledCells.Clear();
            _emptyCells.Clear();

            for (int row = 0; row < Size.Y; row++)
            {
                for (int column = 0; column < Size.X; column++)
                {
                    _values[row, column] = _emptyValue;
                    _emptyCells.Add(new Point(column, row));
                }
            }
        }

        /// <summary>
        ///     Checks the grid cell located at the <paramref name="index"/> given,
        ///     then returns a <see cref="bool"/> indicating if that grid cell is empty.
        /// </summary>
        /// <param name="index">
        ///     A <see cref="int"/> value that defines the index of the grid cell to check
        /// </param>
        /// <returns>
        ///     <c>true</c> if the grid cell is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty(int index)
        {
            Maths.GetColumnAndRow(index, Size.X, out int column, out int row);
            return IsEmpty(column, row);
        }

        /// <summary>
        ///     Checks the grid cell located at the <paramref name="cell"/> value
        ///     given, then returns a <see cref="bool"/> indicating if that grid
        ///     cell is empty.
        /// </summary>
        /// <param name="cell">
        ///     A <see cref="Point"/> value who's <see cref="Point.X"/> and
        ///     <see cref="Point.Y"/> values define the column and row of
        ///     the grid cell to check.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the grid cell is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty(Point cell)
        {
            return IsEmpty(cell.X, cell.Y);
        }

        /// <summary>
        ///     Checks the grid cell at the <paramref name="column"/> value and
        ///     <paramref name="row"/> value given, then returns a
        ///     <see cref="bool"/> value indicating if that grid cell is empty.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     A <see cref="ArgumentOutOfRangeException"/> exception will be thrown
        ///     if either the column or row values given are outside the bounds of
        ///     this grid.
        /// </exception>
        /// <param name="column">
        ///     A <see cref="int"/> value that represents the column of the grid
        ///     cell to check.
        /// </param>
        /// <param name="row">
        ///     A <see cref="int"/> vlaue that represents the row of the grid cell
        ///     to check.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the grid cell is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty(int column, int row)
        {
            if (!Maths.IsInRange(column, 0, Size.X))
            {
                throw new ArgumentOutOfRangeException(nameof(column), $"The column value given is outside the bounds of this grid. Value given was: {column}.  Expected a value greater than or equal to zero and less than {Size.X}");
            }

            if (!Maths.IsInRange(row, 0, Size.Y))
            {
                throw new ArgumentOutOfRangeException(nameof(row), $"The row value given is outside the bounds of this grid. Value given was: {row}.  Expected a value greater than or equal to zero and less than {Size.Y}");
            }

            return !EqualityComparer<T>.Default.Equals(this[column, row], _emptyValue);
        }
    }
}
