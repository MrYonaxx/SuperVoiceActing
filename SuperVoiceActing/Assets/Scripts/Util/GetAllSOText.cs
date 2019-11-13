/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using System.IO;

namespace VoiceActing
{
    public class GetAllSOText: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [FilePath]
        public string filePath;

        [AssetList(AutoPopulate = true)]
        public List<StoryEventData> storyEventDataList;

        [AssetList(AutoPopulate = true)]
        public List<DoublageEventData> doublageDataList;

        [AssetList(AutoPopulate = true)]
        public List<ContractData> contractList;

        [AssetList(AutoPopulate = true)]
        public List<SkillActorData> skillList;

        #endregion

        [Button]
        private void GetAllText()
        {
            StreamWriter streamWriter = new StreamWriter(filePath);




            StoryEventText storyEventText = null;
            for (int i = 0; i < storyEventDataList.Count; i++)
            {
                for (int j = 0; j < storyEventDataList[i].GetEventSize(); j++)
                {
                    if (storyEventDataList[i].GetEventNode(j) is StoryEventText)
                    {
                        storyEventText = (StoryEventText)storyEventDataList[i].GetEventNode(j);
                        streamWriter.WriteLine(storyEventText.Text);
                    }
                }
            }




            DoublageEventText doublageEventText = null;
            for (int i = 0; i < doublageDataList.Count; i++)
            {
                for (int j = 0; j < doublageDataList[i].GetEventSize(); j++)
                {
                    if (doublageDataList[i].GetEventNode(j) is DoublageEventText)
                    {
                        doublageEventText = (DoublageEventText)doublageDataList[i].GetEventNode(j);
                        streamWriter.WriteLine(doublageEventText.Text);
                    }
                }
            }




            for (int i = 0; i < contractList.Count; i++)
            {
                streamWriter.WriteLine(contractList[i].Name);

                for (int j = 0; j < contractList[i].Description.Length; j++)
                    streamWriter.WriteLine(contractList[i].Description[j]);

                for (int j = 0; j < contractList[i].TextDataContract.Length; j++)
                {
                    for (int k = 0; k < contractList[i].TextDataContract[j].TextDataPossible.Length; k++)
                    {
                        streamWriter.WriteLine(contractList[i].TextDataContract[j].TextDataPossible[k].Text);
                    }
                }
            }




            for (int i = 0; i < skillList.Count; i++)
            {
                streamWriter.WriteLine(skillList[i].SkillName);
                streamWriter.WriteLine(skillList[i].Description);
            }



            streamWriter.Close();
        }

    } 

} // #PROJECTNAME# namespace