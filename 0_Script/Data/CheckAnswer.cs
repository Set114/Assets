using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnswer : MonoBehaviour
{
    public TestDataManager testDataManager;
    public void CorrectAnswerData(int Index)
    {
        string answerData = "";
        int score = 0;
        if(Index == 0 || Index == 1 || Index == 3)
        {
            answerData = "物理";
        }
        else if(Index == 2 || Index == 4)
        {
            answerData = "化學";
        }

        if(Index == 0 || Index == 1 || Index == 2)
        {
            score = 33 ;
        }
        else if(Index == 3 || Index == 4)
        {

            score = 50 ;
        }
        testDataManager.Getanswer(answerData);
        testDataManager.Getscore(score);
        // lvl1TestGM.SetScore(Index,answer);
        // lvl1TestGM.GetAnswer(Index,answerData);

    }

    public void WrongAnswerData(int Index)
    {
        string answerData = "";
        if(Index == 0 || Index == 1 || Index == 3)
        {
            answerData = "化學";

        }
        else if(Index == 2 || Index == 4)
        {
            answerData = "物理";

        }

        testDataManager.Getanswer(answerData);
        testDataManager.Getscore(0);
        // lvl1TestGM.GetAnswer(Index,answerData);
        // lvl1TestGM.SetScore(Index,0);
    }


}
