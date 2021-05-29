using System.Reflection;
using EFT.UI;
using EFT.UI.Matchmaker;
using Aki.Common.Utils.Patching;
using Aki.SinglePlayer.Utils;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Spaceman.Hardcore
{
    public class Program
    {
        static void Main(string[] args)
        {
            PatcherUtil.Patch<MatchmakerOfflineRaidPatch>();
        }
    }

    public class MatchmakerOfflineRaidPatch : GenericPatch<MatchmakerOfflineRaidPatch>
    {
        public MatchmakerOfflineRaidPatch() : base(postfix: nameof(PatchPostfix))
        {
        }

        public static void PatchPostfix(UpdatableToggle ____botsEnabledToggle, UpdatableToggle ____enableBosses, UpdatableToggle ____scavWars, UpdatableToggle ____taggedAndCursed, TMPDropDownBox ____aiAmountDropdown, TMPDropDownBox ____aiDifficultyDropdown, List<CanvasGroup> ____canvasGroups, List<CanvasGroup> ____wavesCanvasGroups, CanvasGroup ____pveSettingGroups)
        {
            JObject json = JObject.Parse(RequestHandler.GetJson("/mod/spaceman-harcore/config"));
            JToken jsonData = json["data"];
            try
            {
                ____botsEnabledToggle.interactable = (bool)jsonData["botsEnabledModify"];
                ____enableBosses.interactable = (bool)jsonData["enableBossesModify"];
                ____scavWars.interactable = (bool)jsonData["scavWarsModify"];
                ____taggedAndCursed.interactable = (bool)jsonData["taggedAndCursedModify"];
                ____aiAmountDropdown.enabled = (bool)jsonData["aiAmmountDifficultyModify"];
                ____aiDifficultyDropdown.enabled = (bool)jsonData["aiAmmountDifficultyModify"];

                foreach (CanvasGroup canvasGroup in ____wavesCanvasGroups)
                {
                    canvasGroup.interactable = (bool)jsonData["aiAmmountDifficultyModify"];
                }

            }
            catch
            {
            }
        }

    protected override MethodBase GetTargetMethod()
        {
            return typeof(MatchmakerOfflineRaid).GetMethod("Show", BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }
}
