using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Cjayride.SteadySail
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "cjayride.SteadySail";
        public const string PluginName = "Steady Sail";
        public const string PluginVersion = "1.0.0";

        public static ConfigEntry<float> SettleSeconds;
        public static ConfigEntry<float> MaxUpSpeed;

        void Awake()
        {
            SettleSeconds = Config.Bind("General", "Settle seconds", 0.4f,
                "After the sail reaches its new height, keep damping the roll for this many seconds.");
            MaxUpSpeed = Config.Bind("General", "Max upward speed", 1.5f,
                "Upward speed cap, in meters per second, while the sail is moving. Stops the mast shove from launching the hull.");
            new Harmony(PluginGuid).PatchAll();
            Logger.LogInfo("Sail changes no longer roll or launch the boat.");
        }

        internal static void Steady(Ship ship)
        {
            if (ship == null || !ship.m_hasSail || ship.m_body == null)
            {
                return;
            }

            SailSettle settle = ship.GetComponent<SailSettle>();
            if (settle == null)
            {
                settle = ship.gameObject.AddComponent<SailSettle>();
            }

            settle.Apply(ship);
        }
    }

    [DefaultExecutionOrder(10000)]
    sealed class SailSettle : MonoBehaviour
    {
        float _until;

        public void Apply(Ship ship)
        {
            if (!ship.m_sailWasInPosition)
            {
                _until = Time.time + Mathf.Max(0f, Plugin.SettleSeconds.Value);
            }

            if (Time.time > _until)
            {
                return;
            }

            Rigidbody body = ship.m_body;
            Vector3 up = ship.transform.up;
            Vector3 angular = body.angularVelocity;
            Vector3 yaw = Vector3.Project(angular, up);
            body.angularVelocity = yaw + (angular - yaw) * 0.1f;

            Vector3 velocity = body.linearVelocity;
            float rise = Vector3.Dot(velocity, Vector3.up);
            float cap = Plugin.MaxUpSpeed.Value;
            if (rise > cap)
            {
                body.linearVelocity = velocity - Vector3.up * (rise - cap);
            }
        }
    }

    [HarmonyPatch(typeof(Ship), nameof(Ship.CustomFixedUpdate))]
    static class AfterShipPhysics
    {
        static void Postfix(Ship __instance)
        {
            Plugin.Steady(__instance);
        }
    }
}
