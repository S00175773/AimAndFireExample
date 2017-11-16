using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimatedSprite
{
    class Sentry : RotatingSprite
    {
        private Game myGame;
        Vector2 StartPosition;
        float chaseRdaius = 200;
        float previousAngleOfRotation = 0;
        bool following = false;
        Vector2 target = new Vector2(0,0);
        public Projectile sentryProjectile { get; set; }

        public Sentry(Game g, Texture2D texture, Vector2 userPosition, int framecount) 
                : base(g,texture,userPosition,framecount)
            {
            myGame = g;
            StartPosition = position;
        }

        public bool inChaseZone(PlayerWithWeapon p)
        {
            float distance = Math.Abs(Vector2.Distance(this.WorldOrigin, p.CentrePos));
            if (distance <= chaseRdaius)
                return true;
            return false;
        }

        public void follow(PlayerWithWeapon p)
        {
            if (inChaseZone(p))
            {
                this.angleOfRotation = TurnToFace(StartPosition, p.CentrePos, angleOfRotation, .1f);
                following = true;
                target = p.position;
            }
        }

        public void unfollow(PlayerWithWeapon p)
        {
            following = false;
        }

        public void loadProjectile(Projectile r)
        {
            sentryProjectile = r;
        }

        public override void Update(GameTime gametime)
        {
        
            if (sentryProjectile != null && sentryProjectile.ProjectileState == Projectile.PROJECTILE_STATE.STILL)
            {
                sentryProjectile.position = this.position;
                sentryProjectile.hit = false;
                // fire the rocket and it looks for the target
                if (following && previousAngleOfRotation == angleOfRotation) 
                    sentryProjectile.fire(target);
            }

            if (sentryProjectile != null)
                sentryProjectile.Update(gametime);

            previousAngleOfRotation = angleOfRotation;
            base.Update(gametime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (sentryProjectile != null && sentryProjectile.ProjectileState != Projectile.PROJECTILE_STATE.STILL)
                sentryProjectile.Draw(spriteBatch);

        }


    }
}
