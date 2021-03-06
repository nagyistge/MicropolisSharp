﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micropolis.Utilities
{
    public class TileDrawer
    {
        private const int TILE_SIZE = 16;

        private const int GRID_WIDTH = 256 / TILE_SIZE;
        private const int GRID_HEIGHT = 960 / TILE_SIZE;

        private Texture2D _tileSheet;

        public TileDrawer(Texture2D tileSheet)
        {
            _tileSheet = tileSheet;
        }

        public void DrawTile(int tileId, SpriteBatch batch, Vector2 drawPosition, Color overrideColor)
        {
            //Translate Tile Id to grid position
            int y = tileId / GRID_WIDTH;
            int x = tileId % GRID_WIDTH;

            if ((y < 0 || y > GRID_HEIGHT) || (x < 0 || x > GRID_WIDTH))
            {
                throw new Exception("Invalid Grid Tile");
            }

            batch.Draw(_tileSheet, Normalise(drawPosition), ClippedRectange(drawPosition, new Rectangle(x * TILE_SIZE, y * TILE_SIZE, TILE_SIZE, TILE_SIZE)), overrideColor);
        }

        private Rectangle ClippedRectange(Vector2 drawPosition, Rectangle original)
        {
            int x = original.X;
            int y = original.Y;
            int w = original.Width;
            int h = original.Height;

            if(drawPosition.X < 0)
            {
                x = (int)(x -drawPosition.X); // x - dp.X == x + Abs(dp.X) because dp.X < 0
                w = (int)(w + drawPosition.X);
            }
            if (drawPosition.Y < 0)
            {
                y = (int)(y - drawPosition.Y); // x - dp.X == x + Abs(dp.X) because dp.X < 0
                h = (int)(h + drawPosition.Y);
            }

            return new Rectangle(x, y, w, h);
        }

        private Vector2 Normalise(Vector2 drawPosition)
        {
            return new Vector2(Math.Max(drawPosition.X, 0), Math.Max(drawPosition.Y, 0));
        }

        internal void DrawAnimatedTile(int tileId, int cycle, SpriteBatch spriteBatch, Vector2 vector2)
        {
            if ((tileId >= 128 && tileId < 143) || (tileId >= 192 && tileId <= 207))
            {
                DrawTile(tileId - (16 * cycle), spriteBatch, vector2, Color.White);
            }
        }
    }
}
