using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    class LerpData
    {
        public Vector2 Start;
        public Vector2 End;
        public float TravelPercentage;
        public float Step;

        LerpStates state = LerpStates.End;

        public void SetUp(Vector2 start, Vector2 end, float step)
        {
            Start = start;
            End = end;
            Step = step;
            state = LerpStates.Lerping;
            TravelPercentage = 0;
        }

        public void Stop()
        {
            state = LerpStates.End;
        }

        public Vector2? Update()
        {
            switch(state)
            {
                case LerpStates.Lerping:

                    Vector2 currentLocationOnLine = Vector2.Lerp(Start, End, TravelPercentage);
                    TravelPercentage += Step;

                    if(TravelPercentage >= 1)
                    {
                        state = LerpStates.End;
                    }

                    return currentLocationOnLine;

                case LerpStates.End:
                    break;
            }

            return null;
        }
    }
}
