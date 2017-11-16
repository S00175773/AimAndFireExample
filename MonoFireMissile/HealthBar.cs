using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Health
{
    class HealthBar
    {
        public int health;
        private Texture2D txBar; // hold the texture
        Rectangle _healthRect;   // display the Health bar size

        public Rectangle HealthRect
        {
            get
            {
                return _healthRect;
            }

            set
            {
                _healthRect = value;
            }
        }

        public HealthBar(GraphicsDevice graphicsDivice)
        {

            txBar = new Texture2D(graphicsDivice, 1, 1);
            txBar.SetData(new Color[] { Color.White });
            health = 100;
            HealthRect = new Rectangle(new Point(100, 100), new Point(100, 100));

        }
        public HealthBar(GraphicsDevice graphicsDivice, Point InitialPosition)
        {

            txBar = new Texture2D(graphicsDivice, 1, 1);
            txBar.SetData(new Color[] { Color.White });
            health = 100;
            HealthRect = new Rectangle(InitialPosition, new Point(100, 20));

        }
        public void update(Point newPos, int newHealth)
        {
            _healthRect.Location = newPos;
            health = newHealth;
            _healthRect = new Rectangle(newPos, new Point(health, 20));
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (health > 60)
                spriteBatch.Draw(txBar, HealthRect, Color.Green);
            else if (health > 30 && health <= 60)
                spriteBatch.Draw(txBar, HealthRect, Color.Orange);
            else if (health > 0 && health <= 30)
                spriteBatch.Draw(txBar, HealthRect, Color.Red);

            spriteBatch.End();
        }
    }
}
