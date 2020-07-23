using UnityEngine;

namespace LadderWarp
{
    public class LadderSeat : KerbalSeat
    {
        public override CommNet.VesselControlState GetControlSourceState()
        {
            return CommNet.VesselControlState.None;
        }
    }
}

namespace EVAButtonsNoncontrolledEnabling
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class EVAButtonsEnabling : MonoBehaviour
    {
        public void OnEnable()
        {
            GameEvents.onVesselCreate.Add(onVesselCreated);
            GameEvents.onVesselLoaded.Add(onVesselLoaded);
        }

        public void OnDisable()
        {
            GameEvents.onVesselCreate.Remove(onVesselCreated);
            GameEvents.onVesselLoaded.Remove(onVesselLoaded);
        }

        private void onVesselLoaded(Vessel vessel)
        {
            EnableButtons(vessel);
        }

        private void onVesselCreated(Vessel vessel)
        {
            EnableButtons(vessel);
        }

        private void EnableButtons(Vessel vessel)
        {
            foreach (Part p in vessel.parts)
            {
                foreach (PartModule m in p.FindModulesImplementing<KerbalEVA>())
                {
                    foreach (BaseEvent e in m.Events)
                    {
                        e.requireFullControl = false;
                        e.guiActiveUncommand = true;
                    }
                }
            }
        }
    }
}

