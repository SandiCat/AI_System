using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Sandi_s_Way;
using C3.XNA;


namespace AI_System
{
    public class Grid : GameObject
    {
        public Grid()
            : base()
        {
        }
        public Grid(Vector2 position)
            : base(position)
        {
        }
        public Grid(Vector2 position,  Dictionary<Type, char> ANSI, int side, int spacing, int squareSide) 
            : base(position)
        {
            _ANSIrepresentations = ANSI;
            _side = side;
            _spacing = spacing;
            _squareSide = squareSide;
        }//To use this construcor you are need to use the "ImportExisting" method, not "Create"


        public GameObject[,] Objects;

        Dictionary<Type, char> _ANSIrepresentations;
        Vector2[,] _positions;

        Vector2[,] _verticalLinePoints; //Pairs of vectors, each row representing a line
        Vector2[,] _horizontalLinePoints; //Pairs of vectors, each row representing a line
        const int _lineThickness = 1;

        readonly int _side;
        readonly int _spacing;

        readonly int _squareSide;

        public override void Create(GameObject createdObject)
        {
            if (createdObject == this)
            {
                if (_ANSIrepresentations == null || _side == 0 || _spacing == 0 || _squareSide == 0) 
                    //This means that the object isnt initialized properly
                {
                    throw new NullReferenceException();
                }

                Objects = new GameObject[_side, _side];
                _positions = new Vector2[_side, _side];

                int doubleSpacing = _spacing * 2; //This is used so there is space for lines left

                #region Calculate square positions

                float yPosition = Sprite.Position.Y + _spacing;

                for (int i = 0; i < _side; i++)
                {
                    for (int j = 0; j < _side; j++)
                    {
                        _positions[i, j] = new Vector2(yPosition, _squareSide * j + Sprite.Position.X + _spacing + doubleSpacing * j);
                    }

                    yPosition += _squareSide + doubleSpacing;
                }
                #endregion

                #region Calculate line positions

                _verticalLinePoints = new Vector2[2, _side + 1];
                _horizontalLinePoints = new Vector2[2, _side + 1];

                int lineLenght = _spacing * 2 + _squareSide * _side + doubleSpacing * (_side - 1);
                int lineSpacing = _spacing * 2 + _squareSide;

                for (int i = 0; i <= _side; i++)
                {
                    Vector2 horizontalFirstPoint = new Vector2(Sprite.Position.X, Sprite.Position.Y + lineSpacing * i);
                    Vector2 horizontalSecondPoint = new Vector2(horizontalFirstPoint.X + lineLenght, horizontalFirstPoint.Y);

                    _horizontalLinePoints[0, i] = horizontalFirstPoint;
                    _horizontalLinePoints[1, i] = horizontalSecondPoint;

                    Vector2 verticalFirstPoint = new Vector2(Sprite.Position.X + lineSpacing * i, Sprite.Position.Y);
                    Vector2 verticalSecondPoint = new Vector2(verticalFirstPoint.X, verticalFirstPoint.Y + lineLenght);

                    _verticalLinePoints[0, i] = verticalFirstPoint;
                    _verticalLinePoints[1, i] = verticalSecondPoint;
                }

                #endregion

            }
        }
        public override void Update()
        {
            for (int i = 0; i < _side; i++)
            {
                for (int j = 0; j < _side; j++)
                {
                    if (Objects[i, j] != null) Objects[i, j].Sprite.Position = _positions[i, j];
                }
            } 
        }
        public override void Draw()
        {
            for (int i = 0; i <= _side; i++)
            {
                GameInfo.RefSpriteBatch.DrawLine(_verticalLinePoints[0, i], _verticalLinePoints[1, i], Color.Black, _lineThickness);
                GameInfo.RefSpriteBatch.DrawLine(_horizontalLinePoints[0, i], _horizontalLinePoints[1, i], Color.Black, _lineThickness);
            }
        }

        public Vector2 GetXY(GameObject obj)
        {
            int hashCode = obj.GetHashCode();

            for (int i = 0; i < _side; i++)
            {
                for (int j = 0; j < _side; j++)
                {
                    if (Objects[i, j] != null && Objects[i, j].GetHashCode() == hashCode)
                        return new Vector2(i, j);
                }
            }

            throw new IndexOutOfRangeException();
        }
    }
}
