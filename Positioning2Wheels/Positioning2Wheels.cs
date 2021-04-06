using EventArgsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Positioning2WheelsNS
{
    public class Positioning2Wheels
    {
        int robotId;
        Location robotLocation = new Location();
        double fEch = 50;

        public Positioning2Wheels(int id)
        {
            robotId = id;
        }

        public void OnOdometryRobotSpeedReceived(object sender, PolarSpeedArgs e)
        {
            /// Ajoutez votre code de calcul de la nouvelle position ici
            

             robotLocation.X += e.Vx * 1/ fEch * Math.Cos(robotLocation.Theta);
            robotLocation.Y += e.Vx * 1 / fEch * Math.Sin(robotLocation.Theta);
            robotLocation.Theta += e.Vtheta * 1/fEch ;

            /// Ajoutez l'appel à l'event de transmission de la position calculée ici
            OnCalculatedLocation(robotId, robotLocation);
        }

        //Output events
        public event EventHandler<LocationArgs> OnCalculatedLocationEvent;
        public virtual void OnCalculatedLocation(int id, Location locationRefTerrain)
        {
            var handler = OnCalculatedLocationEvent;
            if (handler != null)
            {
                handler(this, new LocationArgs { RobotId = id, Location = locationRefTerrain });
            }
        }
    }
}
