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
        Vector2[ , ] _positions;

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

                float yPosition = Sprite.Position.Y + _spacing;
                int doubleSpacing = _spacing * 2; //This is used so there is space for lines left

                //Calculate the positions:
                for (int i = 0; i < _side; i++)
                {
                    for (int j = 0; j < _side; j++)
                    {
                        _positions[i, j] = new Vector2(_squareSide * j + Sprite.Position.X + _spacing + doubleSpacing * j, yPosition);
                    }

                    yPosition += _squareSide + doubleSpacing;
                }
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
    }
}
